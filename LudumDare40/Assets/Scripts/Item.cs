using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    
    public string itemName = "Item Name";
    public string interactionInfo = "Click to interact";

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    private void OnMouseEnter()
    {
        MouseInteraction.instance.ShowItemInfo(this);
    }

    private void OnMouseExit()
    {
        MouseInteraction.instance.HideItemInfo(this);
    }

    public void Interact()
    {

    }
}
