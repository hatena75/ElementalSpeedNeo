using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BanaFunction : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro banaText;
    private Animator animator;
    [SerializeField]
    private GameObject bana;

    public void TextureChangeGo(){
        banaText.text = "GO!";
    }

    public void Animation(){
        //bana.SetActive(true);
        //animator.Play("ReadyAnimation");
        animator.enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent (typeof(Animator)) as Animator;
        animator.enabled = false;
        //this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
