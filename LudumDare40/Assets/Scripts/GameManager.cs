using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public struct LevelBounds
    {
        public float left;
        public float right;
        public float top;
        public float bottom;
        public override string ToString()
        {
            return "L: " + left + ", R: " + right + ", T: " + top + ", B: " + bottom;
        }
    }

    public LevelBounds levelBounds;

    public List<Item> items;

    [SerializeField]
    private Transform levelTransform;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private InventoryUI inventoryUI;

	// Use this for initialization
	private void Awake () {
        instance = this;
        levelBounds = CalculateLevelRect(new LevelBounds(), levelTransform);
        levelBounds.top += 100f;    //There is no top limit so make the number bigger than the highest object.
        Item[] allItems = FindObjectsOfType<Item>();
        foreach(Item i in allItems)
        {
            if(GetItem(i.itemName) == null)
            {
                //Make a copy of the item, so there is always one even if the item found is destroyed.
                Item newItem = Instantiate(i);
                newItem.gameObject.SetActive(false);
                items.Add(newItem);
            }
        }
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    private LevelBounds CalculateLevelRect(LevelBounds currentRect, Transform parent)
    {
        foreach(Transform child in parent)
        {
            Collider2D c = child.GetComponent<Collider2D>();
            if(c != null)
            {
                if(c.bounds.min.x < currentRect.left)
                {
                    currentRect.left = c.bounds.min.x;
                }
                if(c.bounds.max.y > currentRect.top)
                {
                    currentRect.top = c.bounds.min.y;
                }
                if(c.bounds.max.x > currentRect.right)
                {
                    currentRect.right = c.bounds.max.x;
                }
                if(c.bounds.min.y < currentRect.bottom)
                {
                    currentRect.bottom = c.bounds.min.y;
                }
            }
            currentRect = CalculateLevelRect(currentRect, child);
        }
        return currentRect;
    }

    public GameObject GetPlayer()
    {
        return player;
    } 

    public InventoryUI GetInventoryUI()
    {
        return inventoryUI;
    }

    public Item GetItem(string itemName)
    {
        foreach(Item i in items)
        {
            if(i.itemName == itemName)
            {
                return i;
            }
        }
        return null;
    }
}
