using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjViewer : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {

        mouseLookScript = GameObject.Find("Main Camera").GetComponent<MouseLook>();
        playerMovementScript = GameObject.Find("PLAYER").GetComponent<PlayerMovement>();
        //If we use another camera for interaction, this line has to be edited.
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        rotateSpeed = 250;
    }

    // Update is called once per frame
    void Update()
    {
        mouseLookScript.playerCanLookAround = !isViewing;
        playerMovementScript.playerCanMove = !isViewing;

        //Main viewing loop
        if (isViewing)
        { 
            if (Input.mouseScrollDelta.y != 0)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    if (distanceToCamera > 0.2f)
                    {
                        distanceToCamera += -Input.mouseScrollDelta.y / 10f;
                    }
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    if (distanceToCamera < 0.5f)
                    {
                        distanceToCamera += -Input.mouseScrollDelta.y / 10f;
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
    }
    //This void just gets called by any gameObject in the scene when we want to inspect an object. The
    //object that is viewed has to be given (this can be a prefab).
    public void ViewObject(GameObject objectToView)
    {
        objToView = objectToView;
        distanceToCamera = 0.6f;
        objToView.transform.position = cam.transform.position + cam.transform.forward * 2;
        isViewing = true;
    }
    //Gets called in this script whenever we quit viewing the object.
    void ExitObjectView()
    {
        objToView.GetComponent<Rigidbody>().isKinematic = false;
        currentEulerValue = 0;
        isViewing = false;
    }
}
