using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillAttack : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    float duration = 1f;

    float timer = 0f;

    bool monsterHit = false;

    bool flipX = false;

    private void Start()
    {
        flipX = transform.parent.gameObject.GetComponent<SpriteRenderer>().flipX;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > duration)
        {
            Destroy(gameObject);
        }

        if(!monsterHit)
        {
            if(flipX)
            {
                transform.position += new Vector3(-speed * Time.deltaTime, 0f, 0f );
            }
            else
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") == false)
            return;

        monsterHit = true;

        MonsterHp monsterHp = collision.gameObject.GetComponent<MonsterHp>();
        monsterHp.GetDamage(2);
    }
}
