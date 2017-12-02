using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    private float speed = 5;


	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (isDead)
        {
            return;
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


}
