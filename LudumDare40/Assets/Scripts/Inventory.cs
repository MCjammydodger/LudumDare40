using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public struct InventorySlot
    {
        public string itemName;
        public int quantity;
    }

    private List<InventorySlot> items;
    private int inventoryCapacity = 18;

	// Use this for initialization
	private void Start () {
        items = new List<InventorySlot>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyUp(KeyCode.I))
        {
            MouseInteraction.instance.HideItemInfo(MouseInteraction.instance.GetCurrentItem());
            GameObject inventoryUI = GameManager.instance.GetInventoryUI().gameObject;
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public bool AddToInventory(Item item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemName == item.itemName)
            {
                InventorySlot newSlot = new InventorySlot();
                newSlot.itemName = item.itemName;
                newSlot.quantity = items[i].quantity + 1;
                items[i] = newSlot;
                return true;
            }
        }
        if (items.Count == inventoryCapacity)
        {
            return false;
        }
        InventorySlot slot = new InventorySlot();
        slot.itemName = item.itemName;
        slot.quantity = 1;
        items.Add(slot);
        
        return true;
    }

    public bool RemoveFromInventory(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == itemName)
            {
                if (items[i].quantity > 1)
                {
                    InventorySlot slot = new InventorySlot();
                    slot.itemName = itemName;
                    slot.quantity = items[i].quantity - 1;
                    items[i] = slot;
                }
                else
                {
                    items.RemoveAt(i);
                }
                GameObject newItem = Instantiate(GameManager.instance.GetItem(itemName).gameObject, transform.position, Quaternion.identity);
                newItem.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public List<InventorySlot> GetItems()
    {
        return items;
    }

    public int GetCapacity()
    {
        return inventoryCapacity;
    }
}
