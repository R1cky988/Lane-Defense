using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int damage = 1;
    [SerializeField] private float lifeTime = 6f;
    [SerializeField] private bool isHitting = false;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(isHitting) return;

        EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
        if(enemy != null)
        {
            isHitting = true;
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
