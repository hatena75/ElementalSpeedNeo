using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerTitle : MonoBehaviour
{
    private static bool isVs; //trueなら対人戦
    private static int level;
    public GameObject panel;
    float a;
    private SEManagerTitle se;

    public static int Level { get => level; }
    public static bool IsVs { get => isVs; }

    public void MainLoad(int lv){
        isVs = false;
        level = lv;
        StartCoroutine(FadeOutpanel());
    }

    public void LobbyLoad(){
        isVs = true;
        StartCoroutine(FadeOutpanel());
    }

    void Awake()
    {        
        se = GameObject.Find ("SEManager").GetComponent<SEManagerTitle>();
        a = panel.GetComponent<Image>().color.a;
        Screen.SetResolution(450, 800, false, 60);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        isVs = false;
        level = 2;
        StartCoroutine(FadeInpanel());
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
        se.DecisionSE();
        panel.SetActive(true);

        while(a < 1.0f)
        {
            //Debug.Log(a);
            panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene ("CharacterSelect");
    }

    IEnumerator FadeOutLobby()
    {
        se.DecisionSE();
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

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown (0)) {
            StartCoroutine(FadeOutpanel());
		}
        */
    }
}
