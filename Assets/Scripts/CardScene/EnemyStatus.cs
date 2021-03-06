﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added
using Photon.Pun;

public class EnemyStatus : CommonPlayerStatus
{
    void Start()
    {
        base.Start();        
        textHP = GameObject.Find ("EnemyHP").GetComponent<Text>();
        barHP = GameObject.Find("EnemyBar").GetComponent<Slider>();
        face = GameObject.Find ("EnemyCharactor");
    }

    void Update()
    {
        base.Update();
    }
}
