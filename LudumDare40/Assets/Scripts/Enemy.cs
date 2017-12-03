using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable {

    protected Player player;
    [SerializeField]
    protected Transform graphicsTransform;

    [SerializeField]
    private Item itemToDrop;

    protected Vector2 movementVector;

    protected Rigidbody2D rb;

    protected Transform currentLevel;

    [SerializeField]
    protected float maxHealth = 100;
    protected float health;

    protected bool isDead = false;
    protected bool inRange = false;

    [SerializeField]
    private float damage = 10;
    [SerializeField]
    private float timeBetweenAttacks = 0.2f;
    private float timeSinceAttack = 0;

    protected AudioSource audioSource;

    // Use this for initialization
    protected virtual void Start () {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        player = GameManager.instance.GetPlayer().GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("No audio source on enemy!");
        }
        currentLevel = transform.parent;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        if (isDead)
        {
            return;
        }
        if (inRange)
        {
            timeSinceAttack += Time.deltaTime;
            if (timeSinceAttack >= timeBetweenAttacks)
            {
                player.TakeDamage(damage);
                timeSinceAttack = 0;
            }
        }
        FollowPlayer();
    }

    protected virtual void FollowPlayer()
    {

    }

    protected override void OnMouseEnter()
    {
        MouseInteraction.instance.ShowItemInfo(this);
    }

    protected void FlipDirection(Transform parent, bool faceRight)
    {
        if (faceRight)
        {
            graphicsTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            graphicsTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    protected void FixedUpdate()
    {
        rb.velocity = movementVector;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            isDead = true;
            transform.rotation = Quaternion.Euler(0, 0, -90);
            movementVector = new Vector3(0, Physics2D.gravity.y);
            if (itemToDrop != null)
            {
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
            GetComponent<Collider2D>().isTrigger = true;
            Destroy(gameObject, 2);
        }
        audioSource.clip = AudioManager.instance.enemyHit;
        audioSource.Play();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            inRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null)
        {
            inRange = false;
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
