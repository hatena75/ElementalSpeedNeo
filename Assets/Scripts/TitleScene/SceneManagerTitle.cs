using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerTitle : MonoBehaviour
{
    private static int level;
    public GameObject panel;
    float a;

    public static int Level { get => level; }

    public void MainLoad(int lv){
        level = lv;
        StartCoroutine(FadeOutpanel());
    }

    void Awake()
    {
        a = panel.GetComponent<Image>().color.a;
        Screen.SetResolution(450, 800, false, 60);
    }
    
    // Start is called before the first frame update
    void Start()
    {
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
        panel.SetActive(true);

        while(a < 1.0f)
        {
            //Debug.Log(a);
            panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene ("Card");
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
