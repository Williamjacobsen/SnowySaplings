using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightBackground : MonoBehaviour
{
    void OnMouseOver()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.33f);
    }

    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
    }
}
