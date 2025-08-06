public class Skeleton : EnemyController
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 3f;
        maxHealth = 30;
        damage = 5;
    }

    protected override void Die()
    {
        // Play goblin-specific death sound or animation
        base.Die();
    }
}
