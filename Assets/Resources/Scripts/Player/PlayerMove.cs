using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [SerializeField]
    float moveSpeed = 1.0f;

    [SerializeField]
    BoxCollider2D attackCollider;

    Animator animator;
    SpriteRenderer sRenderer;
    PlayerState playerState;
    Rigidbody2D rigid;

    private void Start()
    {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        playerState = GetComponent<PlayerState>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (playerState.state == EPlayerState.Attack1 || playerState.state == EPlayerState.Attack2
            || playerState.state == EPlayerState.Hit || playerState.state == EPlayerState.Die )
            return;

        Vector3 moveDir = Vector3.zero;

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

        if(moveDir == Vector3.zero )
        {
            animator.SetTrigger("Idle");
            playerState.state = EPlayerState.Idle;
        }
        else
        {
            animator.SetTrigger("Move");
            playerState.state = EPlayerState.Run;
            if (moveDir.x < 0) //왼쪽을 향하는 경우
            {
                sRenderer.flipX = true; //플레이어 반전
            }
            else if (moveDir.x > 0)
            {
                sRenderer.flipX = false;
            }
        }

        rigid.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    }
}
