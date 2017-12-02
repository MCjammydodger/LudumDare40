using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public static HUD instance;

    [SerializeField]
    private Slider healthBar;

	// Use this for initialization
	private void Awake () {
        instance = this;
	}
	
	// Update is called once per frame
	private void Update () {
		
	}

    public void SetHealthBar(float value)
    {
        healthBar.value = value;
    }
}
