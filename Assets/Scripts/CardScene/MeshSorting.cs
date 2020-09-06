using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSorting : MonoBehaviour
{
    [SerializeField]
    private string m_layerName = "";

    // Start is called before the first frame update
    void Start()
    {
        var render = GetComponent< MeshRenderer >();
        if( render != null )
        {
            render.sortingLayerName = m_layerName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
