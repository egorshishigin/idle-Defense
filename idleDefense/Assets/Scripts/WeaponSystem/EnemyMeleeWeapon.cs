using Zenject;

using HealthSystem;

using UnityEngine;

namespace WeaponSystem
{
    public class EnemyMeleeWeapon : WeaponBase
    {
        [SerializeField] private float _damage;

        private PlayerHitBox _playerHitBox;

        [Inject]
        private void Construct(PlayerHitBox playerHitBox)
        {
            _playerHitBox = playerHitBox;
        }

        protected override void Attack()
        {
            _playerHitBox.DamageHandler(_damage);
        }
    }
}