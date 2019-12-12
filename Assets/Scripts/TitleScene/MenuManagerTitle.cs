using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManagerTitle : MonoBehaviour
{
    public GameObject startText;
    public Button buttonEasy;
    public Button buttonNormal;
    public Button buttonHard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startText.activeSelf){
            if (Input.GetMouseButtonDown (0)) {
                buttonEasy.gameObject.SetActive(true);
                buttonNormal.gameObject.SetActive(true);
                buttonHard.gameObject.SetActive(true);
                startText.SetActive(false);
		    }
        }
        
    }
}
