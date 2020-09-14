using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

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

    private PhotonView photonView;

    private Dictionary<String, AudioClip> AudioChart;

    void Start () {
        audioSource = gameObject.GetComponents<AudioSource>();
        audioSource[(int)SE.Players].clip = null;
        audioSource[(int)SE.Effect].clip = null;

        photonView = GetComponent<PhotonView>();

        //属性相性の定義
        AudioChart = new Dictionary<string, AudioClip>();
        AudioChart.Add("takeCard", takeCard);
        AudioChart.Add("playCard", playCard);
        AudioChart.Add("reload", reload);
        AudioChart.Add("attack", attack);
        AudioChart.Add("missingAttack", missingAttack);
        AudioChart.Add("changeElement", changeElement);
        AudioChart.Add("down", down);

    }

    private void PlaySE(SE se, string audio){
        photonView.RPC("PlaySERpc", RpcTarget.All, (int)se, audio);
    }

    
    private void PlaySESingle(SE se, AudioClip audio){
        audioSource[(int)se].clip = audio;
        audioSource[(int)se].Play();
    }
    

    [PunRPC]
    private void PlaySERpc(int se, string audio){
        audioSource[se].clip = AudioChart[audio];
        audioSource[se].Play();
    }

    public void CardTakeSE(){
        PlaySE(SE.Players, "takeCard");
    }

    public void CardPlaySE(){
        PlaySE(SE.Players, "playCard");
    }

    public void ReloadSE(){
        PlaySE(SE.Players, "reload");
    }

    public void AttackSE(){
        PlaySESingle(SE.Effect, attack);
    }

    public void MissingAttackSE(){
        PlaySESingle(SE.Players, missingAttack);
    }

    public void ChangeElementSE(){
        PlaySE(SE.Players, "changeElement");
    }

    public void DownSE(){
        PlaySE(SE.Players, "down");
    }

    void Update () {

    }

}