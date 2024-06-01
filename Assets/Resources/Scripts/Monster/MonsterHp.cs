using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    [SerializeField]
    int maxHp = 3;

    int hp = 3;

    private void Start()
    {
        hp = maxHp;
    }

    public void GetDamage()
    {
        hp--;
        gameObject.GetComponent<MonsterState>().state = EMonsterState.Hit;
        if (hp <= 0 )
        {
            gameObject.GetComponent<MonsterState>().state = EMonsterState.Die;
        }
    }
}
