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

    public void ChangeFace(int Index)
    {
        cardIndex = Index;
        spriteRenderer.sprite = faces[cardIndex];
    }

    public void RandomFace()
    {
        cardIndex = (int)Random.Range(0.0f, (float)cardMax);
        ChangeFace(cardIndex);
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

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        faces = Resources.LoadAll<Sprite> ("pictures/Cards");
        cardMax = faces.Length;
    }

    void Start()
    {
        cardIndex = (int)Random.Range(0.0f, (float)cardMax);
        spriteRenderer.sprite = faces[cardIndex];
        //Debug.Log(cardIndex);
        firstPos = this.transform.position;
    }

}
