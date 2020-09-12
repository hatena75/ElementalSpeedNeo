using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;

public class CharacterSelectButton : MonoBehaviour
{
    private SceneManagerCharacterSelect sc;
    private CharacterAbstract character;
    private Text name;
    private Text explain;
    private SpriteRenderer spriteRenderer;

    public void OnClick(){
        sc.SelectCharacter(character);
    }

    private CharacterAbstract GetCharacterInstance(String str){
        //strの文字列の型(キャラ)のインスタンスをnew
        //リフレクションを用いて汎用的にする
        Type t = Type.GetType(str);
        //昔の引数有りのAddComponentと区別するため、引数の数を指定してメソッドを得る
        MethodInfo mi = typeof(GameObject).GetMethod(
            "AddComponent",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            new Type[0], // ここに引数の型を配列で指定
            null
            );
        MethodInfo bound = mi.MakeGenericMethod(t);
        return (CharacterAbstract)bound.Invoke(gameObject, null);
    }

    public void OnMouseOver(){
        character = GetCharacterInstance(this.gameObject.name);
        name.text = character.NameText;
        explain.text = character.ExplainText;
        spriteRenderer.sprite = Resources.Load<Sprite> (character.StandPicture);
    }

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find ("Master").GetComponent<SceneManagerCharacterSelect>();
        name = GameObject.Find ("Name").GetComponent<Text>();
        explain = GameObject.Find ("Explain").GetComponent<Text>();
        spriteRenderer = GameObject.Find ("CharStand").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
