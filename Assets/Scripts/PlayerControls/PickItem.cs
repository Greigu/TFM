using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickItem : MonoBehaviour
{

    public float grabRange = 5f;
    private GameObject grabbedObject;
    private bool isGrabbed = false;

    private MouseLook mouseScript;
    private PlayerControls playerMovement;

    public float lookRange = 20f;
    private GameObject lastItem;

    private GameObject flashLight;
    private bool isFlashActive = true; //TODO Change in final version
    private int keyItemsPicked = 0;
    // Start is called before the first frame update
    void Start()
    {
        mouseScript = transform.GetComponent<MouseLook>();
        playerMovement = transform.parent.GetComponent<PlayerControls>();
        flashLight = GameObject.Find("FlashLight");
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGlow();
        if (Input.GetMouseButtonDown(0) && !isGrabbed)
        {
            TryGrab();
        }
        else if (Input.GetMouseButtonDown(0) && isGrabbed)
        {
            Debug.Log("No Grabbed");
            mouseScript.SetActiveMoveCamera(true);
            playerMovement.SetIsActiveMove(true);
            if(grabbedObject != null)
            grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(false);
            isGrabbed=false;
        }

        if (Input.GetKeyDown(KeyCode.F) && isFlashActive)
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

    private void TryGrab()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Grabbable"))
            {
                isGrabbed=true;
                Debug.Log("Grabbed");
                mouseScript.SetActiveMoveCamera(false);
                playerMovement.SetIsActiveMove(false);
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(true);
            }else if (hit.collider.CompareTag("Readable"))
            {
                print("Scroll Found");
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().GetText();
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead, false);
            } else if (hit.collider.CompareTag("Speakable"))
            {
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().GetText();
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead, true);
            } else if (hit.collider.gameObject.CompareTag("Openable"))
            {
                OpenClassicDoor openScript = hit.collider.gameObject.GetComponent<OpenClassicDoor>();
                openScript._OpenDoor();
            } else if (hit.collider.gameObject.CompareTag("Pickable"))
            {
                if (hit.collider.gameObject.name.Equals("GrabbableTorch"))
                {
                    isFlashActive = true;
                }
                else
                {
                    keyItemsPicked++;
                }
                hit.collider.gameObject.SetActive(false);
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

    public int GetKeyItems()
    {
        return keyItemsPicked;
    }
}
