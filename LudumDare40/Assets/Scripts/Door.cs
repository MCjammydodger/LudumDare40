using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

    [SerializeField]
    private SpawnPosition link;
    
	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public override void Interact()
    {
        link.MoveToSpawnPosition();
    }
}
