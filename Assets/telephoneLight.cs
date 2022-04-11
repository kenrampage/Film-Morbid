using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class telephoneLight : MonoBehaviour
{
    public Material material_lit;
    public Material material_unlit;
    public bool lit;

    private void Start()
    {
        lit = false;
    }

    private void Update()
    {
        if (lit)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = material_lit;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = material_unlit;
        }
    }
}
