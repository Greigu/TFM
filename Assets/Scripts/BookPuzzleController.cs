using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookPuzzleController : MonoBehaviour
{

    private string[] correctOrder = { "Book_8", "Book_7", "Book_6", "Book_5", "Book_4", "Book_3", "Book_2", "Book_1" };
    private GameController gameController;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        audio = GetComponent<AudioSource>();
       GetOrderedBooks();
    }

    private Dictionary<GameObject, float> GetOrderedBooks()
    {
        Dictionary<GameObject, float> booksDictOrdered = new Dictionary<GameObject, float>();
        var n = GetBooks().OrderBy(entry => entry.Value);
        foreach (var item in n)
        {
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
            gameController.UnlockEnd();
            print("Correct Combination");
            audio.Play();
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
            booksDict.Add(item, a);
        }
        return booksDict;
    }
}
