using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private float xInput;


    [Header("Collision Check")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    
    
    
    private bool facingRight = true;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
    }


    void Update()
    {
        AnimationControllers();
        CollisionChecks();
        FlipController();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();



        if (Input.GetKeyDown(KeyCode.Space))

            Jump();
    }

    private void AnimationControllers()
    {
        anim.SetFloat("xvelocity",rb.velocity.x);
        anim.SetFloat("yvelocity", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
    }


    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    private void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if(mousePos.x < transform.position.x && facingRight)
            Flip();
        else if(mousePos.x > transform.position.x && !facingRight)
            Flip();
    }


    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, groundCheckRadius);
    }
}