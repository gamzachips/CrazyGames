using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class MonsterMoveAndAttack : MonoBehaviour
{
    [SerializeField]
    bool isFirstAttack = false;

    Animator animator;
    SpriteRenderer sRenderer;
    MonsterState monsterState;
    GameObject player;
    bool isColliding = false;

    [SerializeField]
    float moveSpeed = 1f;

    //Positions
    [SerializeField]
    float chaseRange = 0.1f;
    [SerializeField]
    float returnRange = 0.1f;
    [SerializeField]
    float attackRange = 0.1f;
    [SerializeField]
    float spawnPosRange = 0.1f;
    Vector3 spawnPos;

    //Hit
    [SerializeField] 
    float hitTime = 0.5f;
    float hitTimer = 0f;
    bool hit = false;

    //Attack
    [SerializeField]
    float attackCoolTime = 2.0f;
    float attackCoolTimer = 0f;
    [SerializeField]
    float attackTime = 0.5f;
    float attackTimer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        monsterState = GetComponent<MonsterState>();
        player = GameObject.Find("Player");
        spawnPos = transform.position;
    }

    void Update()
    {
        SetFlip();

        if (monsterState.state == EMonsterState.Die)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Die"))
            {
                animator.SetTrigger("Die");
            }
            return;
        }
        
        //Hit ���� ó��
        if (monsterState.state == EMonsterState.Hit)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                animator.SetTrigger("Hit");
            }
            hit = true;
            hitTimer += Time.deltaTime;
            if (hitTimer > hitTime)
            {
                monsterState.state = EMonsterState.Idle;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    animator.SetTrigger("Idle");
                }
                hitTimer = 0f;
            }
            return;
        }
        
        //������ġ�� ������ Idle ���·� ����
        if(Vector3.Distance(transform.position, spawnPos) < spawnPosRange)
        {
            monsterState.state = EMonsterState.Idle;
        }


        //Attack
        if(monsterState.state != EMonsterState.Attack)
        {
            //��Ÿ���� ������ ��
            if(attackCoolTimer > attackCoolTime)
            {
                //�÷��̾ ���� ���� ���� ������
                if(Vector3.Distance(transform.position, player.transform.position) < attackRange)
                {
                    //�����Ѵ�
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    {
                        animator.SetTrigger("Attack");
                    }
                    monsterState.state = EMonsterState.Attack;
                }
                else //���� ���� ���� ������ 
                {
                    //�߰� ���� ���� ���� ��
                    if(Vector3.Distance(transform.position, player.transform.position) < chaseRange)
                    {
                        if(isColliding) //�̹� �浹 ���̶��
                        {
                            //�Ѿư��� �־��ٸ� �� �Ѿư��� �ʰ� �����.
                            if(monsterState.state == EMonsterState.Chase)
                            {
                                monsterState.state = EMonsterState.Idle;
                            }
                        }
                        else //�÷��̾�� �浹 ���� �ƴ϶��
                        {
                            //���� �����̰ų�, �񼱰� �����ε� �¾����� �Ѿư���. 
                            if(isFirstAttack || (!isFirstAttack && hit)) 
                            {
                                if(monsterState.state == EMonsterState.Idle)
                                {
                                    monsterState.state = EMonsterState.Chase;
                                }
                            }
                        }
                    }
                    //�߰� ���� ���� ���� ��
                    else if(Vector3.Distance(transform.position, spawnPos) > spawnPosRange) //���� ��ġ�� ������ ���ư���.
                    {
                        monsterState.state = EMonsterState.Return;
                    }

                    //���� ������ �Ѿ���� ���ư���.
                    if(Vector3.Distance(transform.position, spawnPos) > returnRange)
                    {
                        monsterState.state = EMonsterState.Return;
                    }

                    //���¿� ���� �����δ�.
                    Move();
                }
            }
        }


        //Attack Cool Time
        attackCoolTimer += Time.deltaTime;

        //Attack Time üũ 
        if(monsterState.state == EMonsterState.Attack)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer > attackTime)
            {
                attackTimer = 0f;
                monsterState.state = EMonsterState.Idle;
                animator.SetTrigger("Idle");
                attackCoolTimer = 0f;
            }
        }
    }


    private void Move()
    {
        if(monsterState.state == EMonsterState.Return)
        {
            Vector3 moveDir = (spawnPos - transform.position).normalized;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }

        if(monsterState.state == EMonsterState.Chase)
        {
            Vector3 moveDir = (player.transform.position - transform.position).normalized;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        }
    }

    private void SetFlip()
    {
       
        if (monsterState.state == EMonsterState.Chase
            || monsterState.state == EMonsterState.Attack)
        {
            if(transform.position.x > player.transform.position.x)
            {
                sRenderer.flipX = true;
            }
            else
            {
                sRenderer.flipX = false;
            }
        }
        else if (monsterState.state == EMonsterState.Return)
        {
            if (transform.position.x > spawnPos.x)
            {
                sRenderer.flipX = true;
            }
            else
            {
                sRenderer.flipX = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            isColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            isColliding = false;
        }
    }
}
