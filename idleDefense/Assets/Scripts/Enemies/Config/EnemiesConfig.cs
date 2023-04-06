using System.Collections.Generic;

using UnityEngine;

namespace Enemies.Config
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        public List<Enemy> Enemies => _enemies;
    }
}