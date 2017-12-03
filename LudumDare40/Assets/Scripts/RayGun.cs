using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : Item {

    [SerializeField]
    private Ray rayVisuals;

    private float cooldown = 3;
    private float timeSinceFired;
    private float rayLength = 2;

    private List<Enemy> enemiesHit;
    private AudioSource audioSource;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        timeSinceFired = cooldown;
        enemiesHit = new List<Enemy>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        timeSinceFired += Time.deltaTime;
        if(timeSinceFired >= rayLength)
        {
            rayVisuals.gameObject.SetActive(false);
        }
	}

    public override void Use()
    {
        if(timeSinceFired >= cooldown)
        {
            timeSinceFired = 0;
            GameManager.instance.GetPlayer().GetComponent<Animator>().SetTrigger("Shoot");
            rayVisuals.gameObject.SetActive(true);
            enemiesHit.Clear();
            audioSource.clip = AudioManager.instance.ray;
            audioSource.Play();
        }
    }
}
