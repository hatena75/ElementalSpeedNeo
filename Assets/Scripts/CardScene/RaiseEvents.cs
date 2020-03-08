using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public enum EEventType : byte
{
    Hello = 1,
    PutSync,
    CardSync,
}

public class RaiseEvents : MonoBehaviour
{
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
            case EEventType.Hello:
                //CustomDataから送られたデータを取り出し
                string data = (string)photonEvent.CustomData;
                Debug.Log(data);
                break;
            /*
            case EEventType.PutSync:
                object[] content = (object[])photonEvent.CustomData;
                ((GameObject)content[0]).GetComponent<CardModel>().ChangeFace((int)content[1]);
                ((GameObject)content[2]).GetComponent<CardModel>().ChangeFace((int)content[3]);
                break;
            */
            /*
            case EEventType.CardSync:
                object[] content = (object[])photonEvent.CustomData;
                ((GameObject)content[0]).GetComponent<CardModel>().ChangeFaceSync((int)content[1]);
                break;
            */
            default:
                break;
        }
    }

    public void Hello()
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.Hello, "Hello!", raiseEventOptions, SendOptions.SendReliable);
    }

    public void PutSync(object[] cnt){
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.PutSync, cnt, raiseEventOptions, SendOptions.SendReliable);
    }

    public void CardSync(object[] cnt){
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent( (byte)EEventType.CardSync, cnt, raiseEventOptions, SendOptions.SendReliable);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
