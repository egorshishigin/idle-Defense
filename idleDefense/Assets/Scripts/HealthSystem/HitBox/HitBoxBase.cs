using UnityEngine;

namespace HealthSystem
{
    public abstract class HitBoxBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Died += DiedHandler;
        }

        private void OnDisable()
        {
            _health.Died -= DiedHandler;
        }

        public void DamageHandler(float damage)
        {
            _health.TakeDamage(damage);
        }

        protected abstract void DiedHandler();
    }
}