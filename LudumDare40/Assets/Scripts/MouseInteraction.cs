using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseInteraction : MonoBehaviour {

    public static MouseInteraction instance;

    private Interactable currentItem;

    [SerializeField]
    private GameObject itemInfoUI;
    [SerializeField]
    private Text itemNameText;
    [SerializeField]
    private Text weightText;
    [SerializeField]
    private Text itemInteractionText;

    private RectTransform itemInfoUITransform;
    private Transform cameraTransform;
	// Use this for initialization
	private void Start () {
        instance = this;
        cameraTransform = Camera.main.GetComponent<CameraFollow>().transform;
        itemInfoUITransform = itemInfoUI.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            if(currentItem != null)
            {
                currentItem.Interact();
            }
        }
	}

    public void ShowItemInfo(Interactable item)
    {
        currentItem = item;
        itemNameText.text = currentItem.itemName;
        itemInfoUI.SetActive(true);
        weightText.text = "";
        itemInteractionText.text = currentItem.interactionInfo;
        SetItemInfoPosition();        
    }
    public void ShowItemInfo(Item item)
    {
        if (item.IsEquipped())
        {
            return;
        }
        ShowItemInfo((Interactable)item);
        weightText.text = "Weight: " + item.weight.ToString();
    }

    public void ShowItemInfo(Door item)
    {
        ShowItemInfo((Interactable)item);
        if (item.requiredKey != null)
        {
            itemInteractionText.text = "Requires " + item.requiredKey.itemName + " to unlock.";
        }
    }

    public void HideItemInfo(Interactable item)
    {
        if(currentItem == item)
        {
            itemInfoUI.SetActive(false);
            currentItem = null;
        }
    }

    private void SetItemInfoPosition()
    {
        Vector3 itemPosition = currentItem.transform.position;
        Collider2D itemCollider = currentItem.GetComponent<Collider2D>();
        float itemInfoWidth = itemInfoUITransform.localScale.x * itemInfoUITransform.rect.width;
        float itemInfoHeight = itemInfoUITransform.localScale.y * itemInfoUITransform.rect.height;

        if (itemPosition.x < cameraTransform.position.x)
        {
            itemInfoUI.transform.position = new Vector3(itemPosition.x + (itemInfoWidth / 2), (itemCollider.bounds.max.y) + (itemInfoHeight / 2), 1);
        }
        else
        {
            itemInfoUI.transform.position = new Vector3(itemPosition.x - (itemInfoWidth / 2), (itemCollider.bounds.max.y) + (itemCollider.bounds.size.y / 2) + (itemInfoHeight / 2), 1);
        }
    }

    public Interactable GetCurrentItem()
    {
        return currentItem;
    }
}
