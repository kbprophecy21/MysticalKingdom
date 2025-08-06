using UnityEngine;
using System.Collections;

public class CannonTower : MonoBehaviour
{
    [Header("Tower Settings")]
    public float range = 6f;
    public float fireRate = 1f;
    public int damage = 20;
    public GameObject projectileVisual;
    public Transform firePoint;
    public LayerMask enemyLayer;

    [Header("Turret Rotation")]
    public Transform turret; // Assign this to your rotating turret part
    public float rotationSpeed = 5f;

    private float fireCooldown;
    private Transform currentTarget;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        if (currentTarget == null || !IsTargetInRange(currentTarget))
        {
            FindTarget();
        }

        if (currentTarget != null)
        {
            RotateTurretToward(currentTarget);

            if (fireCooldown <= 0f)
            {
                FireAtTarget();
                fireCooldown = 1f / fireRate;
            }
        }
    }

    void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, range, enemyLayer);
        float closest = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (Collider col in enemies)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < closest)
            {
                closest = dist;
                bestTarget = col.transform;
            }
        }

        currentTarget = bestTarget;
    }

    bool IsTargetInRange(Transform target)
    {
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    void FireAtTarget()
    {
        if (projectileVisual == null || firePoint == null || currentTarget == null) return;

        GameObject visual = Instantiate(projectileVisual, firePoint.position, Quaternion.identity);
        StartCoroutine(MoveAndHit(visual, currentTarget, damage));
    }

    IEnumerator MoveAndHit(GameObject proj, Transform target, int dmg)
    {
        float speed = 10f;

        while (target != null && Vector3.Distance(proj.transform.position, target.position) > 0.2f)
        {
            Vector3 dir = (target.position - proj.transform.position).normalized;
            proj.transform.position += dir * speed * Time.deltaTime;
            yield return null;
        }

        if (target != null)
        {
            EnemyController ec = target.GetComponent<EnemyController>();
            if (ec != null)
            {
                ec.TakeDamage(dmg);
            }
        }

        Destroy(proj);
    }

    void RotateTurretToward(Transform target)
    {
        if (turret == null || target == null) return;

        Vector3 direction = target.position - turret.position;
        direction.y = 0f; // Flatten the vector to rotate only horizontally

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
