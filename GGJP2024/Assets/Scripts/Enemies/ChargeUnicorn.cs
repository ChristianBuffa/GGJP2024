using UnityEngine;

public class ChargeUnicorn : BaseEnemy
{
    [SerializeField] private float chargeRange;
    [SerializeField] private float chargeSpeed;

    private Vector3 chargeStartPosition;
    private bool charging = false;

    public override void Attack()
    {
        if (!charging)
        {
            animator.SetTrigger("Attack");
            
            charging = true;
            chargeStartPosition = transform.position;

            Vector3 chargePosition;

            if (playerTransform.position.x < transform.position.x)
            {
                chargePosition = transform.position + Vector3.left * chargeRange;
            }
            else
            {
                chargePosition = transform.position + Vector3.right * chargeRange;
            }

            StartCoroutine(ChargeTowardsPosition(chargePosition));
        }
    }

    private System.Collections.IEnumerator ChargeTowardsPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chargeSpeed * Time.deltaTime);
            yield return null;
        }

        charging = false;

        TransitionToState(EnemyState.Stand);
    }
}
