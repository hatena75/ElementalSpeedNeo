using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerTitle : MonoBehaviour
{
    public GameObject panel;
    float a;
    private SEManagerTitle se;

    public static int Level{
        get;
        private set;
    }

    //trueなら対人戦
    public static bool IsVs{
        get;
        private set;
    }

    public void MainLoad(int lv){
        IsVs = false;
        Level = lv;
        StartCoroutine(FadeOutpanel());
    }

    public void LobbyLoad(){
        IsVs = true;
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
        IsVs = false;
        Level = 2;
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

    // Update is called once per frame
    void Update()
    {

    }
}
