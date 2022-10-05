using UnityEngine;

public class QuestItemController : MonoBehaviour
{
    public ItemSO itemData;
    public bool isPickup = false;
    private QuestTransactor _questTransactor;
    void Start()
    {
        _questTransactor = FindObjectOfType<QuestTransactor>();
    }
    public void ClickItem()
    {
        if (isPickup)
        {
            AddToInventory();
        }
        else
        {
            RemoveItemFromInventory();
        }
    }

    private void AddToInventory()
    {
        _questTransactor.AddQuestItem(itemData);
    }
    private void RemoveItemFromInventory()
    {
        int itemIndex = transform.GetSiblingIndex();
        _questTransactor.RemoveQuestItem(itemIndex);
        Destroy(gameObject);
    }

}
