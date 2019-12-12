using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlay : MonoBehaviour
{
    private int level;
    public float timeOut;
    private float timeElapsed;
    private Judgement jm;
    private SEManager se;

    
    private GameObject[] moveFlg;

    // Start is called before the first frame update
    void Start()
    {
        jm = GameObject.Find ("Master").GetComponent<Judgement>();
        se = GameObject.Find ("SEManager").GetComponent<SEManager>();

        level = SceneManagerTitle.Level;
        timeOut = (float)level;
        if(timeOut == 0.0f){
            timeOut = 2.0f;
        }
        timeElapsed = 0.0f;
        moveFlg = null;
    }

    private bool MovingCard(GameObject hand, GameObject field)
    {
        Vector3 prePos = hand.transform.position;
        hand.transform.position = Vector3.Lerp(hand.transform.position, field.transform.position, 0.1f);
        
        //十分近づいたら置く処理
        if(Vector3.Distance(prePos, hand.transform.position) < 0.01f)
        {
            jm.Put(hand, field);
            se.CardPlaySE();
            return false;
        }
        else
        {
            return true;
        }
    }

    private GameObject[] Play()
    {
        // Enemyのカードを格納
        GameObject[] enemyHands = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject[] fields = GameObject.FindGameObjectsWithTag("Field");

        foreach (GameObject enemyHand in enemyHands) {
            foreach (GameObject field in fields) {
                //取りあえず置けるときに置く
                if(jm.PutAble(enemyHand, field))
                {
                    //どのカードをどの場に置くか決める
                    return new GameObject[] {enemyHand, field};
                }
            }
        }

        //置けるカードが無かった場合、手札を更新して終了
        foreach (GameObject enemyHand in enemyHands) {
            enemyHand.GetComponent<CardModel>().RandomFace();
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= timeOut)
        {
            moveFlg = Play();
            if(moveFlg != null){
                se.CardTakeSE();
            }

            timeElapsed = 0.0f;
        }

        if(moveFlg != null)
        {
            if(!MovingCard(moveFlg[0], moveFlg[1]))
            {
                moveFlg = null;
            }
        }

    }
}
