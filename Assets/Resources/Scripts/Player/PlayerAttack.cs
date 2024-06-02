using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    GameObject player;
    PlayerState playerState;
    Animator animator;
    BoxCollider2D attackCollider;
    SpriteRenderer sRenderer;

    GameObject targetMonster;
    public GameObject bugPlayer;

    //Attack
    [SerializeField]
    float attack1Time = 0.5f;
    float attack1Timer = 0f;

    //Hit
    [SerializeField]
    float hitTime = 0.7f;
    float hitTimer = 0f;

// Bug
    BugManager bugManager;
    Transform playerTransform;

    Vector2[] copyBugPos = new Vector2[] // �÷��̾�� �����÷��̾� ���� �Ÿ� : Vector2 �迭 �ʱ�ȭ
    {
            new Vector2(-80, 0),
            new Vector2(80, 0),
            new Vector2(-160, 0),
            new Vector2(160, 0)
    };

    [SerializeField]
    private int copyBugCount = 0;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState>();
        animator = player.GetComponent<Animator>();
        sRenderer = player.GetComponent<SpriteRenderer>();  
        attackCollider = GetComponent<BoxCollider2D>();

        attackCollider.size = new Vector2(2f, 1.5f);

        bugManager = FindObjectOfType<BugManager>();

        // �������� Ȯ���ؼ� ���� �߰�.
        if (SceneManager.GetActiveScene().name == "Stage1Scene")
        {
            bugManager.AddBugFlag(BugManager.BugState.PlayerCopyBug);
        }

    }

    void Update()
    {
        if (playerState.state == EPlayerState.Die)
        {
            animator.SetTrigger("Die");
            return;
        }

        //Hit ���� ó��
        if (playerState.state == EPlayerState.Hit)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            {
                animator.SetTrigger("Hit");
            }
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

        //�Ϲݰ��� ���� ���� 
        if (playerState.state == EPlayerState.Attack1
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

    void OnDestroy()
    {
        bugManager.RemoveBugFlag(BugManager.BugState.PlayerCopyBug);
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
                PlayerCopyBug();

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
                    targetMonster.GetComponent<MonsterHp>().GetDamage();
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
                    targetMonster.GetComponent<MonsterHp>().GetDamage();
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

    private void PlayerCopyBug()
    {
        if (bugManager == null || bugPlayer == null)
        {
            Debug.LogError("BugManager or bugPlayer is not set.");
            return;
        }

        if (bugManager.CheckBugFlag(BugManager.BugState.PlayerCopyBug) == false) { return; }
        if (copyBugCount >= 4) { return; }

        Vector2 copyPos = copyBugPos[copyBugCount];

        // �Ϲݰ��� �� �� ���� ����Player ������ �ϳ��� �߰�.
        GameObject newBugPlayer = Instantiate(bugPlayer, player.transform);
        newBugPlayer.transform.localPosition = new Vector3(copyBugPos[copyBugCount].x, copyBugPos[copyBugCount].y, 0f);

        copyBugCount++;
    }
}
