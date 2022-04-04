using UnityEngine;

public class MapBoard : MonoBehaviour
{
    [SerializeField] MapPiece[] blackOnes, whiteOnes;
    public void CheckTiles()
    {
        if (Check())
        {
            for (int i = 0; i < 13; i++)
            {
                //Black
                blackOnes[i].gameObject.transform.parent = null;
                blackOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                blackOnes[i].gameObject.tag = "holdable";
                blackOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
                //White
                whiteOnes[i].gameObject.transform.parent = null;
                whiteOnes[i].gameObject.GetComponent<Rigidbody>().isKinematic = false;
                whiteOnes[i].gameObject.tag = "holdable";
                whiteOnes[i].GetComponent<MapPiece>().puzzleSolved = true;
            }
        }
    }
    /// <summary>
    /// Checks if the city board is filled in correctly.
    /// </summary>
    /// <returns>The state of correctness of the board.</returns>
    private bool Check()
    {
        //Checks if tiles that need to be black, are black
        for (int i = 0; i < 13; i++)
        {
            if (!blackOnes[i].isBlack)
            {
                return false;
            }
        }
        //Checks if tiles that need to be white, are white
        for (int j = 0; j < 13; j++)
        {
            if (whiteOnes[j].isBlack)
            {
                return false;
            }
        }
        return true;
    }
}
