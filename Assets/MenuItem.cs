using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    [SerializeField] float xOffset, yOffset;
    private Vector3 origPos;
    // Start is called before the first frame update
    void Start()
    {
        origPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(origPos.x - (Input.mousePosition.x - Screen.width / 2) * xOffset, origPos.y - (Input.mousePosition.y - Screen.height / 2) * yOffset, 0);
    }
}
