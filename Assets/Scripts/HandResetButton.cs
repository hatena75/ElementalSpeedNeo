using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandResetButton : MonoBehaviour
{
    public float timeOut;
    private float timeElapsed;

    public void OnClick() {
        if(timeElapsed >= timeOut){
            
            GameObject[] playerHands = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject playerHand in playerHands) {
                playerHand.GetComponent<CardModel>().RandomFace();
            }

            timeElapsed = 0.0f;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeOut = 3.0f;
        timeElapsed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }
}
