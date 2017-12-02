using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    private float speed = 5;
    private float damage = 10;
    private float timeBetweenAttacks = 0.2f;
    private float timeSinceAttack = 0;
    private bool inRange = false;

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        FollowPlayer();
        if (inRange)
        {
            timeSinceAttack += Time.deltaTime;
            if(timeSinceAttack >= timeBetweenAttacks)
            {
                player.TakeDamage(damage);
                timeSinceAttack = 0;
            }
        }
	}

    protected override void FollowPlayer()
    {
        float direction = player.transform.position.x - transform.position.x;
        float y = Physics2D.gravity.y;
        if (direction > 0)
        {
            movementVector = new Vector2(speed, y);
            FlipDirection(graphicsTransform, true);
        }
        else
        {
            movementVector = new Vector2(-speed, y);
            FlipDirection(graphicsTransform, false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if(p != null)
        {
            inRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if(p != null)
        {
            inRange = false;
        }
    }
}
