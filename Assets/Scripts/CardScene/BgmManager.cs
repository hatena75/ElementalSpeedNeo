using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public AudioClip bgm;
    private AudioSource audioSource;

    public void BgmPlay(){
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = bgm;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
