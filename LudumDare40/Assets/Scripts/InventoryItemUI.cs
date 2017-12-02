using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour {

    [SerializeField]
    private Text itemName;

    [SerializeField]
    private Text itemQuantity;

    private InventoryUI inventoryUI;
    private int itemIndex;

    private Color normalColour = Color.white;
    private Color selectedColour = Color.yellow;
    private Image image;

	// Use this for initialization
	private void Awake () {
        image = GetComponent<Image>();
        image.color = normalColour;
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public void SetItemName(string name)
    {
        itemName.text = name;
    }

    public void SetItemQuantity(int quantity)
    {
        itemQuantity.text = "x" + quantity;
    }

    public void SetInventoryUI(InventoryUI ui)
    {
        inventoryUI = ui;
    }

    public void SetIndex(int i)
    {
        itemIndex = i;
    }

    public void Clicked()
    {
        inventoryUI.SetCurrentSelected(itemIndex);
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            image.color = selectedColour;
        }
        else
        {
            image.color = normalColour;
        }
    }
}
