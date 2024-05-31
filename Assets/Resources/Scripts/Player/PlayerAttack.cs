using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject player;
    PlayerState playerState;
    Animator animator;
    BoxCollider2D attackCollider;
    SpriteRenderer sRenderer;

    GameObject targetMonster;

    [SerializeField]
    float attack1Time = 0.5f;


    float attack1Timer = 0f;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState>();
        animator = player.GetComponent<Animator>();
        sRenderer = player.GetComponent<SpriteRenderer>();  
        attackCollider = GetComponent<BoxCollider2D>();

        attackCollider.size = new Vector2(2f, 1.5f);
    }
   
    void Update()
    {
        transform.position = player.transform.position;

        MoveCollider();
        Attack1();
        Attack2();


        //일반공격 상태 해제 
        if(playerState.state == EPlayerState.Attack1
            || playerState.state == EPlayerState.Attack2)
        {
            attack1Timer += Time.deltaTime;
            if(attack1Timer > attack1Time)
            {
                attack1Timer = 0f;
                playerState.state = EPlayerState.Idle;
            }
        }
    }


    private void MoveCollider()
    {
        //마우스 방향에 따른 콜라이더 위치 변경
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < player.transform.position.x) //플레이어 왼쪽 클릭
        {
            attackCollider.offset = new Vector2(-0.6f, 0); //공격 콜라이더 오프셋
        }
        else //플레이어 오른쪽 클릭
        {
            attackCollider.offset = new Vector2(0.6f, 0); //공격 콜라이더 오프셋
        }
    }

    private void Attack1()
    {
        //왼쪽 마우스 클릭 - 기본 공격
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (playerState.state == EPlayerState.Idle
                || playerState.state == EPlayerState.Run)
            {
                animator.SetTrigger("Attack1");// 공격 애니메이션 재생
                playerState.state = EPlayerState.Attack1;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < player.transform.position.x) //플레이어 왼쪽 클릭
                {
                    sRenderer.flipX = true; //플레이어 반전
                }
                else //플레이어 오른쪽 클릭
                {
                    sRenderer.flipX = false;
                }

                if (targetMonster) //충돌중인 몬스터가 있으면
                {

                }
            }

        }
    }

    private void Attack2()
    {
        //왼쪽 마우스 클릭 - 추가 공격
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //추가 공격 타이밍 체크! 
            if (attack1Timer > attack1Time * 0.75 && attack1Timer < attack1Time)
            {
                animator.SetTrigger("Attack2");// 공격 애니메이션 재생
                playerState.state = EPlayerState.Attack2;
                attack1Timer = 0f;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < player.transform.position.x) //플레이어 왼쪽 클릭
                {
                    sRenderer.flipX = true; //플레이어 반전
                }
                else //플레이어 오른쪽 클릭
                {
                    sRenderer.flipX = false;
                }

                if (targetMonster) //충돌중인 몬스터가 있으면
                {

                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") == false)
            return;

        targetMonster = collision.gameObject;
    }
}
