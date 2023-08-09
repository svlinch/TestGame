using UnityEngine;

public abstract class Enemy
{
    protected EventService _eventService;
    protected EnemyTemplate _template;
    protected EnemyUnit _unit;
    public abstract void Initialize(EventService eventService, EnemyTemplate template, EnemyUnit unit);
    public abstract void HandleUpdate(float dTime);
    public abstract GameObject GetUnitObject();
    public abstract string GetId();
    public abstract uint GetScoreReward();
    protected abstract void HandleHarm(Bullet bullet);
    protected abstract void HandleKill(Bullet bullet);
    protected abstract void HandleBorder(BorderData data);
    protected abstract void HandleBullet(Bullet bullet);
}
