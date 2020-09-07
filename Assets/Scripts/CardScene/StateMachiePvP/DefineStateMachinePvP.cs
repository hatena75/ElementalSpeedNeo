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

    //同期させる変数
    public static bool attackEnd = false;
    public static bool opponentReady = false;

    public void GameStart(){
        stateMachine.SendEvent((int)StateEventId.Start);
    }

    //手札を動かせるようにしたり動かせないようにしたりする関数
    public static void CanPlayHand(GameObject[] hands, HandResetButton reload ,bool flg){

        string who = hands[0].tag;

        void TrueChange(string str){
            if(who.Equals(str)){
                foreach(GameObject hand in hands){
                    hand.GetComponent<Mouse>().enabled = true;
                    hand.GetComponent<CardModel>().MovableColor();
                }

                reload.Activate();
            }
            else{
                foreach(GameObject hand in hands){
                    hand.GetComponent<CardModel>().MovableColor();
                }
            }
        }

        if(flg){
            //自分のターンに自分のCardを動かせるようにする
            if(PhotonNetwork.IsMasterClient){
                TrueChange("Player");
            }
            else{
                TrueChange("Player2");
            }
        }
        else{
            //散らかったカードを戻し、Cardを動かせないようにする
            foreach (GameObject hand in hands) {
                hand.GetComponent<CardModel>().ResetPos();
                hand.GetComponent<CardModel>().UnMovableColor();
                hand.GetComponent<Mouse>().enabled = false;
            }

            reload.DeActivate();
        }
    }

    // この DefineStateMachinePvP クラスのアイドリング状態クラス
    private class InitialState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        GameObject panel;
        private BanaFunction bf = GameObject.Find("bana").GetComponent<BanaFunction>();
        private NetworkMethods nm = GameObject.Find("Master").GetComponent<NetworkMethods>();
        protected internal override void Enter()
        {
            PhotonNetwork.IsMessageQueueRunning = true;
            //オンラインかオフラインか
            if(PhotonNetwork.OfflineMode = !SceneManagerTitle.IsVs){
                //オフラインならRPC利用のために1人用ルーム(offlineという部屋名)に入る
                nm.JoinOfflineRoom();
            }
            //マスタークライアントがフィールドを含む全てのインスタンスを生成
            nm.InitialPlacement();

            //探し出すためには最初にパネルがactiveである必要がある
            panel = GameObject.Find ("Panel");

        }
        protected internal override void Update()
        {
            if(!panel.activeSelf){
                bf.Animation();
                //フェードイン終了まで待つ
                //探し出すためには最初にパネルがactiveである必要がある
                stateMachine.SendEvent((int)StateEventId.Start);
            }
        }
    }

    //完全にパネルがなくなった後の状態
    private class StandByState : ImtStateMachine<DefineStateMachinePvP>.State
    {
        protected internal override void Enter()
        {
            //相手にこの状態に入ったことを通知

        }

        protected internal override void Update()
        {
            //相手がこの状態に入るまで待つ(遅延軽減)
            //入ってきたら対戦開始
            if(opponentReady){

            }
        }

        protected internal override void Exit()
        {
            //banaアニメーション再生
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
    }

    private void Update()
    {
        // ひたすら更新
        stateMachine.Update();
    }
}