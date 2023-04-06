using System.Collections.Generic;

using HealthSystem;

using UnityEngine;

namespace Player
{
    public class TargetPicker : MonoBehaviour
    {
        [SerializeField] private float _aimRadius;

        [SerializeField] private Color _gizmosColor;

        private Transform _currentTarget;

        public Transform CurrentTarget => _currentTarget;

        private List<EnemyHitBox> _enemies = new List<EnemyHitBox>();

        public void SelectTarget()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _aimRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider.TryGetComponent<EnemyHitBox>(out EnemyHitBox enemy))
                {
                    _enemies.Add(enemy);
                }
            }

            _currentTarget = null;

            float minDistance = Mathf.Infinity;

            foreach (var enemy in _enemies)
            {
                float distance = Vector2.Distance(enemy.transform.position, transform.position);

                if (distance < minDistance)
                {
                    _currentTarget = enemy.transform;

                    minDistance = distance;
                }
            }

            _enemies.Clear();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;

            Gizmos.DrawWireSphere(transform.position, _aimRadius);
        }
    }
}