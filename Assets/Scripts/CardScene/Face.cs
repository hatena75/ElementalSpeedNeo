using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum Faces : int
{
    Normal, Attack, Win, Lose //ダメージを受けたときもLose
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
        if(!PhotonNetwork.IsMasterClient){
            spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, 180);
            //spriteRenderer.transform.Rotate(new Vector3(0, 0, 180));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
