using UnityEngine;

namespace HealthSystem
{
    public class EnemyHitBox : HitBoxBase
    {
        protected override void HitHandler()
        {
            Debug.Log("Enemy damaged");
        }
    }
}