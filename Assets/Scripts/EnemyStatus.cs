﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added

public class EnemyStatus : MonoBehaviour
{
    public readonly int maxHP = 200;
    private int HP;
    private Text textHP;
    private Slider barHP;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
        textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
        barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();

    }

    public void Damage(int damage)
    {
        HP -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        textHP.text = HP.ToString();
        barHP.value = HP;

        if(HP <= 0)
        {
            GameObject.Find ("Master").GetComponent<SceneManagerMain>().Win();
        }
        
    }
}
