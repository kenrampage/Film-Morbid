using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjViewer : MonoBehaviour
{
    public crosshair crosshairScript;
    public playerInteractionState playerInteractionStateScript;
    public MouseLook mouseLookScript;
    public PlayerMovement playerMovementScript;
    GameObject objToView;
    GameObject cam;
    public float distanceToCamera, rotateSpeed;
    public bool isViewing;

    //Rotation purposes:
    Quaternion objRotation;
    int currentEulerValue;
    Vector3[] eulerValues = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(90, 0, 0), new Vector3(270, 0, 0) };

    //Finishing Purposes:
    Vector3 originalPos;
    Quaternion originalRotation;

    [SerializeField] GameObject piano;
    // Start is called before the first frame update
    void Start()
    {
        crosshairScript = GameObject.Find("PLAYER").GetComponent<crosshair>();
        playerInteractionStateScript = GameObject.FindGameObjectWithTag("Player").GetComponent<playerInteractionState>();
        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        //this line depends on player being named "PLAYER".
        playerMovementScript = GameObject.Find("PLAYER").GetComponent<PlayerMovement>();
        //If we use another camera for interaction, this line has to be edited.
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rotateSpeed = 250;
    }

    // Update is called once per frame
    void Update()
    {
        //disable crosshair when viewing
        crosshairScript.crosshairParent.SetActive(!isViewing);
        //disable movement when viewing
        if (!piano.GetComponent<MusicPuzzle>().won && Camera.main.transform.parent.GetComponent<SmoothIntro>().timer < 0)
        {
            playerMovementScript.playerCanMove = !isViewing;
        }

        //Main viewing loop
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
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    if (distanceToCamera > 0.2f)
                    {
                        distanceToCamera += Input.mouseScrollDelta.y / 10f;
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
            objToView.transform.rotation = Quaternion.RotateTowards(objToView.transform.rotation, objRotation, rotateSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                ExitObjectView();
            }
        }
        //For picking up objects
        else if (!isViewing)
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 4))
            {
                if(hit.collider.tag == "holdable")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        ViewObject(hit.collider.gameObject);
                        if (hit.collider.gameObject.GetComponent<Rigidbody>() != null)
                        {
                            playerInteractionStateScript.playerIsAllowedToInteract = false;
                            hit.collider.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }
                }
            }
        }
    }
    //This void just gets called by any gameObject in the scene when we want to inspect an object. The
    //object that is viewed has to be given (this can be a prefab).
    public void ViewObject(GameObject objectToView)
    {
        originalPos = objectToView.transform.position;
        originalRotation = objectToView.transform.rotation;
        objToView = objectToView;
        distanceToCamera = 0.35f;
        objToView.transform.position = cam.transform.position + cam.transform.forward * 2;
        objToView.layer = 7; //change viewed object layer to "item layer" - prevent clipping
        isViewing = true;
    }
    //Gets called in this script whenever we quit viewing the object.
    void ExitObjectView()
    {
        if (objToView.GetComponent<Rigidbody>() != null)
        {
            objToView.GetComponent<Rigidbody>().isKinematic = false;
        }
        objToView.transform.position = originalPos;
        objToView.transform.rotation = originalRotation;
        objToView.layer = 0; //revert viewed object layer to 0 when not viewing
        currentEulerValue = 0;

        isViewing = false;
        playerInteractionStateScript.playerIsAllowedToInteract = true;

    }
}
