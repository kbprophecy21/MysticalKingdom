using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyController : MonoBehaviour
{
    [Header("Stats")]
    public float moveSpeed = 2f;
    public int maxHealth = 50;
    public int damage = 10;
    public float attackCooldown = 1.5f;

    [Header("Targeting")]
    public Transform target; // Usually set to the CastleTower in Start()
    public float attackRange = 1f;

    protected int currentHealth;
    private float attackTimer;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        if (target == null && CastleTower.Instance != null)
        {
            target = CastleTower.Instance.transform;
        }
        attackTimer = attackCooldown;
    }

    protected virtual void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > attackRange)
        {
            MoveTowardsTarget();
        }
        else
        {
            AttackTarget();
        }
    }

    protected virtual void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z)); // Keep flat
    }

    protected virtual void AttackTarget()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && CastleTower.Instance != null)
        {
            CastleTower.Instance.TakeDamage(damage);
            attackTimer = attackCooldown;
        }
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // Placeholder: Play death animation, effects, drop loot, etc.
        Destroy(gameObject);
    }
}
