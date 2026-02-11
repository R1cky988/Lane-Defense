using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private Animator attackAnimation;

    private EnemyMovement enemyMovement;
    private bool isAttacking = false;
    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Home") && !isAttacking)
        {
            isAttacking = true;
            attackAnimation.SetBool("isMoving", false);
            enemyMovement.enabled = false;
            enemyMovement.rb.velocity = Vector2.zero;
            StartCoroutine(Attack(collision.gameObject));
        }
    }
    private IEnumerator Attack(GameObject target)
    {
        BaseHealth baseHealth = target.GetComponent<BaseHealth>();
        while(baseHealth != null && baseHealth.currentHealth > 0)
        {
            attackAnimation.SetTrigger("attack");
            baseHealth.TakeDamage(damage);
            yield return new WaitForSeconds(attackSpeed);
        }
    }
}
