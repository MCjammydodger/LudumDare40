using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy {
    [SerializeField]
    private float timeToJump = 2;
    private float timeSinceJump = 0;
    private float jumpDecreaseRate = 60;
    private float speed = 10;
    private float jumpSpeed = 10;

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        timeSinceJump += Time.deltaTime;

	}

    protected override void FollowPlayer()
    {
        float movementY = Mathf.Max(Physics2D.gravity.y, movementVector.y - (jumpDecreaseRate * Time.deltaTime));
        float movementX = 0;
        float direction = player.transform.position.x - transform.position.x;
        if (timeSinceJump >= timeToJump)
        {
            timeSinceJump = 0;
            movementY = 10;
            if (currentLevel == GameManager.instance.GetPlayer().GetComponent<Player>().currentLevel)
            {
                audioSource.clip = AudioManager.instance.spider;
                audioSource.Play();
            }
        }

        if (!IsGrounded())
        {
            if (direction > 0)
            {
                movementX = speed;
                FlipDirection(graphicsTransform, true);
            }
            else
            {
                movementX = -speed;
                FlipDirection(graphicsTransform, false);
            }
        }
        else
        {
            movementX = 0;
        }
        movementVector = new Vector2(movementX, movementY);
    }

    private bool IsGrounded()
    {
        float maxDistanceToGround = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, maxDistanceToGround);
        if (hit.transform != null)
        {
            return true;
        }
        return false;
    }
}
