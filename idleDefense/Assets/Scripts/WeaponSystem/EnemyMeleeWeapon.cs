using System.Collections;
using System.Collections.Generic;

using Zenject;

using UnityEngine;
using HealthSystem;

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

            Debug.Log("attack player");
        }
    }
}