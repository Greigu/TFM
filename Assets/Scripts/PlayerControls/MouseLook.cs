using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 250f;
    public Transform playerBody;
    private bool isActiveMoveCamera = true;
    float xRotation = 0f;
    private GameObject flashLight;

    private bool isPaused;
    private GameController gameController;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        flashLight = GameObject.Find("FlashLight");
        flashLight.SetActive(false);
    }

    void Update()
    {
        isPaused = gameController.GetIsPaused();
        if (!isPaused){
            FuncMouseLook();
        }
    }

    public void SetActiveMoveCamera(bool a)
    {
        isActiveMoveCamera = a;
    }

    private void FuncMouseLook()
    {
        if (isActiveMoveCamera)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (flashLight.activeSelf)
                {
                    flashLight.SetActive(false);
                }
                else
                {
                    flashLight.SetActive(true);
                }

            }
        }
    }
}
