using UnityEngine;

public class Book : MonoBehaviour
{
    public bool isCorrectBook;
    public bool pulledOut, pulling;
    [SerializeField] float moveSpeed;
    float timer;
    public bool won;
    private void Start()
    {
        timer = 0;
        pulledOut = false;
        pulling = false;
    }
    void Update()
    {
        if (Camera.main.transform.parent.GetComponent<SmoothIntro>().started)
        {
            if (!won)
            {
                if (!pulling)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 4))
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            if (hit.collider.gameObject == gameObject)
                            {
                                pulledOut = !pulledOut;
                                transform.parent.GetComponent<Bookcase>().checkBooks();
                                pulling = true;
                            }
                        }
                    }
                }
                if (pulling)
                {
                    timer += Time.deltaTime;
                    if (pulledOut)
                    {
                        if (timer < 0.6)
                        {
                            transform.localPosition -= new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                        }
                        else if (timer > 0.6)
                        {
                            won = transform.parent.GetComponent<Bookcase>().checkBooks();
                            timer = 0;
                            pulling = false;
                        }
                    }
                    else if (!pulledOut)
                    {
                        if (timer < 0.6)
                        {
                            transform.localPosition += new Vector3(Time.deltaTime * moveSpeed, 0, 0);
                        }
                        else if (timer > 0.6)
                        {
                            timer = 0;
                            pulling = false;
                        }
                    }
                }
            }
        }
    }
}
