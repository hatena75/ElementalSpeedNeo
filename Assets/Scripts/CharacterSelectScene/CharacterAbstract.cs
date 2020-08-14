using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbstract : MonoBehaviour
{
    protected string name;
    protected string picture;

    public abstract void Skill();

    public CharacterAbstract(){}

}
