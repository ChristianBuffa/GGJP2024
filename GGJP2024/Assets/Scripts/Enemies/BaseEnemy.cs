using Unity.Mathematics;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public enum EnemyState
    {
        Stand,
        Patrol,
        Attack,
        Dash,
        Dead
    }

    [SerializeField] private Bullet currentBullet;
    [SerializeField] private float detectionRange;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float detectionResetTime = 3f; 
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

    private void Attack()
    {
        playerDetected = true;

        Vector3 spawnPosition = transform.position + new Vector3(-1, 0, 0);
        Quaternion spawnRotation = Quaternion.Euler(0,0,180);

        Bullet bullet = Instantiate(currentBullet, spawnPosition, spawnRotation);
        bullet.rb.velocity = Vector2.left * bullet.bulletSpeed;
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