using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public GameObject playerCam;
    static public Dictionary<string, bool> itemsGrabbed = new Dictionary<string, bool>();
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        print(1);
        if (other.gameObject.CompareTag("Player"))
        {
            print(2);
            itemsGrabbed = playerCam.GetComponent<PickItem>().GetKeyItems();
            SceneManager.LoadScene("CreditsScene");
        }
    }
}
