using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioClip hit;
    public AudioClip door;
    public AudioClip enemyHit;
    public AudioClip ray;
    public AudioClip countdown;
    public AudioClip explosion;
    public AudioClip zombie1;
    public AudioClip zombie2;
    public AudioClip spider;

	// Use this for initialization
	void Start () {
        instance = this;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
