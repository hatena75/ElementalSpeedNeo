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
    public bool animationEnd = false;
    //SE関連
    [SerializeField]
    private AudioClip cyber1;
    [SerializeField]
    private AudioClip cyber2;
    private AudioSource audioSource;

    public void TextureChangeGo(){
        banaText.text = "GO!";
    }

    public void Animation(){
        //bana.SetActive(true);
        //animator.Play("ReadyAnimation");
        animator.enabled = true;
    }

    public void AnimationEnd(){
        animationEnd = true;
    }

    public void PlayCyber1(){
        audioSource.clip = cyber1;
        audioSource.Play();
    }

    public void PlayCyber2(){
        audioSource.clip = cyber2;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent (typeof(Animator)) as Animator;
        animator.enabled = false;
        //this.gameObject.SetActive(false);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
