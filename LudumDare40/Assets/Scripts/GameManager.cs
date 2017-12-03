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
    private SpawnPosition spawnPoint;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private InventoryUI inventoryUI;
    [SerializeField]
    private Transform earth;
    [SerializeField]
    private GameObject ufo;


    private float timeToShowVictoryScreen = 2;
    private float timeSinceVictory = 0;
    private bool victory = false;

	// Use this for initialization
	private void Awake () {
        PauseGame(false);
        instance = this;
        spawnPoint.MoveToSpawnPosition();
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
        if (victory)
        {
            if (timeSinceVictory > timeToShowVictoryScreen)
            {
                PauseGame(true);
                HUD.instance.ShowVictoryScreen();
                inventoryUI.gameObject.SetActive(false);
            }
            timeSinceVictory += Time.unscaledDeltaTime;
        }
	}

    public void CalculateLevelRect(Transform parent)
    {
        player.GetComponent<Player>().currentLevel = parent;
        levelBounds = CalculateLevelRect(new LevelBounds(), parent);
        levelBounds.top += 100f;    //There is no top limit so make the number bigger than the highest object.
    }

    private LevelBounds CalculateLevelRect(LevelBounds currentRect, Transform parent)
    {
        foreach(Transform child in parent)
        {
            Collider2D c = child.GetComponent<Collider2D>();
            if(c != null)
            {
                bool newRect = false;
                if (currentRect.left == 0 && currentRect.right == 0 && currentRect.top == 0 && currentRect.bottom == 0)
                {
                    newRect = true;
                }
                if(c.bounds.min.x < currentRect.left || newRect)
                {
                    currentRect.left = c.bounds.min.x;
                }
                if(c.bounds.max.y > currentRect.top || newRect)
                {
                    currentRect.top = c.bounds.min.y;
                }
                if(c.bounds.max.x > currentRect.right || newRect)
                {
                    currentRect.right = c.bounds.max.x;
                }
                if(c.bounds.min.y < currentRect.bottom || newRect)
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

    public void GameOver()
    {
        PauseGame(true);
        HUD.instance.ShowGameoverScreen();
        inventoryUI.gameObject.SetActive(false);
    }

    public void Victory()
    {
        Camera.main.GetComponent<CameraFollow>().enabled = false;
        Camera.main.transform.position = new Vector3(earth.position.x, earth.position.y, Camera.main.transform.position.z);
        ufo.SetActive(true);
        victory = true;
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
