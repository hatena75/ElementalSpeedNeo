using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    public AudioClip takeCard;
    public AudioClip playCard;
    public AudioClip reload;
    public AudioClip attack;
    public AudioClip missingAttack;
    public AudioClip changeElement;
    public AudioClip down;


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

    public void ReloadSE(){
        PlaySE(SE.Players, reload);
    }

    public void AttackSE(){
        PlaySE(SE.Effect, attack);
    }

    public void MissingAttackSE(){
        PlaySE(SE.Players, missingAttack);
    }

    public void ChangeElementSE(){
        PlaySE(SE.Players, changeElement);
    }

    public void DownSE(){
        PlaySE(SE.Players, down);
    }

    void Update () {

    }

}