using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEasy : MonoBehaviour
{
    private SceneManagerTitle st;

    public void OnClick() {
        st.MainLoad(3);
    }

    // Start is called before the first frame update
    void Start()
    {
        st = GameObject.Find ("Master").GetComponent<SceneManagerTitle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
