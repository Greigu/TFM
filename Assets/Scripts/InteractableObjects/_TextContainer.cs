using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _TextContainer: MonoBehaviour
{
    public string text;
    public string GetText()
    {
        string modText = text.Replace("#", "\n");
        return modText;
    }
}
