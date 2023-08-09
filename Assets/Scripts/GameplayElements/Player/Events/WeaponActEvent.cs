using UnityEngine;

public class WeaponActEvent
{
    public WeaponTemplate Template;
    public ShootData Data;

    public WeaponActEvent(WeaponTemplate template, ShootData data)
    {
        Template = template;
        Data = data;
    }
}

public class ShootData
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Angle;

    public ShootData(Vector2 position, Vector2 direction, float angle)
    {
        Position = position;
        Direction = direction;
        Angle = angle;
    }
}
