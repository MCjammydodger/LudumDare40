using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour {
    private List<Enemy> enemiesHit;

    private float damage = 50;

    private void Awake()
    {
        enemiesHit = new List<Enemy>();
    }

    private void OnEnable()
    {
        enemiesHit.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy e = collision.transform.GetComponent<Enemy>();
        if (e != null && !enemiesHit.Contains(e))
        {
            e.TakeDamage(damage);
            enemiesHit.Add(e);
        }
    }
}
