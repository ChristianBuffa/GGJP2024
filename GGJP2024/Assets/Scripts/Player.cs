using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDeath()
    {
        animator.SetTrigger("Death");
        Debug.Log("player death");
        GetComponent<Move>().enabled = false;
        GetComponent<Shooting>().enabled = false;
        GetComponent<Jump>().enabled = false;
    }
}
