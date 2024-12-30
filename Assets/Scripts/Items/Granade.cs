using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Item
{
    [SerializeField] private float timeExplosion;
    [SerializeField] private float damage;
    [SerializeField] private float radiusEffect;
    [SerializeField] private GameObject explosionEff;
    private Rigidbody2D rb;
    private float timeCount = 0;
    private bool startCountTime;
    [SerializeField] private LayerMask effectedLayers;
    [SerializeField] private LayerMask groundLayers;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        timeCount = timeExplosion;
        startCountTime = true;

    }
    private void Update()
    {
        if (startCountTime)
        {
            timeCount -= Time.deltaTime;
            if (timeCount <= 0)
                Explosion();
        }
    }
    public override void Excute()
    {
        rb.AddForce(new Vector2(3, 7), ForceMode2D.Impulse);
    }

    void Explosion()
    {
        Instantiate(explosionEff, transform.position, Quaternion.identity);
        Collider2D[] allObjectInsideRadius = Physics2D.OverlapCircleAll(transform.position, radiusEffect, effectedLayers);
        if (allObjectInsideRadius.Length > 0)
        {
            foreach (Collider2D coll in allObjectInsideRadius)
            {
                if (coll.GetComponent<Health>() != null)
                    coll.GetComponent<Health>().TakeDamage(damage);
            }
        }
        Debug.Log("Explosion");
        startCountTime = false;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiusEffect);
    }
}
