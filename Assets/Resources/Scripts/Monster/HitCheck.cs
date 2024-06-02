using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheck : MonoBehaviour
{
    GameObject player;
    PlayerState playerState;

    [SerializeField]
    float damagedTime = 0.5f;
    float damagedTimer = 0f;

    bool isColliding = false;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState>();
    }

    private void Update()
    {
        damagedTimer += Time.deltaTime;
    }

    private void FixedUpdate()
    {
       if(isColliding)
        {
            if (playerState.state == EPlayerState.Attack1
                || playerState.state == EPlayerState.Attack2)
            {
                if (damagedTimer > damagedTime)
                {
                    GetComponentInChildren<MonsterHp>().GetDamage(1);
                    damagedTimer = 0f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player == null)
            return;

        if (collision.gameObject.CompareTag("PlayerAttack") == false)
            return;

        isColliding = true;   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player == null)
            return;

        if (collision.gameObject.CompareTag("PlayerAttack") == false)
            return;
        isColliding = false;
    }

}
