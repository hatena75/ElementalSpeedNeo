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

    public void Activate(){
        usable = true;
        btn.interactable = usable;
    }

    public void DeActivate(){
        usable = false;
        btn.interactable = usable;
    }

    public void OnClick() {
        if(usable){
            character.Skill();
            DeActivate();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //btn = gameObject.GetComponent<Button>();
        DeActivate();
        character = SceneManagerCharacterSelect.UsingCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
