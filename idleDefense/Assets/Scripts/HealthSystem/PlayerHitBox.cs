using UnityEngine;

namespace HealthSystem
{
    public class PlayerHitBox : HitBoxBase
    {
        protected override void HitHandler()
        {
            Debug.Log("Player damaged");
        }
    }
}