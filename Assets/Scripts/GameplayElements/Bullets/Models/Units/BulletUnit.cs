using UnityEngine;

public abstract class BulletUnit : MonoBehaviour
{
    public Bullet BulletLogic { get; protected set; }
    public abstract void Initialize(Bullet bullet);
    public abstract void HandleUpdate(Vector2 change);
    public abstract void RotateSprite(float angle);
}
