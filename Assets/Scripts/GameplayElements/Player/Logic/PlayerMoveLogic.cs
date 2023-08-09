using UnityEngine;

public class PlayerMoveLogic
{
    private EventService _eventService;

    private PlayerUnit _unit;
    private PlayerTemplate _template;

    private CustomProperty<float> _speed;
    private CustomProperty<float> _angle;
    private CustomProperty<Vector2> _position;

    private bool _acceleration;
    private Vector2 _rotateDirection;

    public PlayerMoveLogic(PlayerUnit unit, PlayerTemplate template, EventService eventService)
    {
        _eventService = eventService;
        _unit = unit;
        _template = template;

        _unit.SetCallbacks(HandleBorder, HandleEnemy);

        _speed = new CustomProperty<float>();
        _angle = new CustomProperty<float>();
        _position = new CustomProperty<Vector2>();

        _eventService.SendMessage(new ShipInitializedEvent(_speed, _angle, _position));
    }

    public void HandleUpdate(float dTime)
    {
        CheckoutSpeed(dTime);
        CheckoutRotation(dTime);

        _unit.HandleUpdate(Vector3.forward * _angle.Value, Vector3.up * _speed.Value * dTime);
        _position.Value = _unit.GetPosition();
    }

    public void Reset()
    {
        _speed.Value = 0;
        _angle.Value = 0;
        _position.Value = Vector2.zero;

        _unit.Reset();
    }

    public void UpdateAcceleration(bool state)
    {
        _acceleration = state;
    }

    public void UpdateRotateDirection(Vector2 direction)
    {
        _rotateDirection = direction;
    }

    public ShootData GetShootData()
    {
        return new ShootData(_unit.GetPosition(), _unit.GetDirection(), _angle.Value);
    }

    private void CheckoutSpeed(float dTime)
    {
        _speed.Value += _acceleration ? _template.GetAccelerationSpeed() * dTime : _template.GetMomentum() * dTime;
        _speed.Value = Mathf.Min(_speed.Value, _template.GetMaxSpeed());
        if (_speed.Value < 0f)
        {
            _speed.Value = 0f;
        }
    }

    private void CheckoutRotation(float dTime)
    {
        var newAngle = _angle.Value - _rotateDirection.x * _template.GetRotationSpeed() * dTime;
        if (newAngle > 360)
        {
            newAngle -= 360;
        }
        else if (newAngle < -360)
        {
            newAngle += 360;
        }
        _angle.Value = newAngle;
    }

    private void HandleBorder(BorderData borderData)
    {
        _unit.ChangePosition(borderData.ChangeValue);
        _position.Value = _unit.GetPosition();
    }

    private void HandleEnemy()
    {
        _eventService.SendMessage(new GameStateChangeEvent(false));
    }
}
