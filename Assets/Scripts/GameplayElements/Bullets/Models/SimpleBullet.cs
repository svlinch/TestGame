using UnityEngine;
using Assets.Scripts.Utility;

public class SimpleBullet: Bullet
{
    private SimpleBulletUnit _unit;

    private Vector2 _direction;
    private GameTimer _timer;

    public override void Initialize(BulletTemplate template, BulletUnit unit, ShootData data)
    {
        Template = template;
        _unit = unit as SimpleBulletUnit;
        _unit.Initialize(this);

        _direction = data.Direction;
        _unit.RotateSprite(data.Angle);
        _timer = new GameTimer(Template.GetLifeTime());
    }

    public override bool HandleUpdate(float dTime)
    {
        _unit.HandleUpdate(_direction * dTime * Template.GetSpeed());

        if (_timer.HandleUpdate(dTime))
        {
            return false;
        }
        return true;
    }

    public override GameObject GetUnitObject()
    {
        return _unit.gameObject;
    }
}
