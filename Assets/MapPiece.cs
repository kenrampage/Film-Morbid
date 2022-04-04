using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPiece : MonoBehaviour
{
    [SerializeField] Material[] colors;
    public bool puzzleSolved = false;
    public bool isBlack; 
    private void Update()
    {
        //Changing the color of a square;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit, 4))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleColor();
                }
            }
        }
    }
    ///<summary>Changes color of map tiles when called</summary>
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
        if(!puzzleSolved)
        {
            transform.parent.GetComponent<MapBoard>().CheckTiles();
        }
    }
}
