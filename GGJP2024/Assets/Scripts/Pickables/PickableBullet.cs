using UnityEngine;
using UnityEngine.Serialization;

public class PickableBullet : MonoBehaviour, IPickup
{
    [SerializeField] private BulletInfo bulletInfo;

    public void OnPickUp(BulletInfo info)
    {
        Shooting shooting = FindObjectOfType<Shooting>();
        shooting.myBullet.GetComponent<Bullet>().SetBulletValues(info);
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickUp(bulletInfo);
        }
    }
}
