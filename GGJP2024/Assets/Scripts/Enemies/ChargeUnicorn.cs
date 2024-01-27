using UnityEngine;

public class ChargeUnicorn : BaseEnemy
{
    [SerializeField] private Bullet currentBullet;
    [SerializeField] private float detectionRange;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float detectionResetTime = 3f;
    [SerializeField] private float maxChargeRange;
    [SerializeField] private Transform[] patrolPoints;

    private Transform playerTransform;
    private EnemyState state;

    private int currentPatrolIndex = 0;
    private bool playerDetected = false;
    private float detectionTimer = 0f;

    private void Start()
    {
        state = EnemyState.Patrol;
    }

    private void Update()
    {
        GetPlayerPosition();

        CheckPlayerState();

        StateAction();
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
                playerDetected = false;
                detectionTimer = 0f; 
            }
        }
        else if (distance < detectionRange)
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
        //stand around
    }
    
    private void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.LogError("No patrol points assigned for the enemy.");
            return;
        }

        Vector3 targetPosition = patrolPoints[currentPatrolIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    public void Attack()
    {
        Vector3 targetPosition = FindObjectOfType<Shooting>().transform.position;

        if (Vector3.Distance(transform.position, targetPosition) > maxChargeRange)
        {
            targetPosition.x = -maxChargeRange;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, chargeSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            state = EnemyState.Patrol;
        }
    }

    private void TransitionToState(EnemyState newState)
    {
        state = newState;
    }

    private float GetPlayerDistance()
    {
        return Vector2.Distance(transform.position, playerTransform.position);
    }

    private void GetPlayerPosition()
    {
        playerTransform = FindObjectOfType<Shooting>().transform;
    }
}
