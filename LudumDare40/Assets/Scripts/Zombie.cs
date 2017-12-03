using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    private float speed = 5;

    private float timeSinceNoiseMade = 0;
    private float timeUntilNewNoise = 4;

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
        if(currentLevel == GameManager.instance.GetPlayer().GetComponent<Player>().currentLevel)
        {
            if(timeSinceNoiseMade >= timeUntilNewNoise)
            {
                timeUntilNewNoise = Random.Range(2f, 4f);
                timeSinceNoiseMade = 0;
                int noiseToMake = Random.Range(0, 2);
                switch (noiseToMake)
                {
                    case 0:
                        audioSource.clip = AudioManager.instance.zombie1;
                        break;
                    case 1:
                        audioSource.clip = AudioManager.instance.zombie2;
                        break;
                }
                audioSource.Play();
            }
            timeSinceNoiseMade += Time.deltaTime;
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
