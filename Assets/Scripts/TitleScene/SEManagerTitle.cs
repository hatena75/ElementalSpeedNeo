using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManagerTitle : MonoBehaviour {
    public AudioClip decision;
    private AudioSource audioSource;

    void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void PlaySE(AudioClip audio){
        audioSource.clip = audio;
        audioSource.Play();
    }

    public void DecisionSE(){
        PlaySE(decision);
    }

    void Update () {

    }

}