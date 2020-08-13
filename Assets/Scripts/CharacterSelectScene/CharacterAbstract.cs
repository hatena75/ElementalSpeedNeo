using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbstract : MonoBehaviour
{
    private string name;
    private string picture;

    public abstract void Skill();

    public CharacterAbstract(string name, string picture){
        this.name = name;
        this.picture = picture;
    }

}
