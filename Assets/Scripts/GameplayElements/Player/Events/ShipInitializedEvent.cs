using UnityEngine;

public class ShipInitializedEvent
{
    public CustomProperty<float> Speed;
    public CustomProperty<float> Angle;
    public CustomProperty<Vector2> Position;

    public ShipInitializedEvent(CustomProperty<float> speed, CustomProperty<float> angle, CustomProperty<Vector2> position)
    {
        Speed = speed;
        Angle = angle;
        Position = position;
    }
}
