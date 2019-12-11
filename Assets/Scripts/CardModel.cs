using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite cardBack;
    public int cardIndex; // e.g. faces[cardIndex];
    public int cardMax; // e.g. faces[cardIndex];
    public Vector3 firstPos;

    public void Reset(){
        ResetPos();
        RandomFace();
    }

    public void ChangeFace(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }

    public void RandomFace()
    {
        ChangeFace((int)Random.Range(0.0f, (float)cardMax));
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
    }

    void Start()
    {
        RandomFace();
        //Debug.Log(cardIndex);
        firstPos = this.transform.position;
        if(gameObject.tag != "Field"){
            UnMovableColor();
        }
    }

}
