﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerCharacterSelect : MonoBehaviour
{
    public GameObject panel;
    float a;

    private static CharacterAbstract usingCharacter; //自分の使用キャラ

    public static CharacterAbstract UsingCharacter { get => usingCharacter; }

    public static CharacterAbstract EnemyCharacter { get; set; }

    private SEManagerTitle se;

    public void SelectCharacter(CharacterAbstract character){
        usingCharacter = character;
        se.DecisionSE();
        MainLoad();
    }

    public void MainLoad(){
        if(SceneManagerTitle.IsVs){
            StartCoroutine(FadeOutLobby());
        }
        else{
            StartCoroutine(FadeOutpanel());
        }
    }

    void Awake(){
        a = panel.GetComponent<Image>().color.a;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInpanel());
        //オフライン用のデフォルト
        EnemyCharacter = gameObject.AddComponent<Cpu>();

        se = GameObject.Find ("SEManager").GetComponent<SEManagerTitle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeInpanel()
    {
        while(a > 0.0f)
        {
            //Debug.Log(a);
            panel.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            a -= 0.01f;
            yield return null;
        }

        panel.SetActive(false);
    }

    IEnumerator FadeOutpanel()
    {
        panel.SetActive(true);

        while(a < 1.0f)
        {
            //Debug.Log(a);
            panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        //SceneManager.LoadScene ("Card");
        SceneManager.LoadScene ("CardPvP");

    }

    IEnumerator FadeOutLobby()
    {
        panel.SetActive(true);

        while(a < 1.0f)
        {
            //Debug.Log(a);
            panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene ("Lobby");
    }
}
