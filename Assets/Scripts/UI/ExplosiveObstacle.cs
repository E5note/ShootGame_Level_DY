using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObstacle : MonoBehaviour
{
    public float explosionRadius = 3f;
    public int explosionDamage = 100;
    public GameObject explosionEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // 范围检测
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().Die();
            }
        }
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
