using System.Collections.Generic;

using HealthSystem;

using WeaponSystem.Projectiles;

using UnityEngine;

namespace Spells
{
    public class ChainLightningProjectile : MonoBehaviour
    {
        [SerializeField] private float _startDamage;

        [SerializeField] private Rigidbody2D _rigidbody;

        [SerializeField] private float _speed;

        [SerializeField] private float _spellRadius;

        private const int MaxEnemyCount = 3;

        private const int InitialDamage = 10;

        private Transform _currentTarget;

        private int _enemyCount;

        private EnemyHitBox _currentEnemy;

        private Transform _lastTarget;

        private List<Transform> _targets = new List<Transform>();

        private List<Transform> _hitTargets = new List<Transform>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out _currentEnemy)
                || _enemyCount > MaxEnemyCount
                || _hitTargets.Contains(collision.transform))
            {
                return;
            }

            StopProjectile();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(collision.transform.position, _spellRadius);

            foreach (var collider in colliders)
            {
                if (collider.transform != _currentTarget
                    && collider.transform != transform
                    && !collider.GetComponent<PlayerHitBox>()
                    && !collider.GetComponent<Projectile>())
                {
                    _targets.Add(collider.transform);
                }
            }

            _hitTargets.Add(_currentTarget);

            ProjectileHit();
        }

        private void Update()
        {
            if (_enemyCount == MaxEnemyCount || _currentTarget == null)
                Deactivate();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, _spellRadius);
        }

        public void SetTarget(Transform target)
        {
            _currentTarget = target;

            MoveProjectile();
        }

        private void ProjectileHit()
        {
            DamageEnemy();

            if (_targets.Count == 0)
            {
                Deactivate();

                return;
            }

            MoveToNextTarget();
        }

        private void MoveToNextTarget()
        {
            float minDistance = Mathf.Infinity;

            foreach (var target in _targets)
            {
                if (target == null)
                    continue;

                float distance = Vector2.Distance(target.position, transform.position);

                if (distance < minDistance && target != _lastTarget && !_hitTargets.Contains(target))
                {
                    _currentTarget = target;

                    MoveProjectile();

                    minDistance = distance;

                    _targets.Clear();

                    return;
                }
                else _currentTarget = null;
            }

            _targets.Clear();
        }

        private void DamageEnemy()
        {
            _enemyCount++;

            _lastTarget = _currentTarget;

            EnemyHitBox enemy = _currentTarget.GetComponent<EnemyHitBox>();

            if (_enemyCount > 1)
            {
                _startDamage = _startDamage - _startDamage * 0.15f;
            }

            enemy.DamageHandler(_startDamage);
        }

        private void MoveProjectile()
        {
            _rigidbody.AddForce((_currentTarget.position - transform.position).normalized * _speed, ForceMode2D.Impulse);
        }

        private void StopProjectile()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        private void Deactivate()
        {
            StopProjectile();

            ResetProjectile();

            gameObject.SetActive(false);
        }

        private void ResetProjectile()
        {
            _targets.Clear();

            _hitTargets.Clear();

            _enemyCount = 0;

            _startDamage = InitialDamage;

            _currentTarget = null;

            _lastTarget = null;

            _currentEnemy = null;
        }
    }
}