using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


// 状態を定義しているだけの何もしないクラス
public partial class DefineStateMachine : MonoBehaviour
{
    // この DefineStateMachine クラスのステートマシン
    private ImtStateMachine<DefineStateMachine> stateMachine;

    public void GameStart(){
        stateMachine.SendEvent((int)StateEventId.Start);
    }

    // この DefineStateMachine クラスのアイドリング状態クラス
    private class IdleState : ImtStateMachine<DefineStateMachine>.State
    {
        // 何もしない状態クラスなら何も書かなくても良い（むしろ無駄なoverrideは避ける）
        GameObject panel;

        protected internal override void Enter()
        {
            //探し出すためには最初にパネルがactiveである必要がある
            panel = GameObject.Find ("Panel");
        }
        protected internal override void Update()
        {
            if(!panel.activeSelf){
                //フェードイン終了まで待つ
                //探し出すためには最初にパネルがactiveである必要がある
                stateMachine.SendEvent((int)StateEventId.Start);
            }
        }
    }

    private class EndState : ImtStateMachine<DefineStateMachine>.State
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