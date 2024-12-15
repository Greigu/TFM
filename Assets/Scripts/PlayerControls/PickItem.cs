using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickItem : MonoBehaviour
{

    public float grabRange = 5f;
    private GameObject grabbedObject;

    private MouseLook mouseScript;
    private PlayerControls playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        mouseScript = transform.GetComponent<MouseLook>();
        playerMovement = transform.parent.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrab();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("No Grabbed");
            mouseScript.SetActiveMoveCamera(true);
            playerMovement.SetIsActiveMove(true);
            if(grabbedObject != null)
            grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(false);
        }
    }

    private void TryGrab()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            float dist = hit.distance;
            
            if (hit.collider.CompareTag("Grabbable"))
            {
                Debug.Log("Grabbed");
                mouseScript.SetActiveMoveCamera(false);
                playerMovement.SetIsActiveMove(false);
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(true);
            }else if (hit.collider.CompareTag("Readable"))
            {
                print("Scroll Found");
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().text;
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead);
            }
        }
    }
}
