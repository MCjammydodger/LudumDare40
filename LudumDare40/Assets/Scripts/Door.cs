using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

    [SerializeField]
    private SpawnPosition link;

    public Item requiredKey;

    private Inventory inventory;

	// Use this for initialization
	private void Start () {
        inventory = GameManager.instance.GetPlayer().GetComponent<Inventory>();	
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    protected override void OnMouseEnter()
    {
        MouseInteraction.instance.ShowItemInfo(this);
    }

    public override void Interact()
    {
        if(requiredKey == null || (inventory.GetEquipped() != null && inventory.GetEquipped().itemName == requiredKey.itemName))
        {
            link.MoveToSpawnPosition();
        }
    }
}
