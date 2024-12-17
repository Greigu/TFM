using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class testGlow : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;
    public Material glow;
    private Material normal;
    private bool isLooking = false;
    private GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        normal = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGlow();
    }

    private void CheckIfGlow()
    {
        RaycastHit hit;
        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f);
        if (hit.collider != null)
        {
            if(hit.collider.gameObject.GetComponent<testGlow>() != null)
            {
                test = hit.collider.gameObject;
                isLooking = true;
                //Afegir Material glow i treurel cuan es deixa de apuntar
                hit.collider.gameObject.GetComponent<MeshRenderer>().material = glow;
                //materials[1] = glow;
                //hit.collider.gameObject.GetComponent<MeshRenderer>().materials = materials;

            }
            if(hit.collider.gameObject.Equals(test)) 
            {
                print("looking");
                isLooking = true;
            }
            else if(test != null)
            {
                print("No looking");
                test.GetComponent<MeshRenderer>().material = normal;
                isLooking = false;
                test = null;
            }
        }
    }
}
