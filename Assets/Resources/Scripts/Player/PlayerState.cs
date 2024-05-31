using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerState : MonoBehaviour
{
    
    public EPlayerState state = EPlayerState.Idle;
}

public enum EPlayerState
{
    Idle,
    Run,
    Attack1,
    Attack2,
    Skill,
    Hit,
    Die
}