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
    private delegate void posSyncType();
    private posSyncType posSync;

    private System.Random rand;

    private EffekseerEffectAsset changeeffect;
    private EffekseerEffectAsset changeelementeffect;
    private EffekseerEffectAsset weakingeffect;
    private EffekseerHandle effectHandler;

    private PhotonView photonView;

    public void ChangeEffect(){
        effectHandler = EffekseerSystem.PlayEffect(changeeffect, firstPos);
    }

    public void ChangeElementEffect(){
        effectHandler = EffekseerSystem.PlayEffect(changeelementeffect, firstPos);
    }

    public void WeakingEffect(){
        effectHandler = EffekseerSystem.PlayEffect(weakingeffect, firstPos);
    }

    public void Reset(){
        ResetPos();
        RandomFace();
    }

    public void ChangeFace(int Index)
    {
        //photonView.RPC("ChangeFaceSync", RpcTarget.All, Index);
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }

    /*
    [PunRPC]
    private void ChangeFaceSync(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }
    */

    private void RandomFaceIni()
    {
        //ChangeFace(rand.Next(0, cardMax));
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
    }

    public void RandomFace()
    {
        RandomFaceIni();
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

    /*
    private void PosSync(){
        photonView.RPC("PosSyncRpc", RpcTarget.Others, new float[] {this.transform.position.x, this.transform.position.y});
    }

    [PunRPC]
    private void PosSyncRpc(float[] posList){
        this.transform.position = new Vector3(posList[0], posList[1], this.transform.position.z);
    }
    */
    public void SetRand(){
        rand = new System.Random();
        //乱数インスタンスセット時に初期化
        RandomFaceIni();
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
        RandomFaceIni();
        this.transform.localScale = new Vector3(0.817f, 0.817f, 0.817f);
        //オンライン対戦時、2P側ならカードを反対向きにする→RaiseEventを使うため不要になった。
        /*
        if(!PhotonNetwork.IsMasterClient){
            this.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        */
        firstPos = this.transform.position;
        if(gameObject.tag != "Field"){
            UnMovableColor();
        }

        /*
        if(PhotonNetwork.IsMasterClient){
            if(this.transform.tag == "Player"){
                posSync = new posSyncType(PosSync);
            }
        }
        else{
            if(this.transform.tag == "Player2"){
                posSync = new posSyncType(PosSync);
            }
        }

        if(posSync is null){posSync = new posSyncType(DoNothing);}
        */
    }

    //private void DoNothing(){ }

    void Update(){
        //Startで指定した条件を満たしていればPosSyncが呼ばれ、それ以外では何もしない
        //posSync();
    }

}
