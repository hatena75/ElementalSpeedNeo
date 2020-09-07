using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IceMilkTea.Core;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public partial class DefineStateMachinePvP : MonoBehaviour
{
    private enum EEventType : byte
    {
        AttackEndSync = 1,
        ReadySync
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
        bool data;

        switch( eventCode )
        {
            case EEventType.AttackEndSync:
                //CustomDataから送られたデータを取り出し
                data = (bool)photonEvent.CustomData;
                attackEnd = data;
                Debug.Log("attackend");
                break;
            case EEventType.ReadySync:
                //CustomDataから送られたデータを取り出し
                data = (bool)photonEvent.CustomData;
                opponentReady = data;
                Debug.Log("ready");
                break;
            default:
                break;
        }
    }

    public static void AttackEndSync(bool flg)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.AttackEndSync, flg, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void ReadySync(bool flg)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.Others,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.ReadySync, flg, raiseEventOptions, SendOptions.SendReliable);
    }
    
}
