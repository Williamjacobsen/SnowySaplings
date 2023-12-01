using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleQuest : MonoBehaviour
{
    private TextMeshPro QuestText;

    private bool HaveGatherTreeQuestRan = false;

    private void Awake() 
    {
        QuestText = GetComponent<TextMeshPro>();    
        
        QuestInactive();
    }

    private void FixedUpdate()
    {
        //SceneManager.GetActiveScene().name == "Main" && 
        if (!Quests.GatherTreeQuestPlayed && !HaveGatherTreeQuestRan && Inventory.SmallTreeCount == 0 && Inventory.MediumTreeCount == 0 && Inventory.LargeTreeCount == 0)
        {
            StartCoroutine(GatherTreeQuest());
        }

        if (Quests.GatherTreeQuestPlayed && !Quests.EnterTreeWorkshopPlayed && SceneManager.GetActiveScene().name == "House")
        {
            QuestText.text = "Quest:\nEnter Tree Workshop\nby walking up to christmas tree\nand pressing E";
            QuestText.fontSize = 3.5f;
            Quests.EnterTreeWorkshopPlayed = true;
            QuestActive();
        }
        
    }

    private void QuestActive()
    {
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, 50);
    }

    private void QuestInactive()
    {
        transform.parent.transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, -50);
    }

    private IEnumerator GatherTreeQuest()
    {
        HaveGatherTreeQuestRan = true;

        //yield return new WaitForSeconds(5);

        QuestText.text = "Quest:\nCollect a tree of each height";
        QuestActive();

        while (Inventory.SmallTreeCount == 0 || Inventory.MediumTreeCount == 0 || Inventory.LargeTreeCount == 0) 
        {
            yield return new WaitForSeconds(1);    
        }

        QuestText.text = "Quest:\nCongratulations!\nQuest Completed";

        Quests.GatherTreeQuestPlayed = true;

        yield return new WaitForSeconds(5);

        QuestText.text = "Quest:\nEnter house by pressing E\nIn front of the house";

    }
}
