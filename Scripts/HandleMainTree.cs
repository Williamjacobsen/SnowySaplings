using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMainTree : MonoBehaviour
{
    private void Awake() 
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    private void Update() 
    {
        if (Inventory.SmallTreeCount == 0 && Inventory.MediumTreeCount == 0 && Inventory.LargeTreeCount == 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.1f, 0.1f, 0.1f, 0.5f);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
