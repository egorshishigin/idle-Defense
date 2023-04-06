using Player;

using UnityEngine;

namespace Spells
{
    public class ChainLightning : SpellBase
    {
        [SerializeField] private ChainLightningProjectile _lightningProjectile;

        [SerializeField] private TargetPicker _targetPicker;

        [SerializeField] private Transform _spellSpawnPoint;

        protected override void Spell()
        {
            if (_targetPicker.CurrentTarget == null)
                return;

            LaunchLightning();
        }

        private void LaunchLightning()
        {
            _lightningProjectile.transform.position = _spellSpawnPoint.position;

            _lightningProjectile.gameObject.SetActive(true);

            _lightningProjectile.SetTarget(_targetPicker.CurrentTarget);
        }
    }
}