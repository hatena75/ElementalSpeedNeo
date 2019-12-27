using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandResetButton : MonoBehaviour
{
    private bool usable;
    private Button btn;

    public void Activate(){
        usable = true;
        btn.interactable = usable;
    }

    public void DeActivate(){
        usable = false;
        btn.interactable = usable;
    }

    public void OnClick() {
        if(usable){  
            GameObject[] playerHands = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject playerHand in playerHands) {
                playerHand.GetComponent<CardModel>().RandomFace();
            }

            DeActivate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        DeActivate();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
