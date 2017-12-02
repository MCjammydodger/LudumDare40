using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private float maxHealth = 100;
    private float health;

    private Inventory inventory;

	// Use this for initialization
	private void Start () {
        inventory = GetComponent<Inventory>();
        health = maxHealth;
        HUD.instance.SetHealthBar(1);
	}
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKey(KeyCode.E))
        {
            inventory.UseEquipped();
        }
	}
    
    public void TakeDamage(float amount)
    {
        health -= amount;
        HUD.instance.SetHealthBar(health / maxHealth);
        if(health <= 0)
        {
            GameManager.instance.GameOver();
        }
    }
}
