using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus { NotAvailable, Available, InProgress, Completed, Failed }

public class QuestTransactor : MonoBehaviour
{
    public PlayerQuestManager pqm;
    public NPCQuestManager npcQuestManager;

    public void RequestQuest()
    {
        if (pqm.activeQuest == null)
        {
            pqm.activeQuest = npcQuestManager.offeredQuest;
            pqm.questStatus = QuestStatus.InProgress;

            Debug.Log("<color=green>Quest '" +
                pqm.activeQuest.questName + "' started.</color>");
        }
        else
        {
            if (pqm.questStatus == QuestStatus.Completed)
            {
                pqm.questItems.AddRange(pqm.activeQuest.rewardItems);
                pqm.questItems.RemoveAll(item => item == pqm.activeQuest.objectiveItem);
                pqm.activeQuest = null;
                pqm.questStatus = QuestStatus.NotAvailable;

                Debug.Log("<color=yellow>Quest Completed!</color>");
            }
            else
            {
                Debug.Log("<color=red>Finish your active quest first!</color>");
            }
        }
    }

    public void AddQuestItem(ItemSO item)
    {
        if (pqm.activeQuest)
        {
            pqm.questItems.Add(item);
            CountQuestItems();
        }
    }

    private void CountQuestItems()
    {
        int itemCount = 0;
        foreach(ItemSO item in pqm.questItems)
        {
            if (item == pqm.activeQuest.objectiveItem)
            {
                itemCount += 1;
            }
        }

        if (itemCount >= pqm.activeQuest.objectiveQuantity)
        {
            pqm.questStatus = QuestStatus.Completed;
            Debug.Log("<color=blue>Quest objective met.</color>");
        }
    }
}
