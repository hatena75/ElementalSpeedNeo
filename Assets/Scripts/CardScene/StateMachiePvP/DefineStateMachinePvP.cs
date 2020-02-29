using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;


// 状態を定義しているだけの何もしないクラス
public partial class DefineStateMachinePvP : MonoBehaviour
{
    // この DefineStateMachinePvP クラスのステートマシン
    private ImtStateMachine<DefineStateMachinePvP> stateMachine;

    public void GameStart(){
        stateMachine.SendEvent((int)StateEventId.Start);
    }

    // この DefineStateMachinePvP クラスのアイドリング状態クラス
    private class InitialState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        GameObject panel;
        private NetworkMethods nm = GameObject.Find("Master").GetComponent<NetworkMethods>();

        protected internal override void Enter()
        {
            PhotonNetwork.IsMessageQueueRunning = true;
            //マスタークライアントがフィールドを含む全てのインスタンスを生成
            nm.InitialPlacement();

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