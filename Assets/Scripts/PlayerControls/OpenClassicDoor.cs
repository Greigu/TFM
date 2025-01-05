using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClassicDoor : MonoBehaviour
{
    private GameObject door;
    private GameObject cam;
    private float rotationSpeed = 45f;
    private bool isOpen = false;
    private Quaternion targetRotation = Quaternion.Euler(new Vector3(0, -90, 0));
    private Quaternion initialRotation;
    private bool isOpening = false;
    private bool isClosing = false;

    // Start is called before the first frame update
    void Start()
    {
        door = this.gameObject;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        initialRotation = transform.localRotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        //OpenDoor();
        if(isOpening)
        {
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            this.isOpen = true;
        }
        else if(isClosing)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, initialRotation, rotationSpeed * Time.deltaTime);
            //this.isOpen = false;
        }
    }

    //private void OpenDoor()
    //{
    //    if (Input.GetKeyDown(KeyCode.E)) // Cambia "E" si necesitas otro botón.
    //    {
    //        RaycastHit hit;
    //        Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f);
    //        if(hit.collider != null)
    //        {
    //            Debug.Log(hit.collider.gameObject.name);
    //            if (hit.collider.gameObject.Equals(door))
    //            {
    //                StartCoroutine(RotateDoor(isOpen));
    //            }
    //        }
    //    }
    //}

    IEnumerator RotateDoor(bool isOpen)
    {
        print(2 + "_" + isOpen);
        if (!isOpen)
        {
            print(3);
            isOpening = true;
            yield return new WaitForSeconds(2);
            isOpening = false;

        }
        else
        {
            //isClosing = true;
            //yield return new WaitForSeconds(2);
            //isClosing = false;
        }
        yield return null;
    }

    public void _OpenDoor()
    {
        print(1);
        StartCoroutine(RotateDoor(isOpen));
    }
}
