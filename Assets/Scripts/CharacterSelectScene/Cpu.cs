using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cpu : CharacterAbstract
{
    public override void Skill(){
        //なし
    }

    public override void SkillSync(int[] data){
        //なし
    }

    public Cpu() : base(){
        this.Name = "Cpu";
        this.Picture = "pictures/Cpu";
    }
}
