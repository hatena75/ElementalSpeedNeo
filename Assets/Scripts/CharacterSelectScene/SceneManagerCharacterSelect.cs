using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerCharacterSelect : MonoBehaviour
{
    private static CharacterAbstract usingCharacter; //自分の使用キャラ

    public static CharacterAbstract UsingCharacter { get => usingCharacter; }

    public void SelectCharacter(CharacterAbstract character){
        usingCharacter = character;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
