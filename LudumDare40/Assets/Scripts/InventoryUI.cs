using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [SerializeField]
    private InventoryItemUI inventoryItemUIPrefab;
    [SerializeField]
    private Transform itemsTransform;
    [SerializeField]
    private Inventory inventory;
    [SerializeField]
    private Button equipButton;
    [SerializeField]
    private Button dropButton;

    private int inventoryCapacity;
    private InventoryItemUI[] slotUIs;
    private int currentSelectedIndex = 0;
    private List<Inventory.InventorySlot> slots;

	// Use this for initialization
	private void Start () {
        inventoryCapacity = inventory.GetCapacity();
        slots = inventory.GetItems();
        slotUIs = new InventoryItemUI[inventoryCapacity];
        for(int i = 0; i < inventoryCapacity; i++)
        {
            slotUIs[i] = Instantiate(inventoryItemUIPrefab, itemsTransform);
            slotUIs[i].SetInventoryUI(this);
        }
        //Refresh();
        gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    private void OnEnable()
    {
        Refresh();    
    }

    private void Refresh()
    {
        for (int i = 0; i < inventoryCapacity;  i++) {
            if(i < slots.Count)
            {
                slotUIs[i].SetItemName(slots[i].itemName);
                slotUIs[i].SetItemQuantity(slots[i].quantity);
            }
            else
            {
                slotUIs[i].SetItemName("");
                slotUIs[i].SetItemQuantity(0);
            }
            slotUIs[i].SetIndex(i);
            slotUIs[i].SetSelected(false);
        }
        equipButton.interactable = false;
        dropButton.interactable = false;
    }

    public void SetCurrentSelected(int i)
    {
        currentSelectedIndex = i;
        for(int slot = 0; slot < slotUIs.Length; slot++)
        {
            if(slot == i)
            {
                slotUIs[slot].SetSelected(true);
            }
            else
            {
                slotUIs[slot].SetSelected(false);
            }
        }
        dropButton.interactable = false;
        equipButton.interactable = false;

        if (currentSelectedIndex < slots.Count)
        {
            if (slots[currentSelectedIndex].quantity > 0)
            {
                dropButton.interactable = true;
                if (GameManager.instance.GetItem(slots[currentSelectedIndex].itemName).equippable)
                {
                    equipButton.interactable = true;
                }
            }
        }
    }

    public void DropSelectedItem()
    {
        inventory.RemoveFromInventory(slots[currentSelectedIndex].itemName);
        Refresh();
    }

    public void EquipSelectedItem()
    {
        inventory.EquipItem(slots[currentSelectedIndex].itemName);
    }
}
