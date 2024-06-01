using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterState : MonoBehaviour
{
    public EMonsterState state = EMonsterState.Idle;  
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