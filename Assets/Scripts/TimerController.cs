using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text timerText;

    private float timeOut;
    private float timeElapsed;
    private int seconds;

    public void Set(float sec){
        timeOut = sec;
    }

    public bool IsActive(){
        return timeOut != 0.0f;
    }

    // Use this for initialization
    void Start()
    {
        timeElapsed = 0.0f;
        timeOut = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeOut != 0.0f){
            //タイマー実行
            timeElapsed += Time.deltaTime;

            if (timeElapsed >= timeOut)
            {
                timeElapsed = 0.0f;
                timeOut = 0.0f;
            }
        }

        seconds = (int)(timeOut - timeElapsed);
        timerText.text = seconds.ToString();

    }
}
