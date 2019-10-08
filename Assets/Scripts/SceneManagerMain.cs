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
        SceneManager.LoadScene("GameClear");
    }

    public void Lose(){
        SceneManager.LoadScene("GameOver");
    }

    void Awake()
    {
        a = Panel.GetComponent<Image>().color.a;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadePanel());
    }

    IEnumerator FadePanel()
    {
        while(a > 0.0f)
        {
            Debug.Log(a);
            Panel.GetComponent<Image>().color -= new Color(0, 0, 0, 0.02f);
            a += 0.02f;
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
