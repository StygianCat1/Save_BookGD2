using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ArrowChangeSetUp : MonoBehaviour
{
    public List<GameObject> arrows;

    //The reset arrow, that change them all to white
    public void ResetAllArrows()
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.GetComponent<Image>().color = Color.white;
        }
    }
}
