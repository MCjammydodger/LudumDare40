using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredMan : Interactable {

    [SerializeField]
    private GameObject speechBubble;

    private float speechDuration = 8;
    private float timeSinceStart;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceStart += Time.deltaTime;
        if(timeSinceStart >= speechDuration)
        {
            speechBubble.SetActive(false);
        }
	}

    public override void Interact()
    {
        speechBubble.SetActive(true);
        timeSinceStart = 0;
    }
}
