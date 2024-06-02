using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackCheck : MonoBehaviour
{
    GameObject monster;
    GameObject player;
    MonsterState monsterState;
    PlayerState playerState;

    BoxCollider2D attackCollider;

    [SerializeField]
    float damageTime = 0.5f;

    float damageTimer = 0f;

    bool isColliding = false;

    void Start()
    {
        monster = transform.parent.gameObject;
        player = GameObject.Find("Player");
        monsterState = monster.GetComponent<MonsterState>();
        playerState = player.GetComponent<PlayerState>();
        attackCollider = GetComponent<BoxCollider2D>(); 
    }

    private void Update()
    {
        MoveCollider();
    }

    private void MoveCollider()
    {
        //�÷��̾� ���⿡ ���� �ݶ��̴� ��ġ ����
        if (monster.transform.position.x > player.transform.position.x) //�÷��̾��� ����
        {
            attackCollider.offset = new Vector2(-0.6f, 0); //���� �ݶ��̴� ������
        }
        else //�÷��̾��� ������ 
        {
            attackCollider.offset = new Vector2(0.6f, 0); //���� �ݶ��̴� ������
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        isColliding = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isColliding = false;
    }

    void FixedUpdate()
    {
        if (isColliding == false)
            return;

        if (monsterState.state == EMonsterState.Attack)
        {
            if (playerState.state == EPlayerState.Hit
                || playerState.state == EPlayerState.Die)
                return;

            damageTimer += Time.deltaTime;
            if (damageTimer < damageTime) return;
            damageTimer = 0f;

            PlayerHp playerHp = player.GetComponent<PlayerHp>();

            playerHp.GetDamage();
        }
    }

}
