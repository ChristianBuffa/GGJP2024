using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Ground ground;
    private Animator animator;
    private SpriteManagement spriteManager;

    private float maxSpeedChange;
    private float Acceleration;
    private bool onGround;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        animator = GetComponent<Animator>();
        spriteManager = GetComponent<SpriteManagement>();
    }

    void Update()
    {
        direction.x = input.RetrieveMoveInput();
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        if (direction.x < 0)
        {
            spriteManager.ShouldFlip(true);
        }
        else
        {
            spriteManager.ShouldFlip(false);
        }
        
        if (direction.x != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = rb.velocity;

        Acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = Acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        rb.velocity = velocity;
    }
}