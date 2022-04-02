using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjViewer : MonoBehaviour
{
    [SerializeField] GameObject testObj;
    GameObject objToView;
    GameObject cam;
    float distanceToCamera, rotateSpeed;
    bool isViewing;

    //Rotation purposes:
    Quaternion objRotation;
    int currentEulerValue;
    Vector3[] eulerValues = { new Vector3(0, 0, 0), new Vector3(0, 90, 0), new Vector3(0, 180, 0), new Vector3(0, 270, 0), new Vector3(90, 0, 0), new Vector3(270, 0, 0) };
    // Start is called before the first frame update
    void Start()
    {
        //If we use another camera for interaction, this line has to be edited.
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        ViewObject(testObj);
        rotateSpeed = 250;
    }

    // Update is called once per frame
    void Update()
    {
        //Main viewing loop
        if (isViewing)
        {
            if (Input.mouseScrollDelta.y != 0)
            {
                if (Input.mouseScrollDelta.y > 0)
                {
                    if (distanceToCamera > 0.33f)
                    {
                        distanceToCamera += -Input.mouseScrollDelta.y / 10;
                    }
                }
                else if (Input.mouseScrollDelta.y < 0)
                {
                    if (distanceToCamera < 0.7f)
                    {
                        distanceToCamera += -Input.mouseScrollDelta.y / 10;
                    }
                }
                objToView.transform.position = cam.transform.position + cam.transform.forward * (distanceToCamera * 5);
            }
            if (Input.GetMouseButtonDown(1))
            {
                currentEulerValue++;
                if(currentEulerValue == 6)
                {
                    currentEulerValue = 0;
                }
                objRotation = Quaternion.Euler(eulerValues[currentEulerValue]);
            }
            objToView.transform.rotation = Quaternion.RotateTowards(objToView.transform.rotation, objRotation, rotateSpeed * Time.deltaTime);
        }
    }
    //This void just gets called by any gameObject in the scene when we want to inspect an object. The
    //object that is viewed has to be given (this can be a prefab).
    public void ViewObject(GameObject objectToView)
    {
        cam.transform.rotation = Quaternion.Euler(0, 0, 0);
        objToView = objectToView;
        distanceToCamera = 0.6f;
        objToView.transform.position = cam.transform.position + cam.transform.forward * (distanceToCamera * 8);
        isViewing = true;
    }
    //Gets called in this script whenever we quit viewing the object.
    void ExitObjectView()
    {

    }
}
