using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject[] menuObj;
    [SerializeField] GameObject[] onlyStandaloneButtons;
    public bool onWeb;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        onWeb = true;
        if (Application.platform != RuntimePlatform.WebGLPlayer)
        {
            onWeb = false;
        }
        Back();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Credits()
    {
        menuObj[0].SetActive(false);
        menuObj[1].SetActive(true);
        onlyStandaloneButtons[0].SetActive(false);
        if (!onWeb)
        {
            onlyStandaloneButtons[0].SetActive(true);
        }
    }
    public void Settings()
    {
        menuObj[0].SetActive(false);
        menuObj[2].SetActive(true);
    }
    public void Back()
    {
        menuObj[1].SetActive(false);
        menuObj[2].SetActive(false);
        menuObj[0].SetActive(true);
        onlyStandaloneButtons[1].SetActive(false);
        if (!onWeb)
        {
            onlyStandaloneButtons[1].SetActive(true);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void ToWebsite()
    {
        Application.OpenURL("https://point-vertex.itch.io");
    }
}
