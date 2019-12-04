using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    public AudioClip takeCard;
    public AudioClip playCard;
    private AudioSource[] audioSource;

    private enum SE : int{
        Players, Effect
    }

    void Start () {
        audioSource = gameObject.GetComponents<AudioSource>();
        audioSource[(int)SE.Players].clip = null;
        audioSource[(int)SE.Effect].clip = null;
    }

    private void PlaySE(SE se, AudioClip audio){
        audioSource[(int)se].clip = audio;
        audioSource[(int)se].Play();
    }

    public void CardTakeSE(){
        PlaySE(SE.Players, takeCard);
    }

    public void CardPlaySE(){
        PlaySE(SE.Players, playCard);
    }

    void Update () {

    }

}