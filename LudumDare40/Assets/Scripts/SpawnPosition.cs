using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour {

    [SerializeField]
    private Transform levelParent;

	// Use this for initialization
	private void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public void MoveToSpawnPosition()
    {
        GameManager.instance.GetPlayer().transform.position = transform.position;
        GameManager.instance.CalculateLevelRect(levelParent);
    }
}
