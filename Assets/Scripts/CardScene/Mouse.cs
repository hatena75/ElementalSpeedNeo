using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Mouse : MonoBehaviourPunCallbacks //, IPunObservable
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public bool isDowned;

    private Judgement jm;
    private SEManager se;

    void Start()
    {
        isDowned = true;
        jm = GameObject.Find ("Master").GetComponent<Judgement>();
        se = GameObject.Find ("SEManager").GetComponent<SEManager>();
    }

    void OnMouseDown() {
        //enableでifを絡ませているのは、enabledをoffにしてもこれらが動くから(Unityの有効無効設定外？)
        if(this.enabled){
            this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
            isDowned = false;
            se.CardTakeSE();
        }
    }

    void OnMouseDrag() {
        if(this.enabled){
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
            transform.position = currentPosition;
        }
        
    }

    void OnMouseUp() {
        if(this.enabled){
            isDowned = true;
            se.CardPlaySE();
        }
    }

    //これは同期されない。恐らくisDownedが相手側では常にfalseになっている。
    //isDowndを共有しようと思ったがPhotonViewで追っている分には遅すぎて検知無理
    void OnTriggerStay2D(Collider2D coll){ //Colliderオブジェクト(カード)と衝突した瞬間1度呼ばれる。
        //Debug.Log("OnTriggerStay");
        if(isDowned && coll.gameObject.tag == "Field"){
            //Debug.Log("触ったよ");
            jm.Put(gameObject, coll.gameObject);
            isDowned = false;
        }
　　}
}
