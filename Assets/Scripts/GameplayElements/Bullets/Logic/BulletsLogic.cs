using System.Collections.Generic;
using Zenject;
using Assets.Scripts.Factories;

public class BulletsLogic
{
    #region Injection
    private EventService _eventService;
    private SubscriptionHolder _subscriptions;
    private PoolFactory _poolFactory;
    private BalanceManager _balanceManager;

    [Inject]
    private void Inject(EventService eventService, IObjectFactory factory, BalanceManager balanceManager)
    {
        _eventService = eventService;
        _balanceManager = balanceManager;

        _poolFactory = new PoolFactory(factory, "bullets");

        _subscriptions = new SubscriptionHolder(_eventService);
        _subscriptions.Subscribe<WeaponActEvent>(WeaponActHandle);
        _subscriptions.Subscribe<BulletHitEvent>(BulletHitHandle);
        _subscriptions.Subscribe<GameStateChangeEvent>(GameStateChangeHandle);
    }
    #endregion

    private List<Bullet> _bullets = new List<Bullet>();

    public void HandleUpdate(float dTime)
    {
        for (int i = 0; i < _bullets.Count; i++)
        {
            if (!_bullets[i].HandleUpdate(dTime))
            {
                _poolFactory.ReturnToPool(_bullets[i].GetUnitObject(), _bullets[i].Template.GetId());
                _bullets.RemoveAt(i);
                i--;
            }
        }
    }

    private bool WeaponActHandle(WeaponActEvent e)
    {
        var description = FactoryDescriptionBuilder.Object()
                        .Kind(e.Template.GetSpawnId())
                        .Position(e.Data.Position)
                        .Parent(null)
                        .Type(EObjectType.Bullet)
                        .Build();

        var bulletUnit = _poolFactory.Create(description).GetComponent<BulletUnit>();
        var template = _balanceManager.GetBulletTemplate(e.Template.GetSpawnId());

        var bulletModel = new SimpleBullet();
        bulletModel.Initialize(template, bulletUnit, e.Data);

        _bullets.Add(bulletModel);
        return true;
    }

    private bool BulletHitHandle(BulletHitEvent e)
    {
        if (!e.BulletLogic.Template.GetDestructible())
        {
            return true;
        }
        _poolFactory.ReturnToPool(e.BulletLogic.GetUnitObject(), e.BulletLogic.Template.GetId());
        _bullets.Remove(e.BulletLogic);
        return true;
    }

    private bool GameStateChangeHandle(GameStateChangeEvent e)
    {
        if (!e.State)
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                _poolFactory.ReturnToPool(_bullets[i].GetUnitObject(), _bullets[i].Template.GetId());
                _bullets.RemoveAt(i);
                i--;
            }
        }
        return true;
    }
}
