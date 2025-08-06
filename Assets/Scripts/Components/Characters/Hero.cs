using UnityEngine;

public class Hero : PlayerController
{
    [Header("Hero Specifics")]
    public GameObject specialAttackPrefab;
    public Transform attackPoint;

    public float specialCooldown = 5f;
    private float cooldownTimer;

    protected override void Update()
    {
        base.Update();

        cooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f)
        {
            UseSpecialAttack();
            cooldownTimer = specialCooldown;
        }
    }

    void UseSpecialAttack()
    {
        if (specialAttackPrefab != null && attackPoint != null)
        {
            Instantiate(specialAttackPrefab, attackPoint.position, attackPoint.rotation);
            Debug.Log("Hero used special attack!");
        }
    }
}
