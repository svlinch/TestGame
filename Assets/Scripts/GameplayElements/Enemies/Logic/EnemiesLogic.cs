using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Factories;
using Assets.Scripts.Utility;
using UniRx;

public class EnemiesLogic: IStarter
{
    #region Injection
    private EventService _eventService;
    private PoolFactory _factory;
    private BalanceManager _balanceManager;
    private SubscriptionHolder _subscriptions;

    public void Inject(EventService eventService, MainFactory factory, BalanceManager balanceManager)
    {
        _eventService = eventService;
        _factory = new PoolFactory(factory as IObjectFactory, "enemies");
        _balanceManager = balanceManager;

        _subscriptions = new SubscriptionHolder(eventService);
        _subscriptions.Subscribe<EnemyHitedEvent>(EnemyHitedHandle);
        _subscriptions.Subscribe<EnemySpecialEvent>(EnemySpecialHandle);
        _subscriptions.Subscribe<ShipInitializedEvent>(ShipInitializedHandle);
        _subscriptions.Subscribe<GameStateChangeEvent>(GameStateChangeHandle);
    }
    #endregion

    private List<Enemy> _enemies;
    private GameTimer _spawnTimer;
    private ReactiveProperty<Vector2> _lastPlayerPosition;

    public IEnumerator Initialize()
    {
        _spawnTimer = new GameTimer(_balanceManager.GetSettings().GetSpawnInterval());
        _lastPlayerPosition = new ReactiveProperty<Vector2>();
        _enemies = new List<Enemy>();
        _enemies.Capacity = (int)_balanceManager.GetSettings().GetMaxEnemies();
        yield return null;
    }

    public Type Type()
    {
        return GetType();
    }

    public void HandleUpdate(float dTime)
    {
        if (_spawnTimer.HandleUpdate(dTime))
        {
            _spawnTimer.CheckoutTimer(_balanceManager.GetSettings().GetSpawnInterval());
            SpawnEnemy();
        }

        foreach(var enemy in _enemies)
        {
            enemy.HandleUpdate(dTime);
        }
    }

    private void SpawnEnemy()
    {
        if (_enemies.Count == _balanceManager.GetSettings().GetMaxEnemies())
        {
            return;
        }
        var template = _balanceManager.GetRandomEnemy();
        var newEnemy = CreateEnemy(template);

        _enemies.Add(newEnemy);
    }

    private bool EnemyHitedHandle(EnemyHitedEvent e)
    {
        _factory.ReturnToPool(e.Enemy.GetUnitObject(), e.Enemy.GetId());
        _enemies.Remove(e.Enemy);
        return true;
    }

    private bool EnemySpecialHandle(EnemySpecialEvent e)
    {
        //switch special type
        //один шаблон для всех объектов, но в нем нет функционала для внесения изменений*
        var randomCount = UnityEngine.Random.Range(2, 5);
        var template = _balanceManager.GetEnemyTemplate(StaticTranslator.LITTLE_ASTEROID_ID);
        
        for (int i = 0; i < randomCount; i++)
        {
            var newEnemy = CreateEnemy(template, e.Position);
            _enemies.Add(newEnemy);
        }
        return true;
    }

    private bool ShipInitializedHandle(ShipInitializedEvent e)
    {
        e.Position.Subscribe(x => _lastPlayerPosition.Value = x);
        return true;
    }

    private bool GameStateChangeHandle(GameStateChangeEvent e)
    {
        if (!e.State)
        {
            for(int i = 0; i < _enemies.Count; i++)
            {
                _factory.ReturnToPool(_enemies[i].GetUnitObject(), _enemies[i].GetId());
                _enemies.RemoveAt(i);
                i--;
            }
        }
        return true;
    }

    private Enemy CreateEnemy(EnemyTemplate template, Vector2? position = null)
    {
        Enemy newEnemy = null;
        switch (template.GetEnemyType())
        {
            case EEnemyType.BigAsteroid: newEnemy = new BigAsteroidEnemy(); break;
            case EEnemyType.Plate: newEnemy = new PlateEnemy(_lastPlayerPosition); break;
            case EEnemyType.Asteroid: newEnemy = new AsteroidEnemy(); break;
        }

        if (position == null)
        {
            position = GetRandomPosition();
        }

        var description = FactoryDescriptionBuilder.Object()
                       .Kind(template.GetId())
                       .Parent(null)
                       .Position(position.Value)
                       .Type(EObjectType.Enemy)
                       .Build();
        var enemyUnit = _factory.Create(description).GetComponent<EnemyUnit>();

        newEnemy.Initialize(_eventService, template, enemyUnit);
        return newEnemy;
    }

    private Vector2 GetRandomPosition()
    {
        var x = UnityEngine.Random.Range(-9f, 9f);
        var y = UnityEngine.Random.Range(-4.6f, 4.6f);
        if (Mathf.Abs(_lastPlayerPosition.Value.x - x) + Mathf.Abs(_lastPlayerPosition.Value.y - y) < 5f)
        {
            return GetRandomPosition();
        }
        return new Vector2(x, y);
    }
}
