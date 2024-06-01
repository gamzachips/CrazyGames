using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public EMonsterState state = EMonsterState.Idle;

    //Die Check
    [SerializeField]
    float dieTime = 0.7f;

    float dieTimer = 0f;

    private void Update()
    {
        if(state == EMonsterState.Die)
        {
            dieTimer += Time.deltaTime;
            if(dieTimer > dieTime)
            {
                Destroy(gameObject);
            }
        }
    }
}


public enum EMonsterState
{
    Idle,
    Chase,
    Return,
    Attack,
    Hit,
    Die
}