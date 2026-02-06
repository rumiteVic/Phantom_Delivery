using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Repartidor{
        Fantasma,
        Normal,
        Heavy,
        Climber
    }

    public Repartidor repaTipo;
    SpriteRenderer sprite;
    private InputSystem_Actions input;
    public float speed = 3f;
    float baseSpeed = 3f;
    public float jumpForce = 12f;
    float baseJump = 12f;
    public float throwForce = 5f;
    bool canJump = false;
    public Rigidbody2D rb;

    public LayerMask layerMask;
    public bool grounded = false;

    public Vector3 offset;
    public float radius;
    public float distance;
    Vector2 move;
    public Package carriedPackage;
    public PlayerController cont;
    Vector2 dir;

    public LayerMask climbLayer;
    bool isClimbing;
    void StatsRepartidor(){
        switch (repaTipo){
            case Repartidor.Fantasma:
                baseSpeed = 3f;
                speed = 3f;
                jumpForce = 8f;
                baseJump = 8f;
                throwForce = 5f;
                break;
                
            case Repartidor.Normal:
                baseSpeed = 3f;
                speed = 3f;
                jumpForce = 8f;
                baseJump = 8f;
                throwForce = 5f;
                break;

            case Repartidor.Heavy:
                baseSpeed = 2f;
                speed = 2f;
                baseJump = 5f;
                jumpForce = 5f;
                throwForce = 4f;
                break;

            case Repartidor.Climber:
                baseSpeed = 2.5f;
                speed = 2.5f;
                baseJump = 7f;
                jumpForce = 7f;
                throwForce = 3f;
                break;
        }
    }

    void ApplyPackageEffects(Package pkg){
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
                speed = baseSpeed * 0.9f;
                jumpForce = baseJump * 0.8f;
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new InputSystem_Actions();
        input.Player.Enable();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        StatsRepartidor();
    }

    
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(move.x * speed, rb.linearVelocity.y);
        if (canJump && grounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canJump = false;
        }

        if (isClimbing){
            rb.gravityScale = 0;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * speed);
        }
        else{
            rb.gravityScale = 1;
        }
    }

    // Update is called once per frame
    void Update(){
        grounded = Physics2D.OverlapCircle(
        transform.position + offset,
        radius,
        layerMask
        );

        move = input.Player.Move.ReadValue<Vector2>();
        if(move.x >0){
            sprite.flipX = false;
        }
        if(move.x < 0){
            sprite.flipX = true;
        }
        if(input.Player.Jump.WasPressedThisFrame()){
            canJump = true;
        }
        if(input.Player.Throw.WasPressedThisFrame() && !cont.isFantasma){
            Throw();
        }
        if (repaTipo == Repartidor.Climber){
            isClimbing = Physics2D.OverlapCircle(transform.position, 0.2f, climbLayer);
        }
    }
    
    void Throw() {
        if (carriedPackage == null) return;
        
        if(!sprite.flipX){
            dir = new Vector2(1f, 0.6f);
        }
        else if(sprite.flipX){
            dir = new Vector2(-1f, 0.6f);
        }
        
        carriedPackage.OnThrown(dir * throwForce);
        carriedPackage = null;
        speed = baseSpeed;
        jumpForce = baseJump;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        
        if (carriedPackage != null) return;
            Package pkg = col.GetComponent<Package>();

            if (col.gameObject.CompareTag("Pakete") && carriedPackage == null && !cont.isFantasma)
            {
                carriedPackage = pkg;
                carriedPackage.OnPickedUp(transform);
            }
        
    }
}