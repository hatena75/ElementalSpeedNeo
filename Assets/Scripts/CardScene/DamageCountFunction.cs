using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCountFunction : MonoBehaviour
{
    private Animator animator;

    public void Animation(){
        animator.SetBool("DamagePlus", true);
    }

    public void AnimationEnd(){
        animator.SetBool("DamagePlus", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent (typeof(Animator)) as Animator;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
