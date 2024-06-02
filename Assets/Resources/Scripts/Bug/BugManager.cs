using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugManager : MonoBehaviour
{
    public enum BugState
    {
        NONE,

        PlayerCopyBug,
        CrashBug,
        IMGCorruptionBug,

        CharChangeBug,
        InvertedControlBug,
        SoundChangeBug,

        RockMovingBug,

        PosChangeBug,
        ScaleChangeBug,

        EffectPersistBug,

        END
    }

    private uint bugFlag = 0x00000000; // 버그 비트 플래그

    public void AddBugFlag(BugState state)
    {
        bugFlag |= (1u << (int)state);  // state 번째 비트를 1로 설정
    }

    public void RemoveBugFlag(BugState state)
    {
        bugFlag &= ~(1u << (int)state); // state 번째 비트를 0으로 설정
    }

    public bool CheckBugFlag(BugState state)
    {
        return (bugFlag & (1u << (int)state)) != 0; // state 번째 비트가 1인지 확인
    }
}
