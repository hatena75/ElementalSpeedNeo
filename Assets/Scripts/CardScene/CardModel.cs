using UnityEngine;
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

    private EffekseerEffectAsset changeeffect;
    private EffekseerEffectAsset changeelementeffect;
    private EffekseerEffectAsset weakingeffect;
    private EffekseerHandle effectHandler;

    private PhotonView photonView;

    private void ChangeEffect(){
        photonView.RPC("ChangeEffectRpc", RpcTarget.All);
    }

    [PunRPC]
    private void ChangeEffectRpc(){
        effectHandler = EffekseerSystem.PlayEffect(changeeffect, firstPos);
    }

    public void ChangeElementEffect(){
        photonView.RPC("ChangeElementEffectRpc", RpcTarget.All);
    }

    [PunRPC]
    private void ChangeElementEffectRpc(){
        effectHandler = EffekseerSystem.PlayEffect(changeelementeffect, firstPos);
    }

    public void WeakingEffect(){
        photonView.RPC("WeakingEffectRpc", RpcTarget.All);
    }

    [PunRPC]
    private void WeakingEffectRpc(){
        effectHandler = EffekseerSystem.PlayEffect(weakingeffect, firstPos);
    }

    public void Reset(){
        ResetPos();
        RandomFace();
    }

    public void ChangeFace(int Index)
    {
        photonView.RPC("ChangeFaceSync", RpcTarget.All, Index);
    }

    [PunRPC]
    private void ChangeFaceSync(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }

    public void RandomFace()
    {
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
        ChangeEffect();
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
        changeeffect = Resources.Load<EffekseerEffectAsset> ("Effekseer/CardChange");
        changeelementeffect = Resources.Load<EffekseerEffectAsset> ("Effekseer/changeelement");
        weakingeffect = Resources.Load<EffekseerEffectAsset> ("Effekseer/weaking");
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        //RandomFaceを使うとエフェクトが出てしまうため、別で初期化を行なう
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
        this.transform.localScale = new Vector3(0.817f, 0.817f, 0.817f);
        //オンライン対戦時、2P側ならカードを反対向きにする
        if(!PhotonNetwork.IsMasterClient){
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        firstPos = this.transform.position;
        if(gameObject.tag != "Field"){
            UnMovableColor();
        }
    }

}
