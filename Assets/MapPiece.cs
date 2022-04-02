using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    [SerializeField] Material[] colors;
    public bool isBlack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColor()
    {
        isBlack = !isBlack;
        if (isBlack)
        {
            GetComponent<MeshRenderer>().material = colors[0];
        }
        else if (!isBlack)
        {
            GetComponent<MeshRenderer>().material = colors[1];
        }
        transform.parent.GetComponent<MapBoard>().CheckTiles();
    }
}
