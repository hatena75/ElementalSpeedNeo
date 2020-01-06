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
    public Button buttonVsPlayer;
    public Button buttonVsCPU;
    private SEManagerTitle se;

    public void MenuDifficulty(){
        se.DecisionSE();
        buttonEasy.gameObject.SetActive(true);
        buttonNormal.gameObject.SetActive(true);
        buttonHard.gameObject.SetActive(true);
        buttonVsPlayer.gameObject.SetActive(false);
        buttonVsCPU.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        se = GameObject.Find ("SEManager").GetComponent<SEManagerTitle>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startText.activeSelf){
            if (Input.GetMouseButtonDown (0)) {
                se.DecisionSE();
                buttonVsPlayer.gameObject.SetActive(true);
                buttonVsCPU.gameObject.SetActive(true);
                startText.SetActive(false);
		    }
        }
        
    }
}
