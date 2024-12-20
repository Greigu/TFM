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

    public float lookRange = 20f;
    private GameObject lastItem;
    // Start is called before the first frame update
    void Start()
    {
        mouseScript = transform.GetComponent<MouseLook>();
        playerMovement = transform.parent.GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGlow();
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
                tContr.ChangeText(textToRead, false);
            } else if (hit.collider.CompareTag("Speakable"))
            {
                print("speak");
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().text;
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead, true);
            }
            
        }
    }

    private void  CheckIfGlow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookRange))
        {
            if (hit.collider.gameObject != lastItem && lastItem != null)
            {
                print("Reset");
                GlowItems glow = lastItem.GetComponent<GlowItems>();
                glow.ResetMat(lastItem);
                lastItem = null;
            }
            if (hit.collider.GetComponent<GlowItems>() != null)
            {
                lastItem = hit.collider.gameObject;
                GlowItems glow = hit.collider.GetComponent<GlowItems>();
                glow.ChangeMat(hit.collider.gameObject);

            }
        }
    }
}
