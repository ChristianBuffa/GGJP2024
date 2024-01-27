using UnityEngine;

public class Player : MonoBehaviour
{
    public void OnDeath()
    {
        Debug.Log("player death");
        GetComponent<Move>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<Jump>().enabled = false;
    }
}
