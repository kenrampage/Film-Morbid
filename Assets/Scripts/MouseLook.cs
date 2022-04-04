using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public bool playerCanLookAround = true;
    public Transform playerBody;
    float xRotation = 0f;
    //For Smooth Fade In:
    [SerializeField] Image blinder;
    float a;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            a = 1;
            blinder.color = new Color(0, 0, 0, a);
        }
        //mouseSensitivity = 30 + 50f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (a > 0)
            {
                a -= Time.deltaTime / 2;
                blinder.color = new Color(0, 0, 0, a);
            }
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (playerCanLookAround)
        {
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Camera.main.fieldOfView = 20;
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            Camera.main.fieldOfView = 81;
        }
    }
}
