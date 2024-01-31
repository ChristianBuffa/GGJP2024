using UnityEngine;

public class PickableArmor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!other.GetComponent<HealthComponent>().hasShield)
            {
                other.GetComponent<HealthComponent>().ShieldPickUp();
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
