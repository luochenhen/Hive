using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPlatForm : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Fan_Run_Animation");      
        }
    }

}
