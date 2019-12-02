using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faces : int
{
    Normal, Attack, Win, Lose
}

public class Face : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    [SerializeField] string charName; //Inspectorから書き換え可能

    public void ChangeFace(Faces face){
        spriteRenderer.sprite = faces[(int)face];
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        faces = Resources.LoadAll<Sprite> ("pictures/" + charName);
    }

    void Start()
    {
        ChangeFace(Faces.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
