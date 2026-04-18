using UnityEngine;

public class PlayerController : MonoBehaviour
{
        public enum Repartidor{
        Fantasma,
        Normal,
        Heavy,
        Climber
    }
    public Repartidor repaTipo;
    private InputSystem_Actions input;
    public float speed = 7f;
    float baseSpeed = 7f;
    public float jumpForce = 20f;
    float baseJump = 20f;
    float throwForce = 5f;
    bool canJump = false;
    bool iz = false;
    public Rigidbody2D rb;
    public LayerMask layerMask;
    public bool grounded = false;

    public Vector3 offset = new Vector3(0f, -2f, 0f);
    public float radius = 0.6f;
    public Vector2 move;
    public Package carriedPackage;
    Vector2 dir;

    public LayerMask climbLayer;
    public bool isClimbing;

    public GameObject fantasma;
    public GameObject normal;
    public GameObject pesado;
    public GameObject escalador;

    public bool isFantasma = true;
    public Collider2D currentBody;

    [Header("Stamina Escalada")]
    public Vector3 wallOffset = new Vector3(0.5f, -0.75f, 0f);
    public float maxStamina = 5f;
    public float stamina;
    public float staminaDrain = 1.5f;
    public float staminaRecover = 2f;
    public float wallCheckRadius = 0.2f;
    float wallJumpLockTime = 0.15f;
    float wallJumpLockCounter;
    bool touchingWall;
    bool isWallHolding;
    private BoxCollider2D col;

    public GameObject normalBody;
    public GameObject pesadoBody;
    public GameObject escaladorBody;
    
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        fantasma.SetActive(true);
        normal.SetActive(false);
        pesado.SetActive(false);
        escalador.SetActive(false);
        stamina = maxStamina;
        col = GetComponent<BoxCollider2D>();
        StatsRepartidor();
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
    
    void StatsRepartidor(){
        switch (repaTipo){
            case Repartidor.Fantasma:
                baseSpeed = 7f;
                speed = 7f;
                jumpForce = 20f;
                baseJump = 20f;
                throwForce = 0f;
                break;
                
            case Repartidor.Normal:
                baseSpeed = 7f;
                speed = 7f;
                jumpForce = 20f;
                baseJump = 20f;
                throwForce = 5f;
                break;

            case Repartidor.Heavy:
                baseSpeed = 5f;
                speed = 5f;
                baseJump = 16f;
                jumpForce = 16f;
                throwForce = 7f;
                break;

            case Repartidor.Climber:
                baseSpeed = 10f;
                speed = 10f;
                baseJump = 24f;
                jumpForce = 24f;
                throwForce = 5f;
                break;
        }
        speed = baseSpeed;
        jumpForce = baseJump;
        if (carriedPackage != null){
            ApplyPackageEffects(carriedPackage);
        }
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

    void DisableAllBodies(){
        fantasma.SetActive(false);
        normal.SetActive(false);
        pesado.SetActive(false);
        escalador.SetActive(false);
    }

    public void CambiarCuerpo(Collider2D col){
        DisableAllBodies();
        if (col.CompareTag("Normal")){
            normal.SetActive(true);
            repaTipo = Repartidor.Normal;
        }
              
        else if (col.CompareTag("Pesado")){
            pesado.SetActive(true);
            repaTipo = Repartidor.Heavy;
        }
        else if (col.CompareTag("Escalador")){
            escalador.SetActive(true);
            repaTipo = Repartidor.Climber;
            stamina = maxStamina;
        }
        StatsRepartidor();
        isFantasma = false;
        gameObject.layer = LayerMask.NameToLayer("Human");
        rb.linearVelocity = Vector2.zero;
    }

    void ReturnPhantom(){
        DisableAllBodies();
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
        StatsRepartidor();
    }

    void FixedUpdate(){
        if (isWallHolding){
            rb.gravityScale = 0;

            if (isClimbing){
                stamina -= staminaDrain * Time.fixedDeltaTime;
                rb.linearVelocity = new Vector2(move.x * speed * 0.5f, move.y * speed);
            }
            else{
                rb.linearVelocity = Vector2.zero;
            }

        stamina = Mathf.Max(stamina, 0);
        }
        else{
            rb.gravityScale = 1;
            if (wallJumpLockCounter > 0){
                wallJumpLockCounter -= Time.fixedDeltaTime;
            }
            else{
                rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
            }
        }
        if (canJump){
                if(grounded){
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
                else if (touchingWall && repaTipo == Repartidor.Climber){

                    rb.gravityScale = 1;
                    rb.linearVelocity = Vector2.zero;

                    float direction = iz ? 1f : -1f;
                    Vector2 jumpDir = new Vector2(direction * 1.2f, 1.5f).normalized;

                    rb.AddForce(jumpDir * jumpForce * 1.2f, ForceMode2D.Impulse);

                    wallJumpLockCounter = wallJumpLockTime;
                    stamina -= 1f;
                }
                canJump = false;
            }
        if(grounded){
            stamina = maxStamina;
        }
    }

    void Update(){
        grounded = Physics2D.OverlapCircle(
        transform.position + offset,
        radius,
        layerMask
        );

        move = input.Player.Move.ReadValue<Vector2>();
        if(input.Player.Jump.WasPressedThisFrame()){
            canJump = true;
        }
        if(input.Player.Throw.WasPressedThisFrame() && !isFantasma){
            Throw();
        }
        if (input.Player.Interact.WasPressedThisFrame()){
            if (isFantasma && currentBody != null){
                CambiarCuerpo(currentBody);
                if (TryDeleteBody()) return;
            }
            else if (!isFantasma){
                ReturnPhantom();
            }
        }
        if (input.Player.Activate.WasPressedThisFrame()){
            if (isFantasma && currentBody != null) {
                CambiarCuerpo(currentBody);
            }
            else if (!isFantasma){
                if(carriedPackage != null) {
                    carriedPackage.OnDropped();
                    carriedPackage = null;
                }
                else {
                    ReturnPhantom();
                }
            }
        }

        if(move.x >0){
            iz = false;
        }
        if(move.x < 0){
            iz = true;
        }
        float directionn = iz ? -1f : 1f;
        touchingWall = Physics2D.Raycast(transform.position, Vector2.right * directionn, col.bounds.extents.x + 0.1f, climbLayer);

        bool mirandoPared = (iz && move.x < 0) || (!iz && move.x > 0);

        isWallHolding = repaTipo == Repartidor.Climber && touchingWall && mirandoPared && stamina > 0;
        isClimbing = isWallHolding && Mathf.Abs(move.y) > 0.1f;
    }

    void Throw() {
        if (carriedPackage == null) return;
        
        if(!iz){
            dir = new Vector2(1f, 0.6f);
        }
        else if(iz){
            dir = new Vector2(-1f, 0.6f);
        }
        
        carriedPackage.OnThrown(dir * throwForce, rb.linearVelocity);
        carriedPackage = null;
        speed = baseSpeed;
        jumpForce = baseJump;
    }

    bool TryDeleteBody(){
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (Collider2D hit in hits){
            DeleteBody del = hit.GetComponent<DeleteBody>();

            if (del != null){
                Debug.Log("DELETE ENCONTRADO");
                del.DeleteBodyThere();
                return true;
            }
        }

        return false;
    }
}
