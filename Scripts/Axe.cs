using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Axe : MonoBehaviour
{
    [SerializeField] private TextMeshPro SmallTreeCount;
    [SerializeField] private TextMeshPro MediumTreeCount;
    [SerializeField] private TextMeshPro LargeTreeCount;

    private bool isColliding = false;
    private bool haveHit = false;
    private GameObject objectHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if "tree" is not in name (if not a tree)
        if (other.transform.name.IndexOf("tree") != -1)
        {
            isColliding = true;
            objectHit = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
    }

    private void Update()
    {
        // to avoid multiple collisions in the same attack
        if (!transform.parent.GetComponent<ToolParent>().IsAttacking)
        {
            haveHit = false;
        }

        if (!haveHit && isColliding && transform.parent.GetComponent<ToolParent>().IsAttacking)
        {
            haveHit = true;
            
            //Debug.Log(objectHit.name);

            // tree_stage_4(-4.5, -4.8, 0)(Health: 12) -> 12
            int treeHealth = int.Parse(objectHit.name[(objectHit.name.IndexOf("Health: ") + 7) .. (objectHit.name.Length - 1)]);
            treeHealth--;
            
            // tree_stage_4(-4.5, -4.8, 0)(Health: 12) -> tree_stage_4(-4.5, -4.8, 0)(Health: 11)
            objectHit.name = ( objectHit.name[0 .. (objectHit.name.IndexOf("Health: ") + 7)] + " " + treeHealth + ")" ).ToString();

            int rndFallingDirection = Random.Range(0, 2);
            if (treeHealth == 0)
            {
                if (rndFallingDirection == 0) // left
                {
                    objectHit.GetComponent<Animator>().SetTrigger("Tree Falling Left");
                }
                else // right
                {
                    objectHit.GetComponent<Animator>().SetTrigger("Tree Falling Right");
                }

                objectHit.GetComponent<BoxCollider2D>().enabled = false; // avoid any more hits while playing animation
                
                UpdateInventory();
            }
            else
            {
                objectHit.GetComponent<Animator>().GetComponent<Animator>().SetTrigger("Shake Tree");
            }
        }

        try
        { 
            // if tree has fallen and disappeared
            if (
                objectHit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("tree_falling_right_done")
                ||
                objectHit.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("tree_falling_left_done")
            )
            {
                Destroy(objectHit);
            } 
        } catch{}
    }

    private void UpdateInventory()
    {
        // tree_stage_2(-5.5, -1.8, 0)(Health: 0) -> tree_stage_2
        string treeName = objectHit.name[0 .. objectHit.name.IndexOf('(')];

        TextMeshPro TreeCount = SmallTreeCount;
        int currentTreeCount = 0;
        switch (treeName) {
            case "tree_stage_2":
                TreeCount = SmallTreeCount;
                Inventory.SmallTreeCount++;
                currentTreeCount = Inventory.SmallTreeCount;
                break;
            case "tree_stage_3":
                TreeCount = MediumTreeCount;
                Inventory.MediumTreeCount++;
                currentTreeCount = Inventory.MediumTreeCount;
                break;
            case "tree_stage_4":
                TreeCount = LargeTreeCount;
                Inventory.LargeTreeCount++;
                currentTreeCount = Inventory.LargeTreeCount;
                break;
        }

        //int currentTreeCount = int.Parse(TreeCount.text[(TreeCount.text.IndexOf(':') + 2) .. TreeCount.text.Length]);
        //currentTreeCount++;

        TreeCount.text = TreeCount.text[0 .. (TreeCount.text.IndexOf(':') + 2)] + currentTreeCount;
    }
}
