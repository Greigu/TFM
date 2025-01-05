using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookPuzzleController : MonoBehaviour
{

    private string[] correctOrder = { "1", "4", "3", "2", "5" }; //Canviar per l'ordre desitjat
    // Start is called before the first frame update
    void Start()
    {
       GetOrderedBooks();
    }

    private Dictionary<GameObject, float> GetOrderedBooks()
    {
        Dictionary<GameObject, float> booksDictOrdered = new Dictionary<GameObject, float>();
        var n = GetBooks().OrderBy(entry => entry.Value);
        foreach (var item in n)
        {
            print(item.Key.name + "-" + item.Value);
            booksDictOrdered.Add(item.Key, item.Value);
        }
        return booksDictOrdered;
    }

    public void CheckIfCompleted()
    {
        Dictionary<GameObject, float> actualOrder = GetOrderedBooks();
        int i = 0;
        bool isCorrect = true;
        foreach (var item in actualOrder)
        {
            if (!item.Key.name.Equals(correctOrder[i])){
                isCorrect = false;
            }
            i++;
        }
        if (isCorrect)
        {
            print("Correct Combination");
        }
    }

    private Dictionary<GameObject, float> GetBooks()
    {
        List<GameObject> books = new List<GameObject>();
        Dictionary<GameObject, float> booksDict = new Dictionary<GameObject, float>();
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 5f);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag.Equals("Book"))
            {
                books.Add(hit.collider.gameObject);

            }

        }
        foreach (GameObject item in books)
        {
            float a = Vector3.Distance(transform.position, item.transform.position);
            //print(item.name + "-" + Vector3.Distance(transform.position, item.transform.position));
            booksDict.Add(item, a);
        }
        return booksDict;
    }
}
