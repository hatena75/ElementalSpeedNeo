using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    public bool isDowned;

    private Judgement jm;

    void Start()
    {
        isDowned = true;
        jm = GameObject.Find ("Master").GetComponent<Judgement>();
    }

    void OnMouseDown() {
        this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDowned = false;
    }

    void OnMouseDrag() {
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
        transform.position = currentPosition;
    }

    void OnMouseUp() {
        isDowned = true;
    }

    void OnTriggerStay2D(Collider2D coll){ //Colliderオブジェクト(カード)と衝突した瞬間1度呼ばれる。
        if(isDowned && coll.gameObject.tag == "Field"){
            //Debug.Log("触ったよ");
            isDowned = false;
            jm.Put(gameObject, coll.gameObject);
        }
　　}

}
