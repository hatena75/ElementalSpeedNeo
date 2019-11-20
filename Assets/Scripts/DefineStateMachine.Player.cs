using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;

public partial class DefineStateMachine : MonoBehaviour
{
    private class MyPlayState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
        }
    }

    private class MyAttackState : ImtStateMachine<DefineStateMachine>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected internal override void Enter()
        {
        }

        // 状態の更新はこのUpdateで行う
        protected internal override void Update()
        {
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected internal override void Exit()
        {
        }
    }
}
