using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoard : MonoBehaviour
{
    [SerializeField] MapPiece[] blackOnes, whiteOnes;
    [SerializeField] GameObject blinder;
    // Start is called before the first frame update
    void Start()
    {
        blinder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckTiles()
    {
        if (Check())
        {
            //blinder.SetActive(false);
            for (int i = 0; i < 12; i++)
            {
                blackOnes[i].gameObject.transform.parent = null;
                blackOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                blackOnes[i].gameObject.tag = "holdable";
                blackOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
            }
            for(int i = 0; i < 14; i++)
            {
                whiteOnes[i].gameObject.transform.parent = null;
                whiteOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                whiteOnes[i].gameObject.tag = "holdable";
                whiteOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
            }
        }
    }
    private bool Check()
    {
        for (int i = 0; i < 12; i++)
        {
            if (!blackOnes[i].isBlack)
            {
                return false;
            }
        }
        for (int j = 0; j < 14; j++)
        {
            if (whiteOnes[j].isBlack)
            {
                return false;
            }
        }
        return true;
    }
}
