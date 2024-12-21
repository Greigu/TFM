using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLight : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        transform.Rotate(-45 * Time.deltaTime,0 , 0); 
    }
}
