using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateMoneyText : MonoBehaviour
{
    private TextMeshPro Text;

    private void Awake() 
    {
        Text = transform.GetChild(0).GetChild(0).GetComponent<TextMeshPro>();

        Text.text = Text.text[0 .. (Text.text.IndexOf(':') + 2)] + Inventory.Money + "$";
    }

    int prevMoneyAmount = 0;
    private void FixedUpdate() 
    {
        if (Inventory.Money != prevMoneyAmount)
        {
            Text.text = Text.text[0 .. (Text.text.IndexOf(':') + 2)] + Inventory.Money + "$";
            prevMoneyAmount = Inventory.Money;
        }
    }
}
