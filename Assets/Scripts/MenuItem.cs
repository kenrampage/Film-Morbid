using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [SerializeField] float xOffset, yOffset;
    public Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(origPos.x - (Input.mousePosition.x - Screen.width / 2) * xOffset, origPos.y - (Input.mousePosition.y - Screen.height / 2) * yOffset, 0);
    }
}
