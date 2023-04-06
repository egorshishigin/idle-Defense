using System;

using UnityEngine;

namespace HealthSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _startHealth;

        private float _currentHealth;

        public event Action<float> HealthInitialized = delegate { };

        public event Action<float> Damaged = delegate { };

        public event Action Died = delegate { };

        private void Start()
        {
            _currentHealth = _startHealth;

            HealthInitialized.Invoke(_currentHealth);
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            Damaged.Invoke(_currentHealth);

            if (_currentHealth <= 0)
            {
                Died.Invoke();
            }
        }
    }
}