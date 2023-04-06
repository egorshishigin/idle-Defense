using System.Collections.Generic;

using Zenject;

using Enemies;
using Enemies.Config;

using UnityEngine;

public class EnemiesFactory : IFactory<IEnemy>
{
    private readonly DiContainer _diContainer;

    private List<Enemy> _enemiesPrefabs = new List<Enemy>();

    [Inject]
    public EnemiesFactory(DiContainer diContainer, EnemiesConfig enemiesConfig)
    {
        _diContainer = diContainer;

        _enemiesPrefabs = enemiesConfig.Enemies;
    }

    public IEnemy Create()
    {
        var enemy = SelectRandomEnemy();

        return _diContainer.InstantiatePrefabForComponent<IEnemy>(enemy);
    }

    private Enemy SelectRandomEnemy()
    {
        int randomIndex = Random.Range(0, _enemiesPrefabs.Count);

        return _enemiesPrefabs[randomIndex];
    }
}