using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum QuestStatus { NotAvailable, Available, InProgress, Completed, Failed }

public class QuestTransactor : MonoBehaviour
{
    public PlayerQuestManager pqm;
    public NPCQuestManager npcQuestManager;
    public GameObject itemGroup;
    public QuestItemController itemPrefab;

    public void RequestQuest()
    {
        if (pqm.activeQuest == null)
        {
            StartQuest();
        }
        else
        {
            if (pqm.questStatus == QuestStatus.Completed)
            {
                CompleteQuest();
            }
            else
            {
                Debug.Log("<color=red>Finish your active quest first!</color>");
            }
        }
    }

    private void CompleteQuest()
    {
        pqm.questItems.AddRange(pqm.activeQuest.rewardItems);
        pqm.questItems.RemoveAll(item => item == pqm.activeQuest.objectiveItem);
        pqm.activeQuest = null;
        pqm.questStatus = QuestStatus.NotAvailable;

        Debug.Log("<color=yellow>Quest Completed!</color>");
    }

    private void StartQuest()
    {
        pqm.activeQuest = npcQuestManager.offeredQuest;
        pqm.questStatus = QuestStatus.InProgress;

        Debug.Log("<color=green>Quest '" +
            pqm.activeQuest.questName + "' started.</color>");
    }

    public void AddQuestItem(ItemSO item)
    {
        if (pqm.activeQuest)
        {
            pqm.questItems.Add(item);
            AddItemToUI(item);
            CountQuestItems();
        }
    }

    public void RemoveQuestItem(int index)
    {
        pqm.questItems.RemoveAt(index);
    }

    private void AddItemToUI(ItemSO item)
    {
        QuestItemController newItem = Instantiate(itemPrefab, itemGroup.transform);
        newItem.GetComponent<Image>().sprite = item.img;
        newItem.itemData = item;
    }

    private void CountQuestItems()
    {
        int itemCount = pqm.questItems.Select(i => i == pqm.activeQuest.objectiveItem).Count();

        if (itemCount >= pqm.activeQuest.objectiveQuantity)
        {
            pqm.questStatus = QuestStatus.Completed;
            Debug.Log("<color=blue>Quest objective met.</color>");
        }
    }
}
