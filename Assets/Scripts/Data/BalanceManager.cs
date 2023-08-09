using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Utility;

public class BalanceManager : IStarter
{
    private GameData _gameData;
    private List<EnemyTemplate> _availableEnemies;

    public IEnumerator Initialize()
    {
        _gameData = new GameData();

        var enemies = _gameData.GetEnemies().GetTemplates();
        _availableEnemies = new List<EnemyTemplate>();
        foreach (var enemy in _gameData.GetSettings().GetEnemies())
        {
            _availableEnemies.Add(enemies.FirstOrDefault(x => x.GetId().Equals(enemy)));
        }
        yield break;
    }

    public Type Type()
    {
        return GetType();
    }

    public WeaponTemplate GetWeaponTemplate(string id)
    {
        return CloneUtil.Clone(_gameData.GetWeaponTemplate(id));
    }

    public PlayerTemplate GetPlayerTemplate()
    {
        return CloneUtil.Clone(_gameData.GetPlayerTemplate());
    }

    public BulletTemplate GetBulletTemplate(string id)
    {
        return CloneUtil.Clone(_gameData.GetBulletTemplate(id));
    }

    public EnemyTemplate GetRandomEnemy()
    {
        var randomIndex = UnityEngine.Random.Range(0, _availableEnemies.Count);
        return CloneUtil.Clone(_availableEnemies[randomIndex]);
    }

    public EnemyTemplate GetEnemyTemplate(string id)
    {
        return CloneUtil.Clone(_gameData.GetEnemyTemplate(id));
    }

    public GameSettings GetSettings()
    {
        return _gameData.GetSettings();
    }
}
