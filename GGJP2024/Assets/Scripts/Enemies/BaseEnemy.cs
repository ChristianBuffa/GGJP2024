using System;
using Unity.Mathematics;
using UnityEngine;

public enum EnemyState
{
    Stand,
    Patrol,
    Attack,
    Dash,
    Dead
}

public class BaseEnemy : MonoBehaviour, IAttack
{
    [SerializeField] private Bullet currentBullet;
    [SerializeField] private float detectionRange;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float detectionResetTime = 3f;
    [SerializeField] private int contactDamage;
    [SerializeField] private float maxStandTimer = 3f;
    [SerializeField] private float spawnBulletDistance = 2f;
    [SerializeField] private Transform[] patrolPoints;

    protected Transform playerTransform;
    protected EnemyState state;
    protected bool isStanding = false;

    private Rigidbody2D rb;
    private Collider2D col;
    private SpriteManagement spriteManager;
    private Animator animator;
    protected bool isAttacking = false;

    private int currentPatrolIndex = 0;
    private bool playerDetected = false;
    private float detectionTimer = 0f;
    private float standTimer;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteManager = GetComponent<SpriteManagement>();
        animator = GetComponent<Animator>();

        standTimer = maxStandTimer;
        state = EnemyState.Patrol;
    }

    private void Update()
    {
        if(state == EnemyState.Dead)
            return;
        
        StateAction();
        
        if(isStanding)
            return;
        
        GetPlayerPosition();

        CheckPlayerState();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            other.gameObject.GetComponent<HealthComponent>().TakeDamage(contactDamage);
            TransitionToState(EnemyState.Stand);
            isAttacking = false;
        }
    }

    private void CheckPlayerState()
    {
        float distance = GetPlayerDistance();

        if(playerDetected)
        {
            detectionTimer -= Time.deltaTime;
            state = EnemyState.Stand;
            
            if (detectionTimer <= 0f)
            {
                isStanding = true;
                playerDetected = false;
                detectionTimer = 0f; 
            }
        }
        else if (distance < detectionRange && !isStanding)
        {
            TransitionToState(EnemyState.Attack);
            detectionTimer = detectionResetTime; 
        }
        else
        {
            TransitionToState(EnemyState.Patrol);
        }
    }

    private void StateAction()
    {
        switch (state)
        {
            case EnemyState.Stand:
                Stand();
                break;
                
            case EnemyState.Patrol:
                Patrol();
                break;

            case EnemyState.Attack:
                Attack();
                break;
        }
    }

    private void Stand()
    {
        isStanding = true;
        standTimer -= Time.deltaTime;
        animator.SetBool("IsRunning", false);

        if (standTimer <= 0f)
        {
            standTimer = maxStandTimer;
            isStanding = false;
        }
    }

    
    private void Patrol()
    {
        animator.SetBool("IsRunning", true);
        
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned for the enemy.");
            return;
        }

        Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        if (targetPosition.x < transform.position.x)
        {
            spriteManager.ShouldFlip(true);
        }
        else
        {
            spriteManager.ShouldFlip(false);
        }
        
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
        
        playerDetected = true;
        
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (playerTransform.position.x > transform.position.x)
        {
            spawnPosition = transform.position + new Vector3(spawnBulletDistance, 0, 0);
            spawnRotation = Quaternion.identity;
        }
        else
        {
            spawnPosition = transform.position + new Vector3(-spawnBulletDistance, 0, 0);
            spawnRotation = Quaternion.Euler(0,0,180);
        }

        Bullet bullet = Instantiate(currentBullet, spawnPosition, spawnRotation);
        bullet.rb.velocity = Vector2.left * bullet.bulletSpeed;
    }

    public virtual void OnDeath()
    {
        animator.SetTrigger("Death");

        transform.position = new Vector3(0, 1, 0) + transform.position;
        
        state = EnemyState.Dead;

        col.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(this.gameObject, 5);
    }

    protected void TransitionToState(EnemyState newState)
    {
        state = newState;
    }

    private float GetPlayerDistance()
    {
        return Vector2.Distance(transform.position, playerTransform.position);
    }

    private void GetPlayerPosition()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }
}
