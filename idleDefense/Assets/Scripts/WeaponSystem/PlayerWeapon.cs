using Zenject;

using Player;

using WeaponSystem.Projectiles;

using UnityEngine;

namespace WeaponSystem
{
    public class PlayerWeapon : WeaponBase
    {
        [SerializeField] private TargetPicker _targetPicker;

        [SerializeField] private Transform _shootPoint;

        private Transform _target;

        private ProjectilesPool _projectilesPool;

        [Inject]
        private void Construct(ProjectilesPool projectilesPool)
        {
            _projectilesPool = projectilesPool;
        }

        protected override void Attack()
        {
            FindTarget();

            Shoot();
        }

        private void FindTarget()
        {
            _targetPicker.SelectTarget();

            _target = _targetPicker.CurrentTarget;
        }

        private void Shoot()
        {
            if (_target == null)
                return;

            Vector2 targetRotation = _target.position - transform.position;

            targetRotation.Normalize();

            float playerRotation = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, playerRotation - 90f);

            _projectilesPool.AddProjectile(_shootPoint.position, transform.up);
        }
    }
}