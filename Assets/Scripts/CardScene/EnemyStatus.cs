using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added
using Photon.Pun;

public class EnemyStatus : CommonPlayerStatus
{
    
    //public readonly int maxHP = 200;
    /*
    private int HP;
    private int damageSum;
    private Text textHP;
    private Slider barHP;
    private Text damageCount;
    private GameObject face;

    private GameObject effect;
    */


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        if(!PhotonNetwork.IsMasterClient){
            textHP = GameObject.Find ("PlayerHP").GetComponent<Text>();
            barHP = GameObject.Find("PlayerBar").GetComponent<Slider>();
            //damageCount = GameObject.Find ("DamageCount").GetComponent<Text>();
            face = GameObject.Find ("PlayerCharactor");
        }
        else
        {
            textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
            barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();
            //damageCount = GameObject.Find ("DamageCount").GetComponent<Text>();
            face = GameObject.Find ("EnemyCharactor");
        }
        //HP = maxHP;
        //damageSum = 0;
        //textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
        //barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();
        //damageCount = GameObject.Find ("DamageCount").GetComponent<Text>();
        //face = GameObject.Find ("EnemyCharactor");

        //プレハブをGameObject型で取得
        //effect = (GameObject)Resources.Load ("Prefabs/Hit_Fire");
        
    }

    /*
    public void Damage(int damage)
    {
        if(damage != 0){
            HP -= damage;
            //ダメージエフェクト
            StartCoroutine("DamageEffect");
        }
    }

    IEnumerator DamageEffect()
    {
        //エフェクト表示
        GameObject ef = Instantiate (effect, face.GetComponent<Renderer>().bounds.center, Quaternion.identity);

        //表情を変更
        face.GetComponent<Face>().ChangeFace(Faces.Lose);

        //2秒停止(エフェクト終了まで待つ)
        yield return new WaitForSeconds(2);

        //エフェクト削除
        Destroy(ef);
        //表情を変更
        face.GetComponent<Face>().ChangeFace(Faces.Normal);
    }

    public void DamagePlus(int damage)
    {
        damageSum += damage;
        damageCount.text = damageSum.ToString();//ここで表示されるのは相手からのダメージ
    }

    public void DamageCal()
    {
        Damage(damageSum);
        damageSum = 0;
        damageCount.text = damageSum.ToString();//ここで表示されるのは相手からのダメージ
    }
    */

    // Update is called once per frame
    void Update()
    {
        base.Update();
        //textHP.text = HP.ToString();
        //barHP.value = HP;
        
        /*
        if(HP <= 0)
        {
            GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
        }
        */
        
    }
}
