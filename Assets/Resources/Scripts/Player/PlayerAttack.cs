using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    GameObject skillPrefab;

    //Attack
    [SerializeField]
    float attack1Time = 0.5f;
    float attack1Timer = 0f;

    //Hit
    [SerializeField]
    float hitTime = 0.3f;
    float hitTimer = 0f;

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
        if (playerState.state == EPlayerState.Die)
        {
            return;
        }
        //Hit ���� ó��
        if (playerState.state == EPlayerState.Hit)
        {
            hitTimer += Time.deltaTime;
            if (hitTimer > hitTime)
            {
                playerState.state = EPlayerState.Idle;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.SetTrigger("Idle");
                }
                hitTimer = 0f;
            }
            return;
        }

        transform.position = player.transform.position;

        MoveCollider();
        Attack1();
        Attack2();
        Skill();

        //�Ϲݰ��� ���� ���� 
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
        //���콺 ���⿡ ���� �ݶ��̴� ��ġ ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (mousePos.x < player.transform.position.x) //�÷��̾� ���� Ŭ��
        {
            attackCollider.offset = new Vector2(-0.6f, 0); //���� �ݶ��̴� ������
        }
        else //�÷��̾� ������ Ŭ��
        {
            attackCollider.offset = new Vector2(0.6f, 0); //���� �ݶ��̴� ������
        }
    }

    private void Attack1()
    {
        //���� ���콺 Ŭ�� - �⺻ ����
        if (Input.GetMouseButtonDown(0))
        {
            if (playerState.state == EPlayerState.Idle
                || playerState.state == EPlayerState.Run)
            {
                animator.SetTrigger("Attack1");// ���� �ִϸ��̼� ���
                playerState.state = EPlayerState.Attack1;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < player.transform.position.x) //�÷��̾� ���� Ŭ��
                {
                    sRenderer.flipX = true; //�÷��̾� ����
                }
                else //�÷��̾� ������ Ŭ��
                {
                    sRenderer.flipX = false;
                }

                if (targetMonster) //�浹���� ���Ͱ� ������
                {
                    targetMonster.GetComponent<MonsterHp>().GetDamage(1);
                }
            }

        }
    }

    private void Attack2()
    {
        //���� ���콺 Ŭ�� - �߰� ����
        if (Input.GetMouseButtonDown(0))
        {
            //�߰� ���� Ÿ�̹� üũ! 
            if (attack1Timer > attack1Time * 0.75 && attack1Timer < attack1Time)
            {
                animator.SetTrigger("Attack2");// ���� �ִϸ��̼� ���
                playerState.state = EPlayerState.Attack2;
                attack1Timer = 0f;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < player.transform.position.x) //�÷��̾� ���� Ŭ��
                {
                    sRenderer.flipX = true; //�÷��̾� ����
                }
                else //�÷��̾� ������ Ŭ��
                {
                    sRenderer.flipX = false;
                }

                if (targetMonster) //�浹���� ���Ͱ� ������
                {
                    targetMonster.GetComponent<MonsterHp>().GetDamage(1);
                }
            }
        }
    }

    private void Skill()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (playerState.state == EPlayerState.Idle
                || playerState.state == EPlayerState.Run)
            {
                animator.SetTrigger("Skill");// ���� �ִϸ��̼� ���
                playerState.state = EPlayerState.Skill;

                GameObject skillObject = Instantiate(skillPrefab);
                skillObject.transform.SetParent(transform.parent);
                SpriteRenderer skillRenderer = skillObject.GetComponent<SpriteRenderer>();

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (mousePos.x < player.transform.position.x) //�÷��̾� ���� Ŭ��
                {
                    sRenderer.flipX = true; //�÷��̾� ����
                    skillObject.transform.position = transform.position + new Vector3(-0.5f, 0.5f);
                    skillRenderer.flipX = true;
                }
                else //�÷��̾� ������ Ŭ��
                {
                    sRenderer.flipX = false;
                    skillObject.transform.position = transform.position + new Vector3(0.5f, 0.5f);
                    skillRenderer.flipX = false;
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
