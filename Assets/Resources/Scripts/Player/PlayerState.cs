using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerState : MonoBehaviour
{
    
    public EPlayerState state = EPlayerState.Idle;

    //Die Check
    [SerializeField]
    float dieTime = 0.9f;

    float dieTimer = 0f;

    private void Update()
    {
        if (state == EPlayerState.Die)
        {
            dieTimer += Time.deltaTime;
            if (dieTimer > dieTime)
            {
                Destroy(gameObject);
            }
        }
    }
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