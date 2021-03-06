﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public partial class SMNew : MonoBehaviour
{

    private enum EEventType : byte
    {
        SendMyHands = 1,
        SendFields,
        PlayCard,
        UseReload,
        UseSkill,
        PlayEnd,
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
                //CustomDataから送られたデータを取り出し
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
                int[] data = (int[])photonEvent.CustomData;
                opponentPlay.PlayCard_Enqueue(data[0], data[1], data[2]);
                break;
            case EEventType.UseReload:
                int[] reloadcontent = (int[])photonEvent.CustomData;
                opponentPlay.UseReload_Enqueue(reloadcontent);
                break;
            case EEventType.UseSkill:
                int[] skillcontent = (int[])photonEvent.CustomData;
                opponentPlay.UseSkill_Enqueue(skillcontent);
                break;
            case EEventType.PlayEnd:
                opponentPlay.PlayEnd_Enqueue( () => { stateMachine.SendEvent((int)StateEventId.OpponentPlayEnd); } );
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

    public static void PlayCard(int pos_hand, int pos_field, int new_index_hand)
    {
        int[] content = new int[] {pos_hand, pos_field, new_index_hand};

        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.PlayCard, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void UseReload()
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
        PhotonNetwork.RaiseEvent( (byte)EEventType.UseReload, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void UseSkill(int[] data)
    {
        int[] content = data; //キャラによってdataの使い方は異なる

        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.UseSkill, content, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void PlayEnd()
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.PlayEnd, null, raiseEventOptions, SendOptions.SendReliable);
    }
    
}
