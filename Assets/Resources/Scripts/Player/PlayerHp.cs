using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField]
    int maxHp = 3; 
    [SerializeField]
    int hp = 3;

    private void Start()
    {
        hp = maxHp;
    }

    public void GetDamage()
    {
        hp--;
        gameObject.GetComponent<PlayerState>().state = EPlayerState.Hit;
        if (hp <= 0)
        {
            gameObject.GetComponent<PlayerState>().state = EPlayerState.Die;
        }
    }
}
