using System;
using IceMilkTea.Core;
using UnityEngine;

// 状態を定義しているだけの何もしないクラス
public partial class DefineStateMachine : MonoBehaviour
{
    // この DefineStateMachine クラスのステートマシン
    private ImtStateMachine<DefineStateMachine> stateMachine;

    // この DefineStateMachine クラスのアイドリング状態クラス
    private class IdleState : ImtStateMachine<DefineStateMachine>.State
    {
        // 何もしない状態クラスなら何も書かなくても良い（むしろ無駄なoverrideは避ける）
    }

    private void Start()
    {
        // ステートマシンを起動
        stateMachine.Update();
    }


    private void Update()
    {
        // ひたすら更新
        stateMachine.Update();
    }
}