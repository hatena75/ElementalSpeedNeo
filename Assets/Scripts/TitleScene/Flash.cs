using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    public Text text;
    private float a;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        a = Mathf.Sin(Time.time*2) / 2 + 0.5f;
        text.color = new Color(text.color.r, text.color.g, text.color.b, a);
    }
}
