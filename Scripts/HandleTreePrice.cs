using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandleTreePrice : MonoBehaviour
{
    [SerializeField] private GameObject StarObject;
    [SerializeField] private GameObject BuildingGrid;

    private int currentPrice = 100;
    TextMeshPro Text;

    private void Awake() 
    {
        Text = transform.parent.GetChild(0).GetComponent<TextMeshPro>();
    }

    int prevChildCount = 1;
    bool hasAddedStarPrice = false;
    private void FixedUpdate() 
    {
        if (Inventory.SmallTreeCount == 0 && Inventory.MediumTreeCount == 0 && Inventory.LargeTreeCount == 0)
        {
            currentPrice = 0;
        }
        else if (currentPrice == 0)
        {
            currentPrice = 100;
        }
        else
        {
            if (StarObject.activeSelf && !hasAddedStarPrice)
            {
                currentPrice += Random.Range(50, 100);
                hasAddedStarPrice = true;
            }
            if (prevChildCount < BuildingGrid.transform.childCount)
            {
                if (!(BuildingGrid.transform.childCount == 2 && prevChildCount == 1))
                {
                    currentPrice += Random.Range(5, 21);
                }
                prevChildCount = BuildingGrid.transform.childCount;
            }
        }

        Text.text = Text.text[0 .. (Text.text.IndexOf(':') + 2)] + currentPrice + "$";        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Inventory.SmallTreeCount > 0)
            {
                Inventory.SmallTreeCount--;
            }
            else if (Inventory.MediumTreeCount > 0)
            {
                Inventory.MediumTreeCount--;
            }
            else if (Inventory.LargeTreeCount > 0)
            {
                Inventory.LargeTreeCount--;
            }
            else
            {
                return;
            }
            
            Inventory.Money += currentPrice;
            currentPrice = 0;
            prevChildCount = 1;
            hasAddedStarPrice = false;
            StarObject.SetActive(false);

            try
            {
                try { BuildingGrid.transform.GetChild(0).gameObject.SetActive(false); } catch{}

                for (int i = 1; i < BuildingGrid.transform.childCount; i++)
                {
                    Destroy(BuildingGrid.transform.GetChild(i).gameObject);
                }
            }
            catch {}
        }
    }
}
