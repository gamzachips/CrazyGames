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

    private uint bugFlag = 0x00000000; // ���� ��Ʈ �÷���

    public void AddBugFlag(BugState state)
    {
        bugFlag |= (1u << (int)state);  // state ��° ��Ʈ�� 1�� ����
    }

    public void RemoveBugFlag(BugState state)
    {
        bugFlag &= ~(1u << (int)state); // state ��° ��Ʈ�� 0���� ����
    }

    public bool CheckBugFlag(BugState state)
    {
        return (bugFlag & (1u << (int)state)) != 0; // state ��° ��Ʈ�� 1���� Ȯ��
    }
}
