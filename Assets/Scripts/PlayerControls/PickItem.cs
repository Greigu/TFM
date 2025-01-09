using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    private bool isFlashActive = false; //False in final version
    private int keyItemsPicked = 0;
    private Dictionary<string, bool> ItemsPicked = new Dictionary<string, bool>();
    private string[] items = { "KNIFE", "EvilBook", "IDOL" };
    // Start is called before the first frame update
    void Start()
    {
        mouseScript = transform.GetComponent<MouseLook>();
        playerMovement = transform.parent.GetComponent<PlayerControls>();
        flashLight = GameObject.Find("FlashLight");
        flashLight.SetActive(false);
        foreach (var item in items)
        {
            ItemsPicked.Add(item, false);
        }
        ApplicationModel.itemsGrabbed = ItemsPicked;

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
            if (grabbedObject != null)
                grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(false);
            isGrabbed = false;
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
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange))
        {
            Debug.Log(hit.collider.tag);
            if (hit.collider.CompareTag("Grabbable"))
            {
                isGrabbed = true;
                Debug.Log("Grabbed");
                mouseScript.SetActiveMoveCamera(false);
                playerMovement.SetIsActiveMove(false);
                grabbedObject = hit.collider.gameObject;
                grabbedObject.GetComponent<GrabbableObject>().SetIsGrabbed(true);
            }
            else if (hit.collider.CompareTag("Readable"))
            {
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().GetText();
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead, false);
            }
            else if (hit.collider.CompareTag("Speakable"))
            {
                string textToRead = hit.collider.gameObject.GetComponent<_TextContainer>().GetText();
                TextController tContr = GameObject.Find("TextController").GetComponent<TextController>();
                tContr.ChangeText(textToRead, true);
            }
            else if (hit.collider.gameObject.CompareTag("Openable"))
            {
                OpenClassicDoor openScript = hit.collider.gameObject.GetComponent<OpenClassicDoor>();
                openScript._OpenDoor();
            }
            else if (hit.collider.gameObject.CompareTag("Pickable"))
            {
                if (hit.collider.gameObject.name.Equals("GrabbableTorch"))
                {
                    isFlashActive = true;
                    flashLight.SetActive(true);
                }
                else
                {
                    keyItemsPicked++;
                    ItemsPicked[hit.collider.gameObject.name] = true;
                    ApplicationModel.itemsGrabbed = ItemsPicked;
                    
                }
                hit.collider.gameObject.SetActive(false);
            }

        }
    }

    private void CheckIfGlow()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookRange))
        {
            
            if ((hit.collider.gameObject != lastItem && lastItem != null) && !hit.collider.gameObject.name.Equals("EvilBook") && !lastItem.name.Equals("EvilBook"))
            {
                GlowItems glow = lastItem.GetComponent<GlowItems>();
                glow.ResetMat(lastItem);
                lastItem = null;
            }
            if ((hit.collider.GetComponent<GlowItems>() != null) && !hit.collider.gameObject.name.Equals("EvilBook"))
            {
                lastItem = hit.collider.gameObject;
                GlowItems glow = hit.collider.GetComponent<GlowItems>();
                glow.ChangeMat(hit.collider.gameObject);
            }
            if (hit.collider.gameObject.name.Equals("EvilBook"))
            {
                if (hit.collider.GetComponent<GlowItems>() != null)
                {
                    lastItem = hit.collider.gameObject;
                    foreach (Transform t in lastItem.transform)
                    {
                        GlowItems glow = t.GetComponent<GlowItems>();
                        glow.ChangeMat(t.gameObject);
                    }
                }
            }
            if (lastItem != null)
            {
                if (hit.collider.gameObject != lastItem && lastItem != null)
                {
                    GameObject book = lastItem;
                    foreach (Transform t in book.transform)
                    {
                        GlowItems glow = t.GetComponent<GlowItems>();
                        glow.ResetMat(t.gameObject);
                    }
                    lastItem = null;
                }
            }
            //
            //if (lastItem != null)
            //{
            //    if (lastItem.name.Equals("EvilBook"))
            //    {
            //        if (hit.collider.gameObject != lastItem && lastItem != null)
            //        {
            //            GameObject book = lastItem;
            //            foreach (Transform t in book.transform)
            //            {
            //                GlowItems glow = t.GetComponent<GlowItems>();
            //                glow.ResetMat(t.gameObject);
            //            }
            //            lastItem = null;
            //        }
            //    }
            //}
        }
    }

    public Dictionary<string, bool> GetKeyItems()
    {
        return ItemsPicked;
    }
}
