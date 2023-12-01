using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private GameObject DecorationMenu;
    [SerializeField] private GameObject[] DecorationPlaceholders;
    [SerializeField] private GameObject[] DecorationObjects;
    [SerializeField] private GameObject StarObject;
    [SerializeField] private Grid Grid;
    [SerializeField] private GameObject CancelBtn;
    [SerializeField] private GameObject SellArea;

    private GameObject CurrentBuildingPlaceholder;
    private GameObject CurrentBuildingObject;
    private GameObject PrefabPlaceholder;

    private void Awake()
    {
        CancelBtn.SetActive(false);
        StarObject.SetActive(false);
    }

    private void Update() 
    {
        if (Inventory.SmallTreeCount == 0 && Inventory.MediumTreeCount == 0 && Inventory.LargeTreeCount == 0)
        {
            return;
        }

        int index = GetCurrentBuildingPrefab();
        if (index != -1)
        {
            if (DecorationPlaceholders[index].transform.name == "Star")
            {
                if (Inventory.Money >= DecorationPrice.Star)
                {
                    if (!StarObject.activeSelf)
                    {
                        StarObject.SetActive(true);
                        Inventory.Money -= DecorationPrice.Star;
                    }
                }
                else
                {
                    CurrentBuildingPlaceholder = null;
                }
            }
            else
            {
                CurrentBuildingPlaceholder = DecorationPlaceholders[index];
                CurrentBuildingObject = DecorationObjects[index];

                CancelBtn.SetActive(true);
            }
        }

        SpawnPrefabPlaceholder();

        SpawnPrefabObject();

        UpdatePrefabPlaceholderPosition();

        UpdateVisualPlaceability();

        UpdateUI();
    }

    /// <summary>
    /// <para>Listens for mouse left click.</para>
    /// <para>When mouse clicks on one of the decoration options</para>
    /// <para>it gets the index of the decoration</para>
    /// </summary>
    /// <returns>The index of the selected decoration, but -1 if no decoration is selected</returns>
    private int GetCurrentBuildingPrefab()
    {
        // only run if we dont have a christmas tree decoration selected
        if (CurrentBuildingPlaceholder != null)
        {
            return -1;
        }

        // only run on mouse left click
        if (!Input.GetMouseButtonDown(0))
        {
            return -1;
        }

        // looks through all children for the one that is highlighted
        for (int i = 0; i < DecorationMenu.transform.childCount; i++)
        {
            if (DecorationMenu.transform.GetChild(i).GetChild(1).GetComponent<SpriteRenderer>().color.a == 0.33f)
            {
                return i;
            }
        }
        
        return -1;
    }

    private void SpawnPrefabPlaceholder()
    {
        // only run if we do have a christmas tree decoration selected
        if (CurrentBuildingPlaceholder == null)
        {
            return;
        }

        // only run if we dont have a prefab placeholder already
        if (PrefabPlaceholder != null)
        {
            return;
        }

        if (!IsAffordable())
        {
            return;
        }

        PrefabPlaceholder = Instantiate(CurrentBuildingPlaceholder, Vector3.zero, Quaternion.identity);
        PrefabPlaceholder.transform.parent = Grid.transform;
    }

    private void SpawnPrefabObject()
    {
        // only run if we do have a christmas tree decoration selected
        if (CurrentBuildingPlaceholder == null)
        {
            return;
        }

        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (!IsPrefabPlaceholderPlaceable())
        {
            return;
        }

        GameObject PrefabObject = Instantiate(CurrentBuildingObject, Grid.CellToWorld(Grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition))), Quaternion.identity);
        PrefabObject.transform.position = new Vector3(PrefabObject.transform.position.x, PrefabObject.transform.position.y, 0);
        PrefabObject.transform.parent = Grid.transform;

        // price
        int curPrice = 0;
        switch (CurrentBuildingPlaceholder.name.Replace("Placeholder", "")) {
            case "RedBall":
                curPrice = 10;
                break;
            case "PurpleBall":
                curPrice = 10;
                break;
            case "GreenBall":
                curPrice = 10;
                break;
            case "BlueBall":
                curPrice = 10;
                break;
            case "CandyCane":
                curPrice = 20;
                break;
            case "Heart":
                curPrice = 25;
                break;
            case "Star":
                curPrice = 50;
                break;
        }
        Inventory.Money -= curPrice;
    }

    private bool IsAffordable()
    {
        int curPrice = 0;
        switch (CurrentBuildingPlaceholder.name.Replace("Placeholder", "")) {
            case "RedBall":
                curPrice = 10;
                break;
            case "PurpleBall":
                curPrice = 10;
                break;
            case "GreenBall":
                curPrice = 10;
                break;
            case "BlueBall":
                curPrice = 10;
                break;
            case "CandyCane":
                curPrice = 20;
                break;
            case "Heart":
                curPrice = 25;
                break;
            case "Star":
                curPrice = 50;
                break;
        }

        if (curPrice <= Inventory.Money)
        {
            return true;
        }

        Cancel();

        return false;
    }

    private Vector3 PrevGridPosition = new(0, 0, 0);
    private void UpdatePrefabPlaceholderPosition()
    {
        // only run if we do have a prefab placeholder already
        if (PrefabPlaceholder == null)
        {
            return;
        }

        Vector3Int currentGridPosition = Grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        // have mouse/tile position not changed?
        if (PrevGridPosition == currentGridPosition) 
        {
            return;
        }
        PrevGridPosition = currentGridPosition;

        PrefabPlaceholder.transform.position = Grid.CellToWorld(new Vector3Int(currentGridPosition.x, currentGridPosition.y, 0));
    }

    private bool IsPrefabPlaceholderPlaceable()
    {
        // only run if we do have a prefab placeholder already
        if (PrefabPlaceholder == null)
        {
            return false;
        }

        if (!IsAffordable())
        {
            return false;
        }

        bool IsOnTree = false;
        bool IsPlaceable = true;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            //if (hit.transform.name == "CancelBtn")
            //{
            //    CurrentBuildingPlaceholder = null;
            //    CurrentBuildingObject = null;
            //    Destroy(PrefabPlaceholder);
            //    return false;
            //}
            if (hit.transform.name == "Tree")
            {
                IsOnTree = true;
            }
            else
            {
                IsPlaceable = false;
            }
        }

        if (IsOnTree && IsPlaceable)
        {
            return true;
        }
        return false;
    }

    private void UpdateVisualPlaceability()
    {
        // only run if we do have a prefab placeholder already
        if (PrefabPlaceholder == null)
        {
            return;
        }

        // change color of placeholder background based on placeability
        if (IsPrefabPlaceholderPlaceable())
        {
            PrefabPlaceholder.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
        }
        else
        {
            PrefabPlaceholder.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }
    }

    private void UpdateUI()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (CurrentBuildingPlaceholder != null)
        {
            // move away but still running
            SellArea.transform.position = new Vector3(SellArea.transform.position.x, SellArea.transform.position.y, -50);
        }
        
        if (!CancelBtn.activeSelf)
        {
            SellArea.transform.position = new Vector3(SellArea.transform.position.x, SellArea.transform.position.y, 0);
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.name == "CancelBtn")
            {
                Cancel();

                SellArea.transform.position = new Vector3(SellArea.transform.position.x, SellArea.transform.position.y, 0);
            }
        }
    }

    private void Cancel()
    {
        CurrentBuildingPlaceholder = null;
        CurrentBuildingObject = null;
        Destroy(PrefabPlaceholder);
        CancelBtn.SetActive(false);
    }
}
