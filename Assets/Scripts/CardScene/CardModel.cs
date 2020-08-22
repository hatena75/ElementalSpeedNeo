﻿using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Effekseer;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex; // e.g. faces[cardIndex];
    public int cardMax; // e.g. faces[cardIndex];
    public Vector3 firstPos;
    private EffekseerEffectAsset effect;
    private EffekseerHandle effectHandler;

    private RaiseEvents rE;
    private PhotonView photonView;

    private void ChangeEffect(){
        if(PhotonNetwork.IsConnected){
            photonView.RPC("ChangeEffectRpc", RpcTarget.All);
        }
        else{
            effectHandler = EffekseerSystem.PlayEffect(effect, firstPos);
        }
    }

    [PunRPC]
    private void ChangeEffectRpc(){
        effectHandler = EffekseerSystem.PlayEffect(effect, firstPos);
    }

    public void Reset(){
        ResetPos();
        RandomFace();
    }

    public void ChangeFace(int Index)
    {
        if(PhotonNetwork.IsConnected){
            photonView.RPC("ChangeFaceSync", RpcTarget.All, cardIndex);
        }
        else{
            cardIndex = Index;
            spriteRenderer.sprite = faces[cardIndex];
        }
        //cardIndex = Index;
        //spriteRenderer.sprite = faces[cardIndex];
        //object[] content = new object[] { this.gameObject, cardIndex};
        //rE.CardSync(content);
    }

    [PunRPC]
    private void ChangeFaceSync(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
        //rE.CardSync(content);
    }

    public void RandomFace()
    {
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
        ChangeEffect();
    }

    public void ToggleFace(bool showFace)
    {
        if (showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }

    public void ResetPos(){
        this.transform.position = firstPos;
    }

    public void UnMovableColor(){
        float changeRed = 0.8f;
        float changeGreen = 0.8f;
        float cahngeBlue = 0.8f;
        float cahngeAlpha = 1.0f;
        //カードが少し暗くなる
        GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, cahngeBlue, cahngeAlpha);
    }

    public void MovableColor(){
        float changeRed = 1.0f;
        float changeGreen = 1.0f;
        float cahngeBlue = 1.0f;
        float cahngeAlpha = 1.0f;
        // 元の画像になる。
        GetComponent<SpriteRenderer>().color = new Color(changeRed, changeGreen, cahngeBlue, cahngeAlpha);
    }


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        faces = Resources.LoadAll<Sprite> ("pictures/frames");
        cardMax = faces.Length;
        effect = Resources.Load<EffekseerEffectAsset> ("Effekseer/CardChange");
        //rE = GameObject.Find("Master").GetComponent<RaiseEvents>();
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //RandomFaceを使うとエフェクトが出てしまうため、別で初期化を行なう
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
        this.transform.localScale = new Vector3(0.817f, 0.817f, 0.817f);
        if(!PhotonNetwork.IsMasterClient && PhotonNetwork.IsConnected){
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        //Debug.Log(cardIndex);
        firstPos = this.transform.position;
        if(gameObject.tag != "Field"){
            UnMovableColor();
        }
    }

}
