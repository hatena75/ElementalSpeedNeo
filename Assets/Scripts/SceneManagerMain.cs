using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMain : MonoBehaviour
{
    public void Win(){
        SceneManager.LoadScene("GameClear");
    }

    public void Lose(){
        SceneManager.LoadScene("GameOver");
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
