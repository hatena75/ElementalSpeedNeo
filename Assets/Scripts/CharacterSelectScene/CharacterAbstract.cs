using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharactorAbstruct : MonoBehaviour
{
    private string name;
    private string picture;

    protected abstract void Skill();

    public CharactorAbstruct(string name, string picture){
        this.name = name;
        this.picture = picture;
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
