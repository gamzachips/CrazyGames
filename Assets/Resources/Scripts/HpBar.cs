using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{

    SpriteRenderer sRenderer;
    PlayerHp playerHp;
    MonsterHp monsterHp;

    [SerializeField]
    int maxHp = 5;

    int hp = 5;

    [SerializeField]
    Sprite[] hpSprites;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        playerHp = transform.parent.gameObject.GetComponent<PlayerHp>();
        monsterHp = transform.parent.gameObject.GetComponent<MonsterHp>();
    }

    void Update()
    {
        if(playerHp)
            hp = playerHp.HP;
        else if (monsterHp)
            hp = monsterHp.HP;
        sRenderer.sprite = hpSprites[hp];
    }
}
