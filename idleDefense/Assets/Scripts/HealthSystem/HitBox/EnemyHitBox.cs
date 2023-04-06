namespace HealthSystem
{
    public class EnemyHitBox : HitBoxBase
    {
        protected override void DiedHandler()
        {
            Destroy(gameObject);
        }
    }
}