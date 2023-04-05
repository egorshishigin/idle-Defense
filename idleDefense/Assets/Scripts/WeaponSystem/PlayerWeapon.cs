using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeaponSystem
{
    public class PlayerWeapon : WeaponBase
    {
        [SerializeField] private Transform _target;

        [SerializeField] private TargetPicker _targetPicker;

        protected override void Attack()
        {
            _targetPicker.SelectTarget();

            _target = _targetPicker.CurrentTarget;

            if (_target == null)
                return;

            Vector2 targetRotation = _target.position - transform.position;

            targetRotation.Normalize();

            float playerRotation = Mathf.Atan2(targetRotation.y, targetRotation.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, playerRotation - 90f);
        }
    }
}