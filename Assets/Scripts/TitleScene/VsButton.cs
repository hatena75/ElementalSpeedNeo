﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VsButton : MonoBehaviour
{
    private SceneManagerTitle st;
    private MenuManagerTitle mt;


    public void OnClickVsPlayer() {
        //Photonの対人ロビーに移動
        st.LobbyLoad();
    }

    public void OnClickVsCPU() {
        mt.MenuDifficulty();
    }

    // Start is called before the first frame update
    void Start()
    {
        st = GameObject.Find ("Master").GetComponent<SceneManagerTitle>();
        mt = GameObject.Find ("Master").GetComponent<MenuManagerTitle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
