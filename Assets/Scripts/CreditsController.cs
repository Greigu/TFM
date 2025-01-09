using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public TMP_Text creditsText;
    public TMP_Text endingText;
    // Start is called before the first frame update
    private Dictionary<string, bool> items = new Dictionary<string, bool>();
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        int i = 0;
        int numGrab = 0;
        items = ApplicationModel.itemsGrabbed;
        string[] test = new string[items.Count];
        foreach (var item in items)
        {
            test[i] = item.Key + " - " + (item.Value ? "Grabbed": "Not Grabbed");
            i++;
            if(item.Value)
            {
                numGrab++;
            }
        }
        creditsText.text = test[0] + "\n" + test[1] + "\n" + test[2];
        endingText.text = numGrab == 3 ? "You destroyed all the artifacts" : "You didn't destroy all the artifacts";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
