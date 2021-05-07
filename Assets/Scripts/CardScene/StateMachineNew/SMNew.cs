using System;
using IceMilkTea.Core;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public partial class SMNew : MonoBehaviour
{
    private ImtStateMachine<SMNew> stateMachine;

    private static CardInfo cardInfo;
    private static OpponentPlay opponentPlay;

    //自分・相手のターンが回る度に1カウント増える
    private static int turnCount = 0;

    public void GameStart(){
        stateMachine.SendEvent((int)StateEventId.StandBy);
    }

    //手札を動かせるようにしたり動かせないようにしたりする関数
    public static void CanPlayHand(GameObject[] hands, HandResetButton reload, SkillButton skill, bool flg){

        string who = hands[0].tag;

        void TrueChange(string str){
            if(who.Equals(str)){
                foreach(GameObject hand in hands){
                    hand.GetComponent<Mouse>().enabled = true;
                    hand.GetComponent<CardModel>().MovableColor();
                }

                reload.Activate();
                skill.Activate();
            }
            else{
                foreach(GameObject hand in hands){
                    hand.GetComponent<CardModel>().MovableColor();
                }
            }
        }

        if(flg){
            //自分のターンに自分のCardを動かせるようにする
            //if(PhotonNetwork.IsMasterClient){
                TrueChange("Player");
            //}
            //else{
                //TrueChange("Player2");
            //}
        }
        else{
            //散らかったカードを戻し、Cardを動かせないようにする
            foreach (GameObject hand in hands) {
                hand.GetComponent<CardModel>().ResetPos();
                hand.GetComponent<CardModel>().UnMovableColor();
                hand.GetComponent<Mouse>().enabled = false;
            }

            reload.DeActivate();
            skill.DeActivate();
        }
    }
    private class InitialState : ImtStateMachine<SMNew>.State
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

            //初期化処理
            GameObject.Find("Skill").GetComponent<SkillButton>().UseCount = PhotonNetwork.IsMasterClient ? 1 : 2;
            turnCount = 0;
        }
        protected internal override void Update()
        {
            //フェードイン終了まで待つ
            if(!panel.activeSelf){
                stateMachine.SendEvent((int)StateEventId.StandBy);
            }
        }

        protected internal override void Exit()
        {
            //アニメーション再生
            bf.Animation();
        }
        
    }

    //お互いに完全にパネルがなくなった後の状態
    private class StandByState : ImtStateMachine<SMNew>.State
    {
        private BanaFunction bf = GameObject.Find("bana").GetComponent<BanaFunction>();
        private BgmManager bm = GameObject.Find("Master").GetComponent<BgmManager>();

        protected internal override void Enter()
        {
            //カードの同期処理
            if(!PhotonNetwork.OfflineMode){
                SendMyHands();
                if(PhotonNetwork.IsMasterClient){
                    SendFields();
                }
            }
        }

        protected internal override void Update()
        {
            //アニメーション終了時に遷移
            if(bf.animationEnd){
                //先攻後攻処理
                if(PhotonNetwork.IsMasterClient){
                    stateMachine.SendEvent((int)StateEventId.GameStart_P1);
                }
                else{
                    stateMachine.SendEvent((int)StateEventId.GameStart_P2);
                }
            }
        }

        protected internal override void Exit()
        {
            bm.BgmPlay();
        }
    }

    private class EndState : ImtStateMachine<SMNew>.State
    {
        protected internal override void Enter()
        {
            
        }
    }

    private void Start()
    {
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        opponentPlay = GameObject.Find("Enemy").GetComponent<OpponentPlay>();
        stateMachine.Update();
    }

    private void Update()
    {
        stateMachine.Update();
    }
}