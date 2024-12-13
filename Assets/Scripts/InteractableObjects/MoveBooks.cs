using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBooks : MonoBehaviour
{
    private GameObject player;
    private GameObject cam;
    private GameObject bookOne;
    private GameObject bookTwo;
    private Vector3 posOne;
    private Vector3 posTwo;
    private bool isBookSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        ChooseBook();
    }

    private void ChooseBook()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Cambia "E" si necesitas otro bot�n.
        {
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f);
            //Debug.Log(hit.collider.gameObject.name);
            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag.Equals("Book") && !isBookSelected)
                {
                    bookOne = hit.collider.gameObject;
                    isBookSelected = true;
                    Debug.Log("Found book 1");

                }
                else if (hit.collider.gameObject.tag.Equals("Book") && isBookSelected)
                {
                    bookTwo = hit.collider.gameObject;
                    ChangePos(bookOne, bookTwo);
                    Debug.Log("Found book 2");
                }
                else
                {
                    ResetBooks();
                }
            } else
            {
                ResetBooks();
            }
            
            //RaycastHit[] hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, 200f);
            //foreach (RaycastHit hit in hits)
            //{
            //    Debug.Log(hit.collider.gameObject.name);
            //    if (hit.collider.tag.Equals("Book") && !isBookSelected)
            //    {
            //        bookOne = hit.collider.gameObject;
            //        isBookSelected = true;
            //    }
            //    else if (hit.collider.gameObject.tag.Equals("Book") && isBookSelected)
            //    {
            //        bookTwo = hit.collider.gameObject;
            //        ChangePos(bookOne, bookTwo);
            //    }
            //}
        }
    }

    private void ChangePos(GameObject b1, GameObject b2)
    {
        posOne = b1.transform.position; 
        posTwo = b2.transform.position;
        b1.transform.position = posTwo;
        b2.transform.position = posOne;
        isBookSelected = false;
    }

    private void ResetBooks()
    {
        isBookSelected = false;
        Debug.Log("No book found");
        bookOne = null;
        bookTwo = null;
    }
}
