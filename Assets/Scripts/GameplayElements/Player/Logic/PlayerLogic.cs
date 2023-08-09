using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Factories;
using UniRx;

public class PlayerLogic: IStarter
{
    #region Injection
    private EventService _eventService;
    private BalanceManager _balanceManager;
    private IObjectFactory _factory;
    private SubscriptionHolder _subscriptions;

    public void Inject(EventService eventService, MainFactory factory, BalanceManager balanceManager)
    {
        _factory = factory as IObjectFactory;
        _balanceManager = balanceManager;

         _eventService = eventService;
        _subscriptions = new SubscriptionHolder(_eventService);
        _subscriptions.Subscribe<MoveEvent>(HandleMoveEvent);
        _subscriptions.Subscribe<RotateEvent>(HandleRotateEvent);
        _subscriptions.Subscribe<ShootEvent>(HandleShootEvent);
        _subscriptions.Subscribe<GameStateChangeEvent>(HandleGameStateChange);
        _subscriptions.Subscribe<EnemyHitedEvent>(HandleEnemyHited);
    }
    #endregion

    private ReactiveProperty<uint> _score;
    private WeaponsLogic _weaponsLogic;
    private PlayerMoveLogic _moveLogic;

    public IEnumerator Initialize()
    {
        var template = _balanceManager.GetPlayerTemplate();
        InitializeWeapons(template);

        var description = FactoryDescriptionBuilder.Object()
            .Kind(StaticTranslator.PLAYER)
            .Parent(null)
            .Position(Vector3.zero)
            .Type(EObjectType.Player)
            .Build();

        var unit = _factory.Create(description).GetComponent<PlayerUnit>();
        _moveLogic = new PlayerMoveLogic(unit, template, _eventService);

        _score = new ReactiveProperty<uint>(0);
        _eventService.SendMessage(new PlayerInitializedEvent(_score));
        yield return null;
    }

    public Type Type()
    {
        return GetType();
    }

    public void HandleUpdate(float dTime)
    {
        _moveLogic.HandleUpdate(dTime);
        _weaponsLogic.HandleUpdate(dTime);
    }

    private bool HandleMoveEvent(MoveEvent e)
    {
        _moveLogic.UpdateAcceleration(e.State);
        return true;
    }
    
    private bool HandleRotateEvent(RotateEvent e)
    {
        _moveLogic.UpdateRotateDirection(e.Direction);
        return true;
    }
    
    private bool HandleShootEvent(ShootEvent e)
    {
        _weaponsLogic.WeaponAct(e.WeaponIndex, _moveLogic.GetShootData());
        return true;
    }

    private bool HandleEnemyHited(EnemyHitedEvent e)
    {
        _score.Value += e.Enemy.GetScoreReward();
        return true;
    }

    private void InitializeWeapons(PlayerTemplate template)
    {
        var weaponList = new List<WeaponTemplate>();

        foreach (var id in template.GetWeapons())
        {
            var wTemplate = _balanceManager.GetWeaponTemplate(id);
            if (wTemplate != null)
            {
                weaponList.Add(wTemplate);
            }
        }
        _weaponsLogic = new WeaponsLogic(_eventService, weaponList);
    }

    private bool HandleGameStateChange(GameStateChangeEvent e)
    {
        if (e.State)
        {
            _score.Value = 0;
        }
        _moveLogic.Reset();
        _weaponsLogic.Reset();
        return true;
    }
}
