using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private TMP_Text textToDisplayUI;
    private TMP_Text textToDisplaySpeak;
    private GameObject textUI;
    private GameObject textSpeak;
    public int waitSeconds = 3;
    public bool isReading = false;
    // Start is called before the first frame update
    void Start()
    {
        textUI = GameObject.Find("TextRead");
        textSpeak = GameObject.Find("TextSpeak");
        textToDisplayUI = textUI.GetComponent<TMP_Text>();
        textToDisplaySpeak = textSpeak.GetComponent<TMP_Text>();
        textUI.transform.parent.gameObject.SetActive(false);
        textSpeak.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (isReading && Input.GetKeyDown(KeyCode.E)){ //Press button to cancel
        //    isReading = false;
        //    textUI.transform.parent.gameObject.SetActive(false);
        //    textSpeak.transform.parent.gameObject.SetActive(false);
        //}
    }

    public void ChangeText(string text, bool isSpeak)
    {
        StartCoroutine(TimeToRead(waitSeconds));// Time to cancel
        isReading = true;
        if (isSpeak)
        {
            textSpeak.transform.parent.gameObject.SetActive(true);
            textToDisplaySpeak.text = text;
        } else
        {
            textUI.transform.parent.gameObject.SetActive(true);
            textToDisplayUI.text = text;
        }
        
    }
    IEnumerator TimeToRead(int count)
    {
        yield return new WaitForSeconds(count);
        isReading = false;
        textUI.transform.parent.gameObject.SetActive(false);
        textSpeak.transform.parent.gameObject.SetActive(false);
    }
}
