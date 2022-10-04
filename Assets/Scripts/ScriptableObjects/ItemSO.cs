using UnityEngine;

[CreateAssetMenu (menuName = "Item/Create Item", fileName ="Item_", order = 1)]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public int itemValue;
}
