using UnityEngine;

public class AnimationTranslade : MonoBehaviour
{
    public float walkSpeed = 3.5f;
    public float runSpeed = 6f;
    private Vector2 movement;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    private bool isRunning = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        movement = new Vector2(inputX, inputY).normalized;

        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (spriteRenderer != null && movement.x != 0)
        {
            spriteRenderer.flipX = movement.x < 0;
        }

        if (animator != null)
        {
            bool isMoving = movement.sqrMagnitude > 0;
            animator.SetBool("Caminando", isMoving);
            animator.SetBool("Corriendo", isMoving && isRunning);
            animator.SetFloat("MoveX", movement.x);
            animator.SetFloat("MoveY", movement.y);
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            float currentSpeed = isRunning ? runSpeed : walkSpeed;
            rb.MovePosition(rb.position + movement * currentSpeed * Time.fixedDeltaTime);
        }
    }

    public void MoverArriba() { movement.y = 1f; }
    public void MoverAbajo() { movement.y = -1f; }
    public void MoverIzquierda() { movement.x = -1f; }
    public void MoverDerecha() { movement.x = 1f; }
    public void Detenerse() { movement = Vector2.zero; }
}