using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SceneManagerOver : MonoBehaviour
{
    public GameObject Panel;
    float a;
    private bool isFade;

    void Awake()
    {
        a = Panel.GetComponent<Image>().color.a;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFade = true;
        StartCoroutine(FadeInPanel());
    }

    IEnumerator FadeInPanel()
    {
        isFade = true;
        while(a > 0.0f)
        {
            //Debug.Log(a);
            Panel.GetComponent<Image>().color -= new Color(0, 0, 0, 0.01f);
            a -= 0.01f;
            yield return null;
        }
        isFade = false;
    }

    IEnumerator FadeOutPanel()
    {
        while(a < 1.0f)
        {
            //Debug.Log(a);
            Panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.01f);
            a += 0.01f;
            yield return null;
        }

        SceneManager.LoadScene ("Title");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown (0) && isFade == false) {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
			StartCoroutine(FadeOutPanel());
		}
    }
}
