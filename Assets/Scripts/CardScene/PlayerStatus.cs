using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //added
using Photon.Pun;

public class PlayerStatus : CommonPlayerStatus
{
    void Start()
    {
        base.Start();
        textHP = GameObject.Find ("PlayerHP").GetComponent<Text>();
        barHP = GameObject.Find("PlayerBar").GetComponent<Slider>();
        face = GameObject.Find ("PlayerCharactor");
    }

    void Update()
    {
        base.Update();
    }
}
