using System.Collections;

using Zenject;

using Enemies;
using Enemies.Factory;

using UnityEngine;

namespace Spawner
{
    public class EnemiesSpawner : MonoBehaviour, IInitializable
    {
        [SerializeField] private float _spawnTime;

        [SerializeField] private Transform[] _spawnPoints;

        private EnemyFactory _enemyFactory;

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        void IInitializable.Initialize() { }

        private void Start()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            StartCoroutine(SpawnEnemy());
        }

        private IEnumerator SpawnEnemy()
        {
            Enemy wave = (Enemy)_enemyFactory.Create();

            PlaceEnemyAtRandomPoint(wave.transform);

            yield return new WaitForSeconds(_spawnTime);

            StartSpawn();
        }

        private void PlaceEnemyAtRandomPoint(Transform enemy)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);

            enemy.position = _spawnPoints[randomIndex].position;
        }
    }
}