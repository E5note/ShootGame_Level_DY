using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObstacle : MonoBehaviour
{
    // public GameObject destroyedEffect;



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Instantiate(destroyedEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}