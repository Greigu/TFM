using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.Progress;

public class GlowItems : MonoBehaviour
{
    public Color emisionC;
   
    public void ResetMat(GameObject item)
    {
        item.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        foreach (Material mat in item.GetComponent<MeshRenderer>().materials)
        {
            mat.DisableKeyword("_EMISSION");
        }
    }

    public void ChangeMat(GameObject item)
    {
        foreach (Material mat in item.GetComponent<MeshRenderer>().materials)
        {
            mat.SetColor("_EmissionColor", emisionC);
            mat.EnableKeyword("_EMISSION");
        }
    }
}
