using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerCharacterSelect : MonoBehaviour
{
    private static string usingCharacter; //自分の使用キャラ

    public static string UsingCharacter { get => usingCharacter; }

    public void SelectCharacter(string character){
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
