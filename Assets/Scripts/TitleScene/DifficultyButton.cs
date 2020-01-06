using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyButton : MonoBehaviour
{
    private SceneManagerTitle st;
    
    public void OnClickEasy() {
        st.MainLoad(3);
    }

    public void OnClickNormal() {
        st.MainLoad(2);
    }

    public void OnClickHard() {
        st.MainLoad(1);
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
