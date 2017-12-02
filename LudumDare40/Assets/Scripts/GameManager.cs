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

    [SerializeField]
    private Transform levelTransform;

	// Use this for initialization
	private void Start () {
        instance = this;
        levelBounds = CalculateLevelRect(new LevelBounds(), levelTransform);
        levelBounds.top += 100f;    //There is no top limit so make the number bigger than the highest object.
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
}
