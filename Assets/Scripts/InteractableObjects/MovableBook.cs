using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBook : MonoBehaviour
{
    public GameObject positions;
    public GameObject books;
    private GameObject cam;
    private List<GameObject> positionsList = new List<GameObject>();
    private List<GameObject> booksList = new List<GameObject>();
    private GameObject player;
    private int firstBook = 0;
    private int secondBook = 0;
    private bool isBookSelected = false;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Transform child in positions.transform)
        {
            positionsList.Add(child.transform.gameObject);
        }
        foreach (Transform child in books.transform)
        {
            booksList.Add(child.transform.gameObject);
        }
        print(booksList.Count);
       
    }

    private void Update()
    {
        ChooseBook();
    }

    private void ChooseBook()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Cambia "E" si necesitas otro botón.
        {
            RaycastHit[] hits = Physics.RaycastAll(player.transform.position, cam.transform.forward, 200f);//Canviar el forward per la direcció de la càmera.
            foreach (RaycastHit hit in hits)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.tag.Equals("Book") && !isBookSelected)
                {
                    Debug.Log("a");
                    for (int i = 0; i < booksList.Count; i++)
                    {
                        if (hit.collider.gameObject.Equals(booksList[i].gameObject))
                        {
                            firstBook = i;
                            isBookSelected = true;
                            Debug.Log(i);
                        }
                    }
                }else if(hit.collider.tag.Equals("Book") && isBookSelected)
                {
                    for (int i = 0; i < booksList.Count; i++)
                    {
                        if (hit.collider.gameObject.Equals(booksList[i].gameObject))
                        {
                            secondBook = i;
                            Debug.Log(i);
                        }
                    }
                    ChangeBook(firstBook, secondBook);
                }
            }
        }
    }
    private void ChangeBook(int i1, int i2)
    {
        GameObject aux = booksList[i1];
        booksList[i1] = booksList[i2];
        booksList[i2] = aux;
        booksList[i1].transform.position = positionsList[i1].transform.position;
        booksList[i2].transform.position = positionsList[i2].transform.position;
        isBookSelected = false;
        
    }
}
