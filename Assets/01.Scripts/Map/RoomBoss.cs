using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보스 방만 되는겅
public abstract class RoomBoss : RoomBase
{
    protected override void Awake()
    {
    }

    public sealed override void SetDoor(bool active)
    {
        // 보스방은 문을 수정할 수 없다ㅏㅏㅏ
    }

    public sealed override void RoomEnter()
    {
        // 보스방은 이미 들어와있잖음;;
    }

    public sealed override void RoomLeave()
    {
    }

    public override void OnComplete()
    {
        // 상속되어있는 코드는 실행 안함 ㅅㄱ
    }
}
