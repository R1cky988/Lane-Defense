using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 2;

    [SerializeField] private Animator animator;
    [SerializeField] private float deathAnimDuration = 1f;
    private EnemyMovement enemyMovement;


    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("death");
        enemyMovement.enabled = false;
        enemyMovement.rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        StopAllCoroutines();
        EnemySpawner.onEnemyDestroyed.Invoke();
        Destroy(gameObject, deathAnimDuration);
    }
}
