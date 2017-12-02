using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable {
    public bool equippable = false;
    public float weight = 0;

    private Inventory inventory;

	// Use this for initialization
	protected virtual void Start () {
        inventory = GameManager.instance.GetPlayer().GetComponent<Inventory>();
    }

    // Update is called once per frame
    protected virtual void Update () {
		
	}

    protected override void OnMouseEnter()
    {
        MouseInteraction.instance.ShowItemInfo(this);
    }

    protected override void OnMouseExit()
    {
        MouseInteraction.instance.HideItemInfo(this);
    }

    public override void Interact()
    {
        if (inventory.AddToInventory(this))
        {
            MouseInteraction.instance.HideItemInfo(this);
            Destroy(gameObject);
        }
    }

    public virtual void Use()
    {

    }
}
