using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    [SerializeField]
    private Transform hand;

    public struct InventorySlot
    {
        public string itemName;
        public int quantity;
        public float weight;
    }

    private List<InventorySlot> items;
    private int inventoryCapacity = 18;

	// Use this for initialization
	private void Awake () {
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
                newSlot.weight = item.weight;
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
        slot.weight = item.weight;
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
                    slot.weight = items[i].weight;
                    items[i] = slot;
                }
                else
                {
                    items.RemoveAt(i);
                    if(hand.childCount == 1)
                    {
                        if(hand.GetChild(0).GetComponent<Item>().itemName == itemName)
                        {
                            Destroy(hand.GetChild(0).gameObject);
                        }
                    }
                }
                GameObject newItem = Instantiate(GameManager.instance.GetItem(itemName).gameObject, transform.position, Quaternion.identity);
                newItem.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void EquipItem(string itemName)
    {
        if(hand.childCount == 1)
        {
            Destroy(hand.GetChild(0).gameObject);
        }
        GameObject newItem = Instantiate(GameManager.instance.GetItem(itemName).gameObject, Vector3.zero, Quaternion.identity, hand);
        newItem.layer = LayerMask.NameToLayer("Player");
        newItem.SetActive(true);
        newItem.GetComponent<Collider2D>().isTrigger = true;
        newItem.GetComponent<Rigidbody2D>().isKinematic = true;
        newItem.transform.localPosition = Vector3.zero;
        newItem.GetComponent<Item>().SetEquipped(true);
    }

    public List<InventorySlot> GetItems()
    {
        return items;
    }

    public int GetCapacity()
    {
        return inventoryCapacity;
    }

    public float GetTotalWeight()
    {
        float total = 0;
        foreach(InventorySlot slot in items)
        {
            total += slot.weight * slot.quantity;
        }
        HUD.instance.SetWeightText(total);
        return total;
    }

    public void UseEquipped()
    {
        if(hand.childCount == 1)
        {
            hand.GetChild(0).GetComponent<Item>().Use();
        }
    }

    public Item GetEquipped()
    {
        if(hand.childCount == 1)
        {
            return hand.GetChild(0).GetComponent<Item>();
        }
        return null;
    }
}
