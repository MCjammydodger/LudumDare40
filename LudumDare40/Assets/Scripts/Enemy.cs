using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable {

    [SerializeField]
    protected Player player;
    [SerializeField]
    protected Transform graphicsTransform;

    protected Vector2 movementVector;

    protected Rigidbody2D rb;

    protected float maxHealth = 100;
    protected float health;

	// Use this for initialization
	protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}

    protected virtual void FollowPlayer()
    {

    }

    protected void FlipDirection(Transform parent, bool faceRight)
    {
        if (faceRight)
        {
            graphicsTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            graphicsTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected void FixedUpdate()
    {
        rb.velocity = movementVector;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Enemy Health: " + health);
    }
}
