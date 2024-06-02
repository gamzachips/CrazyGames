using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    int maxHp = 3;
    [SerializeField]
    int hp = 3;

    Animator animator;

    private void Start()
    {
        hp = maxHp;
        animator = GetComponent<Animator>();
    }

    public void GetDamage()
    {
        hp--;
        
        if (hp <= 0)
        {
            gameObject.GetComponent<PlayerState>().state = EPlayerState.Die;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                animator.SetTrigger("Die");
            }
        }
        else
        {
            gameObject.GetComponent<PlayerState>().state = EPlayerState.Hit;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                animator.SetTrigger("Hit");
            }
        }
    }
}
