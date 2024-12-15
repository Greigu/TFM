using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private TMP_Text m_TextMeshPro;
    private GameObject textUI;
    public bool isReading = false;
    // Start is called before the first frame update
    void Start()
    {
        textUI = GameObject.Find("TextDisplay");
        m_TextMeshPro = textUI.GetComponent<TMP_Text>();
        textUI.transform.parent.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isReading && Input.GetKeyDown(KeyCode.E)){
            isReading = false;
            textUI.transform.parent.gameObject.SetActive(false);
        }
    }

    public void ChangeText(string text)
    {
        isReading = true;
        textUI.transform.parent.gameObject.SetActive(true);
        Debug.Log(text);
        Debug.Log(m_TextMeshPro);
        m_TextMeshPro.text = text;
    }
}
