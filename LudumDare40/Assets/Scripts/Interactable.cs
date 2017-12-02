using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
    public string itemName = "Item Name";
    public string interactionInfo = "Click to interact";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void Interact()
    {

    }


    protected virtual void OnMouseEnter()
    {
        MouseInteraction.instance.ShowItemInfo(this);
    }

    protected virtual void OnMouseExit()
    {
        MouseInteraction.instance.HideItemInfo(this);
    }
}
