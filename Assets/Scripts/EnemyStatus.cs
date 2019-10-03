using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added

public class EnemyStatus : MonoBehaviour
{
    public readonly int maxHP = 200;
    private int HP;
    private Text textHP;

    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
    }

    public void Damage(int damage)
    {
        HP -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = "HP:" + HP;

        if(HP <= 0)
        {
            GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
        }
        
    }
}
