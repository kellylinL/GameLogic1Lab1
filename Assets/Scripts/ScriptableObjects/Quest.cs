using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Quest/Create Quest", fileName = "Quest_", order = 1)]
public class Quest : ScriptableObject
{
    public string questName;
    public string questDescription;

    public ItemSO objectiveItem;
    public int objectiveQuantity;

    public List<ItemSO> rewardItems;
}
