using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    private const int MAXANGLE = 0;
    private const int MINANGLE = 0;
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(-1,0,0), speed);
        if(transform.rotation.x >= 0 && transform.rotation.x <= 180)
        {
            GetComponent<Light>().enabled = true;
            
        }
        else
        {
            GetComponent<Light>().enabled = false;
        }
    }
}
