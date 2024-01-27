using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float jumpHeight = 3f;
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

    private Rigidbody2D rb;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        defaultGravityScale = 1f;
    }
    void Update()
    {
        desiredJump |= input.RetrieveJumpInput();
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = rb.velocity;

        if(onGround)
        {
            jumpPhase = 0;
        }

        if(desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }

        if(rb.velocity.y > 0)
        {
            rb.gravityScale = upwardMovementMultiplier;
        }
        else if(rb.velocity.y < 0)
        {
            rb.gravityScale = downwardMovementMultiplier;
        }
        else if(rb.velocity.y == 0)
        {
            rb.gravityScale = defaultGravityScale;
        }

        rb.velocity = velocity;
    }

    private void JumpAction()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase++;

            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

            if (velocity.y < 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
}