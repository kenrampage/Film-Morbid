using UnityEngine;
public class ObjViewer : MonoBehaviour
{
    public playerInteractionState playerInteractionStateScript;
    public PlayerMovement playerMovementScript;
    public float distanceToCamera, rotateSpeed;
    public MouseLook mouseLookScript;
    public Quaternion originalRotation;
    Quaternion objRotation;
    public bool isViewing;
    GameObject objToView;
    public Vector3 originalPos;
    GameObject cam;
    void Start()
    {
        //this line depends on player being tagged "Player".
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerInteractionStateScript = playerMovementScript.gameObject.GetComponent<playerInteractionState>();
        //If we use another camera for interaction, this line has to be edited.
        cam = Camera.main.gameObject;
        mouseLookScript = cam.GetComponent<MouseLook>();
        rotateSpeed = 250;
    }
    void Update()
    {
        playerMovementScript.playerCanMove = !isViewing;
        if (isViewing)
        { 
            if (Input.mouseScrollDelta.y != 0)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    if (distanceToCamera < 0.5f)
                    {
                        distanceToCamera += Input.mouseScrollDelta.y / 10f;
                    }
                    else if (distanceToCamera > 0.5f)
                    {
                        distanceToCamera = 0.5f;
                    }
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    if (distanceToCamera > 0.2f)
                    {
                        distanceToCamera += Input.mouseScrollDelta.y / 10f;
                    }
                    else if(distanceToCamera < 0.2f)
                    {
                        distanceToCamera = 0.2f;
                    }
                }
                if(distanceToCamera <= 0.2f)
                {
                    distanceToCamera = 0.21f;
                }
            }
            objToView.transform.position = cam.transform.position + cam.transform.forward * (distanceToCamera * 5);
            if (Input.GetMouseButton(1))
            {
                objToView.transform.Rotate(60 * Time.deltaTime, 90 * Time.deltaTime, 40 * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitObjectView();
            }
        }
        else if (!isViewing)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 4))
            {
                if(hit.collider.tag == "holdable")
                {
                    if (playerInteractionStateScript.playerIsAllowedToInteract)
                    {
                        if (Input.GetKeyDown("e"))
                        {
                            ViewObject(hit.collider.gameObject);
                            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                            {
                                hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                            }
                            playerInteractionStateScript.playerIsAllowedToInteract = false;
                        }
                    }
                }
            }
        }
    }
    /// <summary>
    /// Inspecting an object from close-by.
    /// </summary>
    /// <param name="objectToView"></param>
    public void ViewObject(GameObject objectToView)
    {
        originalPos = objectToView.transform.position;
        originalRotation = objectToView.transform.rotation;
        objToView = objectToView;
        distanceToCamera = 0.35f;
        objToView.transform.position = cam.transform.position + cam.transform.forward * 2;
        isViewing = true;
    }
    //Gets called in this script whenever we quit viewing the object.
    private void ExitObjectView()
    {
        if (objToView.GetComponent<Rigidbody>() != null)
        {
            objToView.GetComponent<Rigidbody>().isKinematic = false;
        }
        objToView.transform.position = originalPos;
        objToView.transform.rotation = originalRotation;
        isViewing = false;
        playerInteractionStateScript.playerIsAllowedToInteract = true;   
    }
}
