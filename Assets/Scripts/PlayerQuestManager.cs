using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour
{
    public Quest activeQuest;
    public QuestStatus questStatus;
    public List<InventoryItem> questItems = new List<InventoryItem>();
}
