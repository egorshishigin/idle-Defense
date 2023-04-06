using Zenject;

using Game.GameOver;
using Game.GameOver.View;

using Enemies.Config;
using Enemies.Factory;

using WeaponSystem.Projectiles;

using Spawner;

using HealthSystem;

using UnityEngine;

namespace Infrastructure
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private PlayerHitBox _player;

        [SerializeField] private EnemiesConfig _enemiesConfig;

        [SerializeField] private EnemiesSpawner _spawner;

        [SerializeField] private Projectile _projectilePrefab;

        [SerializeField] private Transform _projectilesPoolHolder;

        [SerializeField] private SceneLoader _sceneLoader;

        [SerializeField] private GameOverView _gameOverView;

        private const int TargetFPS = 60;

        public override void InstallBindings()
        {
            BindGameOver();

            BindGameOverView();

            BindSceneLoader();

            BindProjectilePool();

            BindSpawner();

            BindPlayer();
        }

        public override void Start()
        {
            base.Start();

            Application.targetFrameRate = TargetFPS;
        }

        private void BindGameOver()
        {
            Container
                .Bind<GameOver>()
                .AsSingle();
        }

        private void BindGameOverView()
        {
            Container
                .Bind<GameOverView>()
                .FromInstance(_gameOverView)
                .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .FromInstance(_sceneLoader)
                .AsSingle();
        }

        private void BindProjectilePool()
        {
            Container
                .Bind<ProjectilesPool>()
                .AsSingle();

            Container
                .BindMemoryPool<Projectile, Projectile.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransform(_projectilesPoolHolder);
        }

        private void BindSpawner()
        {
            Container
                .Bind<EnemiesConfig>()
                .FromScriptableObject(_enemiesConfig)
                .AsSingle();

            Container
                .BindInterfacesTo<EnemiesSpawner>()
                .FromInstance(_spawner)
                .AsSingle();

            Container
                .BindFactory<IEnemy, EnemyFactory>()
                .FromFactory<EnemiesFactory>();
        }

        private void BindPlayer()
        {
            Container
                .Bind<PlayerHitBox>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}