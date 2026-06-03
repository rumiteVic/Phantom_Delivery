using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public enum Repartidor { Fantasma, Normal, Heavy, Climber }
    public Repartidor repaTipo;
    private InputSystem_Actions input;

    [Header("Movimiento Base")]
    public float speed = 7f;
    float baseSpeed = 7f;
    public float jumpForce = 20f;
    float baseJump = 20f;
    public Rigidbody2D rb;
    public bool grounded = false;
    public Vector3 offset = new Vector3(0f, -2f, 0f);
    public float radius = 0.6f;
    public LayerMask sueloHumano;
    public LayerMask sueloFantasma;
    
    [Header("Escalada (Solo Climber)")]
    public LayerMask climbLayer;
    public float maxStamina = 5f;
    public float stamina;
    public float staminaDrain = 3f;
    public bool touchingWall;
    public bool isWallHolding;
    float wallJumpLockCounter;
    float wallJumpLockTime = 0.15f;
    float grabLockCounter = 0f;
    float grabLockTime = 0.15f;
    bool canEmergencyJump = true;
    public ParticleSystem partHumo;

    [Header("Referencias y Visuales")]
    public SpriteRenderer characterSprite;
    public Color lowStaminaColor = Color.red;
    private BoxCollider2D col;
    public AudioSource aud;
    public GameObject fantasma, normal, pesado, escalador;
    public GameObject normalBody, pesadoBody, escaladorBody;
    public ParticleSystem ghostPos, ghostDes;

    // Estados internos
    bool canJump = false;
    bool iz = false;
    public Vector2 move;
    public bool isFantasma = true;
    public Package carriedPackage;
    float throwForce = 5f;
    Vector2 dir;
    public Collider2D currentBody;
    public float poseDuration = 0.2f;

    //Extra
    public AudioSource poseer;

    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        DisableAllBodies();
        fantasma.SetActive(true);
        stamina = maxStamina;
        col = GetComponent<BoxCollider2D>();
        StatsRepartidor();
    }

    void Update()
    {
        if (grabLockCounter > 0)
        {
            grabLockCounter -= Time.deltaTime;
        }
        //Detectar suelo
        LayerMask maskActual = repaTipo == Repartidor.Fantasma ? sueloFantasma : sueloHumano;

        grounded = Physics2D.OverlapCircle(transform.position + offset,
                    radius,
                    maskActual
                );
        if (grounded) 
        {
            stamina = maxStamina;
            canEmergencyJump = true;
        }

        //Lanzar y saltar
        move = input.Player.Move.ReadValue<Vector2>();
        if (input.Player.Jump.WasPressedThisFrame()) canJump = true;

        if (input.Player.Throw.WasPressedThisFrame() && !isFantasma) Throw();

        //Escalada si es climber
        if (repaTipo == Repartidor.Climber)
        {
            float directionn = iz ? -1f : 1f;
            Vector2 boxSize = new Vector2(0.2f, col.bounds.size.y * 0.8f);
            Vector2 origin = (Vector2)transform.position + new Vector2(directionn * (col.bounds.extents.x + 0.1f), 0f);
            touchingWall = Physics2D.OverlapBox(origin, boxSize, 0f, climbLayer);

            bool grabPressed = input.Player.Climb.IsPressed();

            isWallHolding = touchingWall && grabPressed && stamina > 0 && grabLockCounter <= 0;

            if (isWallHolding && carriedPackage != null) Throw();
            Parpadeo();
        }
        else
        {
            touchingWall = false;
            isWallHolding = false;
            characterSprite.color = Color.white;
        }

        if (move.x > 0) iz = false;
        else if (move.x < 0) iz = true;

        //Poseer
        if (input.Player.Interact.WasPressedThisFrame())
        {
            if (isFantasma) {
                Collider2D mejorCuerpo = GetNearestBody();
                if (mejorCuerpo != null) StartCoroutine(PoseEffect(mejorCuerpo));
            } else ReturnPhantom();
        }
    }

    void FixedUpdate()
    {
        //Escalada moment
        if (isWallHolding)
        {
            rb.gravityScale = 0;
            // Si se mueve hacia arriba consume más, si está quieto consume poco
            float drainMult = Mathf.Abs(move.y) > 0.1f ? 1f : 0.3f;
            stamina -= staminaDrain * drainMult * Time.fixedDeltaTime;
            rb.linearVelocity = new Vector2(0, move.y * speed * 0.7f);
        }
        else
        {
            if(repaTipo == Repartidor.Fantasma && !grounded)
            {
                rb.gravityScale = 0.6f;
            }
            else
            {
                rb.gravityScale = 1.6f;
            }
            
            if (wallJumpLockCounter > 0) wallJumpLockCounter -= Time.fixedDeltaTime;
            else rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
        }

        if (canJump)
        {
            if (grounded)
            {
                ExecuteJump(Vector2.up * jumpForce);
            }
            else if (isWallHolding)
            {
                float wallDir = iz ? -1f : 1f;

                Vector2 jDir = new Vector2(-wallDir * 0.8f, 1.2f).normalized;
                ExecuteJump(jDir * jumpForce * 1.2f);
                stamina -= 0.5f;
                isWallHolding = false;
                wallJumpLockCounter = wallJumpLockTime;
                grabLockCounter = grabLockTime;
            }
            canJump = false;
        }
    }
    //El salto en si y como de fuerte saltar
    void ExecuteJump(Vector2 force)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(force, ForceMode2D.Impulse);
        aud.Play();
    }

    void Parpadeo()
    {
        if (stamina < maxStamina * 0.3f && stamina > 0) {
            characterSprite.color = Color.Lerp(Color.white, lowStaminaColor, Mathf.PingPong(Time.time * 10, 1));
        } else if (stamina <= 0) {
            characterSprite.color = lowStaminaColor;
            partHumo.Play();
        } else {
            partHumo.Stop();
            characterSprite.color = Color.white;
        }
    }

    void StatsRepartidor() {
        switch (repaTipo) {
            case Repartidor.Fantasma: baseSpeed = 8f; baseJump = 18f; throwForce = 0f; break;
            case Repartidor.Normal: baseSpeed = 10f; baseJump = 26f; throwForce = 5f; break;
            case Repartidor.Heavy: baseSpeed = 5f; baseJump = 14.7f; throwForce = 7f; break;
            case Repartidor.Climber: baseSpeed = 8f; baseJump = 20f; throwForce = 5f; break;
        }
        speed = baseSpeed;
        jumpForce = baseJump;
    }

    void DisableAllBodies() {
        fantasma.SetActive(false); normal.SetActive(false);
        pesado.SetActive(false); escalador.SetActive(false);
    }
    
    public void CambiarCuerpo(Collider2D col){
        DisableAllBodies();
        poseer.Play();
        if (col.CompareTag("Normal")){ normal.SetActive(true); repaTipo = Repartidor.Normal; }
        else if (col.CompareTag("Pesado")){ pesado.SetActive(true); repaTipo = Repartidor.Heavy; }
        else if (col.CompareTag("Escalador")){ escalador.SetActive(true); repaTipo = Repartidor.Climber; stamina = maxStamina; }
        StatsRepartidor();
        isFantasma = false;
        gameObject.layer = LayerMask.NameToLayer("Human");
        rb.linearVelocity = Vector2.zero;
    }

    void Throw() {
        if (carriedPackage == null) return;
        dir = iz ? new Vector2(-1f, 0.6f) : new Vector2(1f, 0.6f);
        if (isWallHolding) dir = new Vector2(0f, 1.5f);
        carriedPackage.OnThrown(dir * throwForce, rb.linearVelocity);
        carriedPackage = null;
        StatsRepartidor();
    }

    Collider2D GetNearestBody() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f); 
        Collider2D nearest = null;
        float shortestDistance = Mathf.Infinity;
        foreach (Collider2D hit in hits) {
            if (hit.CompareTag("Normal") || hit.CompareTag("Pesado") || hit.CompareTag("Escalador")) {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                if (dist < shortestDistance) { shortestDistance = dist; nearest = hit; }
            }
        }
        return nearest;
    }

    IEnumerator PoseEffect(Collider2D col) {
        Transform fantasmita = fantasma.transform;
        Vector3 originalScale = fantasmita.localScale;
        input.Player.Disable();
        float time = 0f;
        ghostPos.Play();
        while (time < poseDuration) {
            float t = time / poseDuration;
            fantasmita.localScale = new Vector3(Mathf.Lerp(1f, 0.6f, t), Mathf.Lerp(1f, 1.8f, t), 1f);
            fantasmita.position = Vector3.Lerp(transform.position, col.transform.position, t * 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        fantasmita.localScale = Vector3.zero;
        input.Player.Enable();
        CambiarCuerpo(col);
        if (col.TryGetComponent(out DeleteBody del)) del.DeleteBodyThere();
        aud.Play();
        fantasmita.localScale = originalScale;
    }

    public void ApplyPackageEffects(Package pkg){
        switch (pkg.paquete){
            case Package.TipoPaquete.Normal:
                speed = baseSpeed;
                jumpForce = baseJump;
                break;

            case Package.TipoPaquete.Pesado:
                speed = baseSpeed * 0.6f;
                jumpForce = baseJump * 0.5f;
                break;

            case Package.TipoPaquete.Fragil:
                speed = baseSpeed;
                jumpForce = baseJump;
                break;
        }
    }
    void ReturnPhantom(){
        DisableAllBodies();
        poseer.Play();
        fantasma.SetActive(true);

        if(repaTipo == Repartidor.Normal){
            Instantiate(normalBody, transform.position, transform.rotation);
        }
        else if(repaTipo == Repartidor.Heavy){
            Instantiate(pesadoBody, transform.position, transform.rotation);
        }
        else if(repaTipo == Repartidor.Climber){
            Instantiate(escaladorBody, transform.position, transform.rotation);
        }
        gameObject.layer = LayerMask.NameToLayer("Phantom");
        repaTipo = Repartidor.Fantasma;
        isFantasma = true;
        if(carriedPackage != null){
            carriedPackage.OnDropped();
            carriedPackage = null;
            speed = baseSpeed;
            jumpForce = baseJump;
        }
        ghostDes.Play();
        StatsRepartidor();
    }
}