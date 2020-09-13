using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class SkillButton : MonoBehaviour
{
    private bool usable;
    public Button btn;

    private CharacterAbstract character;

    public int UseCount{
        private get;
        set;
    }

    public void Activate(){
        if(UseCount != 0){
            usable = true;
            btn.interactable = usable;
        }
    }

    public void DeActivate(){
        usable = false;
        btn.interactable = usable;
    }

    public void OnClick() {
        if(usable){
            character.Skill();
            UseCount--;
            DeActivate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //btn = gameObject.GetComponent<Button>();
        DeActivate();
        character = SceneManagerCharacterSelect.UsingCharacter;
        //先行は1回、後攻は2回まで使用できる。これはofflinemodeの判別処理順番の都合上ステートマシンで初期化している。
        //ここではnullにならないようにだけしている。
        UseCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
