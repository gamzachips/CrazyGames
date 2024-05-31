using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 1.0f;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 moveDir = Vector2.zero;

        if(Input.GetKey(KeyCode.W))
        {
            moveDir.y += 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            moveDir.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir.x += 1;
        }

        moveDir = moveDir.normalized;
        animator.SetFloat("Xspeed", moveDir.x); 
        if(moveDir == Vector2.zero )
        {
            animator.SetTrigger("Stop");
        }

        transform.Translate(moveDir * moveSpeed * Time.deltaTime); 
    }
}
