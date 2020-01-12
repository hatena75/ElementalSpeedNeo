using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


// 状態を定義しているだけの何もしないクラス
public partial class DefineStateMachinePvP : MonoBehaviour
{
    // この DefineStateMachinePvP クラスのステートマシン
    private ImtStateMachine<DefineStateMachinePvP> stateMachine;

    public void GameStart(){
        stateMachine.SendEvent((int)StateEventId.Start);
    }

    // この DefineStateMachinePvP クラスのアイドリング状態クラス
    private class IdleState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        // 何もしない状態クラスなら何も書かなくても良い（むしろ無駄なoverrideは避ける）
    }

    private class EndState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        protected internal override void Enter()
        {
            
        }
    }

    private void Start()
    {
        // ステートマシンを起動
        stateMachine.Update();

        //stateMachine.SendEvent((int)StateEventId.Start);
    }

    private void Update()
    {
        // ひたすら更新
        stateMachine.Update();
    }
}