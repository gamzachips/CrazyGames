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

    Vector2[] copyBugPos = new Vector2[] // 플레이어와 버그플레이어 간의 거리 : Vector2 배열 초기화
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

        // 스테이지 확인해서 버그 추가.
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

        //Hit 상태 처리
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

        //일반공격 상태 해제 
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
        if (Input.GetMouseButtonDown(0))
        {
            if (playerState.state == EPlayerState.Idle
                || playerState.state == EPlayerState.Run)
            {
                PlayerCopyBug();

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
                    targetMonster.GetComponent<MonsterHp>().GetDamage();
                }
            }

        }
    }

    private void Attack2()
    {
        //왼쪽 마우스 클릭 - 추가 공격
        if (Input.GetMouseButtonDown(0))
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

        // 일반공격 할 때 마다 버그Player 프리팹 하나씩 추가.
        GameObject newBugPlayer = Instantiate(bugPlayer, player.transform);
        newBugPlayer.transform.localPosition = new Vector3(copyBugPos[copyBugCount].x, copyBugPos[copyBugCount].y, 0f);

        copyBugCount++;
    }
}
