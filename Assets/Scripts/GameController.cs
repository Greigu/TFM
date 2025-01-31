using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isPaused = true;
    public GameObject MenuUI;
    private GameObject HowToPlayUI;

    public GameObject fountain;
    void Start()
    {
        HowToPlayUI = GameObject.Find("HowToPlayUI");
        HowToPlayUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) {
            SetIsPaused(!GetIsPaused());
             MenuUI.SetActive(!MenuUI.activeSelf);
            if(!GetIsPaused())
            {
                Cursor.lockState = CursorLockMode.Locked;
            } else {
                Cursor.lockState = CursorLockMode.None;
            }

        }
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    public void SetIsPaused(bool paused)
    {
        isPaused = paused;
    }

    public void PlayButton()
    {
        SetIsPaused(false);
        MenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OptionsButton()
    {
        print("OptionsMenú");
            HowToPlayUI.SetActive(!HowToPlayUI.activeSelf);
            MenuUI.SetActive(!MenuUI.activeSelf);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void ShowMenu()
    {
        MenuUI.SetActive(true);
    }

    public void UnlockEnd()
    {
        fountain.transform.localPosition = new Vector3(fountain.transform.localPosition.x, 22.5f, fountain.transform.localPosition.z);
        fountain.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }
}
