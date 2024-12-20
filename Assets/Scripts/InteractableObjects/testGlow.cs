using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGlow : MonoBehaviour // Per a que funcioni, s'ha de canviar el color de emissió
{
    private GameObject cam;
    private bool isLooking = false;
    private GameObject lastItem;
    public Color emisionC;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        CheckIfGlow();
    }

    private void ResetMat()
    {
        lastItem.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        foreach (Material mat in lastItem.GetComponent<MeshRenderer>().materials)
        {
            mat.DisableKeyword("_EMISSION");
        }
        isLooking = false;
        lastItem = null;
    }

    private void CheckIfGlow()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20f);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.GetComponent<testGlow>() != null)
            {
                lastItem = hit.collider.gameObject;
                isLooking = true;
                foreach (Material mat in hit.collider.gameObject.GetComponent<MeshRenderer>().materials)
                {
                    mat.SetColor("_EmissionColor", emisionC);
                    mat.EnableKeyword("_EMISSION");
                }
            }
            if(hit.collider.gameObject.Equals(lastItem)) 
            {
                isLooking = true;
            }
            else if(lastItem != null)
            {
               ResetMat();
            }
        } else
        {
            if (isLooking)
            {
                ResetMat();
            }
        }
    }
}
