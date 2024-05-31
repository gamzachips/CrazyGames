using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }
   
    void Update()
    {
        //왼쪽 마우스 - 기본 공격
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack1");
        }
    }
}
