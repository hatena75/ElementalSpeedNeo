using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerTitle : MonoBehaviour
{
    public GameObject Panel;
    float a;

    void Awake()
    {
        a = Panel.GetComponent<Image>().color.a;
        Screen.SetResolution(450, 800, false, 60);
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
    }

    IEnumerator FadeOutPanel()
    {
        while(a < 1.0f)
        {
            Debug.Log(a);
            Panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene ("Card");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            StartCoroutine(FadeOutPanel());
		}
    }
}
