using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy {

    private float speed = 0.2f;

	// Use this for initialization
	protected override void Start () {
        base.Start();

	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();	
	}

    protected override void FollowPlayer()
    {
        if(player.currentLevel != currentLevel)
        {
            return;
        }
        Vector2 direction = player.transform.position - transform.position;
        movementVector = direction * speed;
    }
}
