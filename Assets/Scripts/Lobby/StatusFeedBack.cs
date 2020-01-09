using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusFeedBack : MonoBehaviour
{
    public Text statusText;

    public void StatusConnected(){
        statusText.text = "Connected";
    }
    public void StatusWaiting(){
        statusText.text = "Waiting...";
    }

    public void StatusMatch(){
        statusText.text = "Match!";
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
