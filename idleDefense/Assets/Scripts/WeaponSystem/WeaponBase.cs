using UnityEngine;

namespace WeaponSystem
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] private float _attackRate;

        private float _nextTimeToAttack;

        private void Update()
        {
            if (Time.unscaledTime >= _nextTimeToAttack)
            {
                _nextTimeToAttack = Time.unscaledTime + 1f / _attackRate;

                Attack();
            }
        }

        protected abstract void Attack();
    }
}