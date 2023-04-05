using UnityEngine;

namespace HealthSystem
{
    public abstract class HitBoxBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            HitHandler();
        }

        public void DamageHandler(float damage)
        {
            _health.TakeDamage(damage);
        }

        protected abstract void HitHandler();
    }
}