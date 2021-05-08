using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HandResetButton : MonoBehaviour
{
    private bool usable;
    public Button btn;
    private SEManager se;
    private CardInfo cardInfo;

    public void Activate(){
        usable = true;
        btn.interactable = usable;
    }

    public void DeActivate(){
        usable = false;
        btn.interactable = usable;
    }

    public void Reload(){
        se.ReloadSE();
        GameObject[] playerHands = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject playerHand in playerHands) {
            playerHand.GetComponent<CardModel>().RandomFace();
        }
    }

    public void Reload(int[] indexes){
        se.ReloadSE();
        for(int i = 0; i < indexes.Length; i++){
            cardInfo.enemyHands[i+1].GetComponent<CardModel>().ReloadSync(indexes[i]);
        }
    }

    public void OnClick() {
        if(usable){
            
            Reload();

            if(!PhotonNetwork.OfflineMode){
                //同期処理
                SMNew.UseReload();
            }

            DeActivate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //btn = gameObject.GetComponent<Button>();
        se = GameObject.Find ("SEManager").GetComponent<SEManager>();
        cardInfo = GameObject.Find("Master").GetComponent<CardInfo>();
        DeActivate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
