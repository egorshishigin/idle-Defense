using System.Collections;

using Zenject;

using HealthSystem;

using UnityEngine;

namespace WeaponSystem.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        [SerializeField] private float _damage;

        [SerializeField] private float _activeTime;

        [SerializeField] private Rigidbody2D _rigidbody;

        private EnemyHitBox _enemyHitBox;

        private ProjectilesPool _projectilesPool;

        [Inject]
        private void Construct(ProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyHitBox>(out _enemyHitBox))
            {
                _enemyHitBox.DamageHandler(_damage);

                ReturnToPool();
            }
            else return;
        }

        public void Launch(Vector2 direction)
        {
            _rigidbody.AddForce(direction * _speed, ForceMode2D.Impulse);

            StartCoroutine(Deactivate());
        }

        public void ResetProjectile()
        {
            _rigidbody.velocity = Vector2.zero;

            _rigidbody.angularVelocity = 0f;
        }

        private void ReturnToPool()
        {
            _projectilesPool.RemoveProjectile();
        }

        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(_activeTime);

            ReturnToPool();
        }

        public class Pool : MemoryPool<Vector2, Vector2, Projectile>
        {
            protected override void OnCreated(Projectile item)
            {
                item.gameObject.SetActive(false);
            }

            protected override void Reinitialize(Vector2 startPosition, Vector2 direction, Projectile item)
            {
                item.transform.position = startPosition;

                item.gameObject.SetActive(true);

                item.Launch(direction);
            }

            protected override void OnDespawned(Projectile item)
            {
                item.gameObject.SetActive(false);

                item.ResetProjectile();
            }
        }
    }
}