using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trees : MonoBehaviour
{
    [SerializeField] private GameObject[] tree; // array of tree stages (0-1 is sapling, 2-4 is grown)
    [SerializeField] private GameObject snowman;

    private void Awake()
    {
        for (int i = 0; i < 250; i++)
        {
            SpawnRandomTree();
        }

        for (int i = 0; i < 50; i++)
        {
            SpawnSnowman();
        }
    }

    private void SpawnRandomTree()
    {
        float rndXCoordinate = 0;
        float rndYCoordinate = 0;

        int rndTreeStage = Random.Range(2, 5);
        
        // avoid location of house
        while (rndXCoordinate > -3 && rndXCoordinate < 3 && rndYCoordinate > -11 && rndYCoordinate < 3)
        {
            rndXCoordinate = Random.Range(-25, 26) + 0.5f;
            rndYCoordinate = Random.Range(-25, 26) + 0.2f;
        }

        GameObject rndTree = Instantiate(tree[rndTreeStage], new Vector3(rndXCoordinate, rndYCoordinate, 0), Quaternion.identity, transform);
        string rndTreeCoordinates = $"({rndXCoordinate}, {rndYCoordinate}, 0)";
        
        int baseHealthMultiplier = 3;
        string rndTreeName = $"tree_stage_{rndTreeStage}{rndTreeCoordinates}(Health: {baseHealthMultiplier * rndTreeStage})";      

        for (int i = 0; i < transform.childCount; i++)
        {
            // "tree_stage_0(0, 0, 0)" -> (0, 0, 0)  
            string childCoordinates = transform.GetChild(i).name[transform.GetChild(i).name.IndexOf("(")..(transform.GetChild(i).name.IndexOf(")") + 1)];

            // if a tree already exists on that tile
            if (childCoordinates == rndTreeCoordinates)
            {
                Destroy(rndTree);
            }
        }
        try { rndTree.name = rndTreeName; } catch{} // error if we deleted gameobject (in for loop above)
    }

    private void SpawnSnowman()
    {
        float rndXCoordinate = 0;
        float rndYCoordinate = 0;
        
        // avoid location of house
        while (rndXCoordinate > -3 && rndXCoordinate < 3 && rndYCoordinate > -11 && rndYCoordinate < 3)
        {
            rndXCoordinate = Random.Range(-25, 26) + 0.5f;
            rndYCoordinate = Random.Range(-25, 26) + 0.2f;
        }

        GameObject rndSnowman = Instantiate(snowman, new Vector3(rndXCoordinate, rndYCoordinate, 0), Quaternion.identity, transform);
        string rndSnowmanCoordinates = $"({rndXCoordinate}, {rndYCoordinate}, 0)";
        
        string rndSnowmanName = $"snowman{rndSnowmanCoordinates}";      

        for (int i = 0; i < transform.childCount; i++)
        {
            // "tree_stage_0(0, 0, 0)" -> (0, 0, 0)  
            string childCoordinates = transform.GetChild(i).name[transform.GetChild(i).name.IndexOf("(")..(transform.GetChild(i).name.IndexOf(")") + 1)];

            // if a tree already exists on that tile
            if (childCoordinates == rndSnowmanCoordinates)
            {
                Destroy(rndSnowman);
            }
        }
        try { rndSnowman.name = rndSnowmanName; } catch{} // error if we deleted gameobject (in for loop above)
    }
}
