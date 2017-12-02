using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseInteraction : MonoBehaviour {

    public static MouseInteraction instance;

    private Item currentItem;

    [SerializeField]
    private GameObject itemInfoUI;
    [SerializeField]
    private Text itemNameText;
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
		
	}

    public void ShowItemInfo(Item item)
    {
        currentItem = item;
        itemNameText.text = currentItem.itemName;
        itemInfoUI.SetActive(true);
        itemInteractionText.text = "[" + currentItem.interactionInfo + "]";
        SetItemInfoPosition();
        
    }

    public void HideItemInfo(Item item)
    {
        if(currentItem == item)
        {
            itemInfoUI.SetActive(false);
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
            itemInfoUI.transform.position = new Vector3(itemPosition.x + (itemInfoWidth / 2), itemPosition.y + (itemCollider.bounds.size.y / 2) + (itemInfoHeight / 2), 1);
        }
        else
        {
            itemInfoUI.transform.position = new Vector3(itemPosition.x - (itemInfoWidth / 2), itemPosition.y + (itemCollider.bounds.size.y / 2) + (itemInfoHeight / 2), 1);
        }
    }
}
