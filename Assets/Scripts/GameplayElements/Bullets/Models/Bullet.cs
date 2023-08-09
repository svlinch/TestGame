using UnityEngine;

public abstract class Bullet
{
    public BulletTemplate Template { get; protected set; }
    public abstract void Initialize(BulletTemplate template, BulletUnit unit, ShootData data);
    public abstract bool HandleUpdate(float dTime);
    public abstract GameObject GetUnitObject();
}
