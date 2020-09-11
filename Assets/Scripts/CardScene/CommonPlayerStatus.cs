using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added

public class CommonPlayerStatus : MonoBehaviour
{
    protected int maxHP = 200;
    protected int HP;
    protected int damageSum;
    protected Text textHP;
    protected Slider barHP;
    //HPの差分を取り、アニメーション調でHPバーを操作
    protected int preHP;
    protected Text damageCount;
    protected DamageCountFunction dcf;
    protected GameObject face;

    protected SEManager sm;
    protected GameObject effect;

    public bool IsAlive(){
        return HP > 0;
    }

    public void AttackFace(){
        face.GetComponent<Face>().ChangeFace(Faces.Attack);
    }

    public void NormalFace(){
        face.GetComponent<Face>().ChangeFace(Faces.Normal);
    }

    // Start is called before the first frame update
    protected void Start()
    {
        HP = maxHP;
        preHP = HP;
        damageSum = 0;
        damageCount = GameObject.Find ("DamageCount").GetComponent<Text>();
        dcf = GameObject.Find ("DamageCount").GetComponent<DamageCountFunction>();
        sm = GameObject.Find ("SEManager").GetComponent<SEManager>();

        //プレハブをGameObject型で取得。使用するエフェクトは2者共通
        effect = (GameObject)Resources.Load ("Prefabs/Hit_Fire");
    }

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
        //アニメーション
        dcf.Animation();
        damageSum += damage;
        damageCount.text = damageSum.ToString();//ここで表示されるのは相手からのダメージ
    }

    public void DamageCal()
    {
        //空振りSEも用意するといいかも
        if(damageSum != 0){
            sm.AttackSE();
        }
        Damage(damageSum);
        damageSum = 0;
        damageCount.text = damageSum.ToString();//ここで表示されるのは相手からのダメージ
    }

    // Update is called once per frame
    protected void Update()
    {
        textHP.text = HP.ToString();
        //barHP.value = HP;
        if(HP != preHP){
            StartCoroutine("BarAnimation");
        }
    }

    IEnumerator BarAnimation()
    {
        while(HP != preHP){
            preHP--;
            barHP.value = preHP;
            yield return null;
        }
    }
}
