using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpBar : MonoBehaviour
{

    SpriteRenderer sRenderer;
    PlayerHp playerHp;

    [SerializeField]
    int maxHp = 5;

    int hp = 5;

    [SerializeField]
    Sprite[] hpSprites;

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        playerHp = transform.parent.gameObject.GetComponent<PlayerHp>();

    }

    void Update()
    {
        hp = playerHp.HP;
        sRenderer.sprite = hpSprites[hp];
    }
}
