using UnityEngine;

public class PlateEnemy : AsteroidEnemy
{
    private Vector2 _lastPlayerPosition;

    public PlateEnemy(CustomProperty<Vector2> playerPosition)
    {
        playerPosition.Subscribe(x => _lastPlayerPosition = x);
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
}
