using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    public Transform grabPosition;

    private bool isGrabed = false;
    public float mouseSensitivity = 500f;
    float xRotation = 0f;
    float yRotation = 0f;
    private Vector3 originalPos;
    private Quaternion originalRot;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        originalRot = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabed)
        {
            
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            transform.Rotate(Vector3.up, -mouseX * mouseSensitivity * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.forward, mouseY * mouseSensitivity * Time.deltaTime, Space.World);
        }
    }

    public void SetIsGrabbed(bool a)
    {
        isGrabed = a;
        if (isGrabed)
        {
            transform.position = grabPosition.position;
        } else if(!isGrabed)
        {
            transform.position = originalPos;
            transform.rotation = originalRot;
            xRotation = 0f;
            yRotation = 0f;
            Debug.Log(transform.position);
            
        }
    }
}
