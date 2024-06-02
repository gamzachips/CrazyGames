using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHp : MonoBehaviour
{
    [SerializeField]
    int maxHp = 3;

    int hp = 3;

    public int HP {  get { return hp; } set {  hp = value; } }

    private void Start()
    {
        hp = maxHp;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        gameObject.GetComponent<MonsterState>().state = EMonsterState.Hit;
        if (hp <= 0 )
        {
            hp = 0;
            gameObject.GetComponent<MonsterState>().state = EMonsterState.Die;
        }
    }
}
