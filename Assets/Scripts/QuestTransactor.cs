using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum QuestStatus { NotAvailable, Available, InProgress, Completed, Failed }

public class QuestTransactor : MonoBehaviour
{
    public PlayerQuestManager pqm;
    public NPCQuestManager npcQuestManager;
    public GameObject itemGroup;
    public QuestItemController itemPrefab;

    public TMP_Text questName;
    public TMP_Text questStatus;

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
        RemoveQuestItems();
        pqm.questItems.AddRange(GetListOfRewards());
        pqm.activeQuest = null;
        SetQuestStatus(QuestStatus.NotAvailable);
        questName.text = "Quest: ";
        Debug.Log("<color=yellow>Quest Completed!</color>");
    }

    private void RemoveQuestItems()
    {
        pqm.questItems.RemoveAll(questItem =>
        {
            if (questItem.item == pqm.activeQuest.objectiveItem)
            {
                Destroy(questItem.inventoryGameObject);
                return true;
            }

            return false;
        });
    }

    private void StartQuest()
    {
        pqm.activeQuest = npcQuestManager.offeredQuest;
        pqm.questStatus = QuestStatus.InProgress;

        Debug.Log("<color=green>Quest '" +
            pqm.activeQuest.questName + "' started.</color>");
        questName.text = "Quest: " + pqm.activeQuest.questName;
        SetQuestStatus(QuestStatus.InProgress);
    }

    public void AddQuestItem(ItemSO item)
    {
        if (pqm.activeQuest)
        {
            GameObject itemUIElement = AddItemToUI(item);
            pqm.questItems.Add(new InventoryItem(item, itemUIElement));
            CountQuestItems();
        }
    }

    public void RemoveQuestItem(int index)
    {
        Destroy(pqm.questItems[index].inventoryGameObject);
        pqm.questItems.RemoveAt(index);
        CountQuestItems();
    }

    private GameObject AddItemToUI(ItemSO item)
    {
        QuestItemController newItem = Instantiate(itemPrefab, itemGroup.transform);
        newItem.GetComponent<Image>().sprite = item.img;
        newItem.itemData = item;
        return newItem.gameObject;
    }

    private void CountQuestItems()
    {
        if (pqm.activeQuest == null) return;

         int itemCount  = pqm.questItems.Where(i =>  i.item == pqm.activeQuest.objectiveItem).Count();

        if (itemCount >= pqm.activeQuest.objectiveQuantity)
        {
            SetQuestStatus(QuestStatus.Completed);
        }
        else
        {
            SetQuestStatus(QuestStatus.InProgress);
        }
    }

    private void SetQuestStatus(QuestStatus status)
    {
        pqm.questStatus = status;
        questStatus.text = "Quest Status: " + pqm.questStatus.ToString();

        if(status == QuestStatus.Completed)
        Debug.Log("<color=blue>Quest objective met.</color>");
    }

    private List<InventoryItem> GetListOfRewards()
    {
        return pqm.activeQuest.rewardItems.Select(ri => {
            return new InventoryItem(ri, AddItemToUI(ri));
        }).ToList();
    }
}
