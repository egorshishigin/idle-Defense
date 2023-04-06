using System.Collections.Generic;

using UnityEngine;

namespace WeaponSystem.Projectiles
{
    public class ProjectilesPool
    {
        private readonly Projectile.Pool _pool;

        private readonly List<Projectile> _projectiles = new List<Projectile>();

        public ProjectilesPool(Projectile.Pool pool)
        {
            _pool = pool;
        }

        public void AddProjectile(Vector2 startPosition, Vector2 direction)
        {
            _projectiles.Add(_pool.Spawn(startPosition, direction));
        }

        public void RemoveProjectile()
        {
            var projectile = _projectiles[0];

            _pool.Despawn(projectile);

            _projectiles.Remove(projectile);
        }
    }
}