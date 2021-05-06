﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public partial class SMNew : MonoBehaviour
{
    //同期は全てイベントの発生で行う？
    //最初に自分の手札情報を相手に送る。マスターならフィールド情報も送る。(後のプレイカード記録のために配列として送る)
    //カードプレイ情報を送る。手札とフィールドの配列要素のみ？計算は相手に任せる？


    private enum EEventType : byte
    {
        SendMyHands = 1,
        SendFields,
        PlayCard,
        UseSkill,
        ChangeHands,
        TurnEnd,
    }

    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void OnEvent(EventData photonEvent)
    {
        var eventCode = (EEventType)photonEvent.Code;

        switch( eventCode )
        {
            case EEventType.SendMyHands:
                int[] enemycontent = (int[])photonEvent.CustomData;
                for(int i = 0; i < enemycontent.Length; i++){
                    cardInfo.enemyHands[i+1].GetComponent<CardModel>().ChangeFace(enemycontent[i]);
                }
                break;
            case EEventType.SendFields:
                int[] fieldcontent = (int[])photonEvent.CustomData;
                for(int i = 0; i < fieldcontent.Length; i++){
                    cardInfo.fields[i+1].GetComponent<CardModel>().ChangeFace(fieldcontent[i]);
                }
                break;
            case EEventType.PlayCard:
                //CustomDataから送られたデータを取り出し
                int[] data = (int[])photonEvent.CustomData;
                int pos_hand = data[0];
                int pos_field = data[1];
                Debug.Log("attackend");
                break;
            case EEventType.UseSkill:
                //CustomDataから送られたデータを取り出し
                Debug.Log("ready");
                break;
            case EEventType.TurnEnd:
                stateMachine.SendEvent((int)StateEventId.OpponentTurnEnd);
                break;
            default:
                break;
        }
    }

    public static void SendMyHands()
    {
        int[] content = new int[3];

        for(int i = 1; i <= 3; i++){
            content[i-1] = cardInfo.myHands[i].GetComponent<CardModel>().cardIndex;
        }

        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.SendMyHands, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void SendFields()
    {
        int[] content = new int[2];

        for(int i = 1; i <= 2; i++){
            content[i-1] = cardInfo.fields[i].GetComponent<CardModel>().cardIndex;
        }

        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.SendFields, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void PlayCard(int pos_hand, int pos_field)
    {
        int[] content = new int[] { pos_hand, pos_field};

        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.PlayCard, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void UseSkill(bool flg)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.UseSkill, flg, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void TurnEnd()
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.TurnEnd, null, raiseEventOptions, SendOptions.SendReliable);
    }
    
}