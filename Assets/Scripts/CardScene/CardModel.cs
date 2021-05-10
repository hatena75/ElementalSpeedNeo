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

    private void ChangeEffect(){
        effectHandler = EffekseerSystem.PlayEffect(changeeffect, firstPos);
    }

    private void ChangeElementEffect(){
        effectHandler = EffekseerSystem.PlayEffect(changeelementeffect, firstPos);
    }

    private void WeakingEffect(){
        effectHandler = EffekseerSystem.PlayEffect(weakingeffect, firstPos);
    }

    public void ChangeElement(int index){
        ChangeFace(index);
        ChangeElementEffect();
    }

    public void ChangeWeaking(int index){
        ChangeFace(index);
        WeakingEffect();
    }

    public void Reset(){
        ResetPos();
        RandomFace();
    }

    public void ChangeFace(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }

    private void RandomFaceIni()
    {
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
    }

    public void RandomFace()
    {
        RandomFaceIni();
        ChangeEffect();
    }

    public void ReloadSync(int index){
        ChangeFace(index);
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
        RandomFaceIni();
        this.transform.localScale = new Vector3(0.817f, 0.817f, 0.817f);
        firstPos = this.transform.position;
        if(gameObject.tag != "Field"){
            UnMovableColor();
        }
    }

    //private void DoNothing(){ }

    void Update(){
    }

}
