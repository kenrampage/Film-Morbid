using UnityEngine;

public class MusicDot : MonoBehaviour
{
    [SerializeField] GameObject musicBoard;
    public float initialYPos;
    public int currentPt;
    Vector3[] pts;
    void Start()
    {
        //The points on which the dots can be get calculated here: 
        initialYPos = transform.position.y;
        pts = new Vector3[]{ new Vector3(transform.position.x, initialYPos, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.15f, transform.position.z), new Vector3(transform.position.x,initialYPos + 0.28f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.41f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.52f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.65f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.78f, transform.position.z), new Vector3(transform.position.x, initialYPos + 0.935f, transform.position.z), new Vector3(transform.position.x, initialYPos + 1.06f, transform.position.z) };
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4f))
        {
            if (Input.GetKeyDown("e"))
            {
                if (hit.collider.gameObject != null)
                {
                    //this.gameObject being the current dot that's clicked by the player:
                    if (hit.collider.gameObject == gameObject)
                    {
                        ChangeDotPosition();
                    }
                    //Treble Clef functions as the confirm button here
                    if (hit.collider.gameObject.name == "Treble Clef")
                    {
                        musicBoard.GetComponent<MusicPuzzle>().CheckIfCorrect(hit.collider.gameObject);
                    }
                }
            }
        }
    }
    private void ChangeDotPosition()
    {
        currentPt++;
        //prevents array overflow
        if (currentPt == pts.Length)
        {
            currentPt = 0;
        }
        //does the actual positioning
        transform.position = pts[currentPt];
    }
    
}
