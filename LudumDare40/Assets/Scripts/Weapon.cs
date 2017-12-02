using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

    [SerializeField]
    private float damage = 10;

    private bool swinging = false;
    private bool damageDealt = false;
    private Animator playerAnimator;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        playerAnimator = GameManager.instance.GetPlayer().GetComponent<Animator>();
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (swinging)
        {
            if (playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                swinging = false;
                damageDealt = false;
            }
        }
	}

    public bool IsSwinging()
    {
        return swinging;
    }

    public override void Use()
    {
        if (!swinging)
        {
            playerAnimator.SetTrigger("Swing");
            swinging = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (swinging && !damageDealt)
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);
                damageDealt = true;
            }
        }
    }
}
