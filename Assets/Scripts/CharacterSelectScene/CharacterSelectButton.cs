using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectButton : MonoBehaviour
{
     private SceneManagerCharacterSelect sc;
    
    public void OnClickYuuki() {
        //newと同じ
        sc.SelectCharacter(gameObject.AddComponent<Yuuki>());
    }

    public void OnClickMasato() {
        sc.SelectCharacter(gameObject.AddComponent<Masato>());
    }

    // Start is called before the first frame update
    void Start()
    {
        sc = GameObject.Find ("Master").GetComponent<SceneManagerCharacterSelect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
