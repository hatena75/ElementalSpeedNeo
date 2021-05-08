using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbstract : MonoBehaviour
{
    //private string name;
    //private string picture;
    protected SEManager sm;
    protected CardInfo cardInfo;

    public string Name{
        get;
        protected set;
    }

    public string Picture{
        get;
        protected set;
    }

    public string NameText{
        get;
        protected set;
    }

    public string ExplainText{
        get;
        protected set;
    }

    public string StandPicture{
        get;
        protected set;
    }

    public abstract void Skill();

    public abstract void SkillSync(int[] data);

    public CharacterAbstract(){}

}
