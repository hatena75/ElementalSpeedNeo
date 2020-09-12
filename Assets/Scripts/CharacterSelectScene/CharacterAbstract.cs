﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbstract : MonoBehaviour
{
    //private string name;
    //private string picture;
    protected SEManager sm;

    public string Name{
        get;
        protected set;
    }

    public string Picture{
        get;
        protected set;
    }

    public abstract void Skill();

    public CharacterAbstract(){}

}
