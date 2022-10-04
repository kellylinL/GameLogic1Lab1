using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            CountQuestItems();
        }
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
