using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleInventory : MonoBehaviour
{
    TextMeshPro LargeTreeText;
    TextMeshPro MediumTreeText;
    TextMeshPro SmallTreeText;

    private void Awake()
    {
        LargeTreeText = transform.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshPro>();
        MediumTreeText = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshPro>();
        SmallTreeText = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshPro>();

        try{ LargeTreeText.text = LargeTreeText.text[0 .. (LargeTreeText.text.IndexOf(':') + 2)] + Inventory.LargeTreeCount; } catch{}
        try{ MediumTreeText.text = MediumTreeText.text[0 .. (MediumTreeText.text.IndexOf(':') + 2)] + Inventory.MediumTreeCount; } catch{}
        try{ SmallTreeText.text = SmallTreeText.text[0 .. (SmallTreeText.text.IndexOf(':') + 2)] + Inventory.SmallTreeCount; } catch{}
    }

    private void Update()
    {
        try{ LargeTreeText.text = LargeTreeText.text[0 .. (LargeTreeText.text.IndexOf(':') + 2)] + Inventory.LargeTreeCount; } catch{}
        try{ MediumTreeText.text = MediumTreeText.text[0 .. (MediumTreeText.text.IndexOf(':') + 2)] + Inventory.MediumTreeCount; } catch{}
        try{ SmallTreeText.text = SmallTreeText.text[0 .. (SmallTreeText.text.IndexOf(':') + 2)] + Inventory.SmallTreeCount; } catch{}
    }
}
