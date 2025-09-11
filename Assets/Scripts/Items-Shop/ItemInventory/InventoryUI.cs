using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public enum ItemsType
{
    none, mango, rice, green
}
[System.Serializable]
public class InventorySlot
{
    public Image image;
    public ItemsType type = ItemsType.none;
    public TMP_Text timerText;
}
public class InventoryUI : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots;
    [SerializeField] Sprite emptySprite;
    public int SlotCount => slots.Count;

    public bool AddItems(Sprite itemSprite, ItemsType type)
    {
        foreach(InventorySlot slot in slots) 
        {
            if(slot.type == ItemsType.none)
            {
                slot.image.sprite = itemSprite;
                slot.type = type;
                return true;
            }
        }
        Debug.Log("Items Full");
        return false;
    }
    public ItemsType GetItemType(int index)
    {
        if (index >= 0 && index < slots.Count)
            return slots[index].type;
        return ItemsType.none;
    }
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < slots.Count)
        {
            slots[index].image.sprite = emptySprite;
            slots[index].type = ItemsType.none;
        }
    }
    public InventorySlot GetSlot(int index)
    {
        if(index >= 0 && index < slots.Count)
            return slots[index];
        return null;
    }
}
