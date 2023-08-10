using System;
using UnityEngine;

public class PlateEnemy : AsteroidEnemy
{
    private Vector2 _lastPlayerPosition;
    private Action<Vector2> _subscribeAction;
    private CustomProperty<Vector2> _playerPosition;
    public PlateEnemy(CustomProperty<Vector2> playerPosition)
    {
        _playerPosition = playerPosition;
        _subscribeAction = HandlePositionChange;
        _playerPosition.Subscribe(_subscribeAction);
    }

    public override void Initialize(EventService eventService, EnemyTemplate template, EnemyUnit unit)
    {
        _eventService = eventService;
        _template = template;
        _unit = unit;

        _unit.Initialize(HandleBullet, HandleBorder);
    }

    public override void HandleUpdate(float dTime)
    {
        var pos = _unit.GetPosition();
        var direction = (_lastPlayerPosition - pos).normalized;
        _unit.HandleUpdate(direction * dTime * _template.GetSpeed());
    }
    protected override void HandleKill(Bullet bullet)
    {
        base.HandleKill(bullet);
        _playerPosition.Unsubscribe(_subscribeAction);
    }

    private void HandlePositionChange(Vector2 position)
    {
        _lastPlayerPosition = position;
    }
}
