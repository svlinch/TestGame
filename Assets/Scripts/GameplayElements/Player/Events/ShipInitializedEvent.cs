using UnityEngine;
using UniRx;

public class ShipInitializedEvent
{
    public ReactiveProperty<float> Speed;
    public ReactiveProperty<float> Angle;
    public ReactiveProperty<Vector2> Position;

    public ShipInitializedEvent(ReactiveProperty<float> speed, ReactiveProperty<float> angle, ReactiveProperty<Vector2> position)
    {
        Speed = speed;
        Angle = angle;
        Position = position;
    }
}
