using UnityEngine;

public class AsteroidEnemy : Enemy
{
    private Vector2 _direction;

    public override void Initialize(EventService eventService, EnemyTemplate template, EnemyUnit unit)
    {
        _eventService = eventService;
        _template = template;
        _unit = unit;

        _direction = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        _unit.Initialize(HandleBullet, HandleBorder);
    }

    public override void HandleUpdate(float dTime)
    {
        _unit.HandleUpdate(_direction * dTime * _template.GetSpeed());
    }

    public override GameObject GetUnitObject()
    {
        return _unit.gameObject;
    }

    public override string GetId()
    {
        return _template.GetId();
    }
    
    public override uint GetScoreReward()
    {
        return _template.GetScoreReward();
    }

    protected override void HandleHarm(Bullet bullet)
    {
        HandleKill(bullet);
    }

    protected override void HandleKill(Bullet bullet)
    {
        _eventService.SendMessage(new EnemyHitedEvent(this as Enemy));
    }

    protected override void HandleBorder(BorderData data)
    {
        _unit.HandleUpdate(data.ChangeValue);
    }

    protected override void HandleBullet(Bullet bullet)
    {
        if (!bullet.Template.GetLetality())
        {
            HandleHarm(bullet);
        }
        else
        {
            HandleKill(bullet);
        }
        _eventService.SendMessage(new BulletHitEvent(bullet));
    }
}
