using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : Interactable {

    private float countdown = 20;
    private int previousNumber = 20;
    [SerializeField]
    private Transform level;

    private bool started = false;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (started)
        {
            countdown -= Time.deltaTime;
            HUD.instance.SetCountdownText((int)countdown);
            if(previousNumber - (int)countdown == 1)
            {
                previousNumber = (int)countdown;
                audioSource.clip = AudioManager.instance.countdown;
                audioSource.Play();
            }

            if(countdown <= 0)
            {
                //Big 'splodes
                audioSource.clip = AudioManager.instance.explosion;
                audioSource.Play();
                if(GameManager.instance.GetPlayer().GetComponent<Player>().currentLevel == level)
                {
                    GameManager.instance.GameOver();
                }
                else
                {
                    GameManager.instance.Victory();
                }
                started = false;
            }
        }
	}

    public override void Interact()
    {
        started = true;
    }
}
