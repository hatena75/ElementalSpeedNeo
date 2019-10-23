using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerMain : MonoBehaviour
{
    public GameObject Panel;
    float a;

    public void Win(){
        Panel.SetActive(true);
        StartCoroutine(FadeOutWinPanel());
    }

    public void Lose(){
        Panel.SetActive(true);
        StartCoroutine(FadeOutLosePanel());
    }

    void Awake()
    {
        a = Panel.GetComponent<Image>().color.a;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeInPanel());
    }

    IEnumerator FadeInPanel()
    {
        while(a > 0.0f)
        {
            Debug.Log(a);
            Panel.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            a -= 0.01f;
            yield return null;
        }

        Panel.SetActive(false);
    }

    IEnumerator FadeOutWinPanel()
    {
        while(a < 1.0f)
        {
            Debug.Log(a);
            Panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene("GameClear");
    }

    IEnumerator FadeOutLosePanel()
    {
        while(a < 1.0f)
        {
            Debug.Log(a);
            Panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
