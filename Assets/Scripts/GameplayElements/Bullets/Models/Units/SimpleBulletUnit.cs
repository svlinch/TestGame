using UnityEngine;

public class SimpleBulletUnit : BulletUnit
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public override void Initialize(Bullet bullet)
    {
        BulletLogic = bullet;
    }

    public override void HandleUpdate(Vector2 change)
    {
        _transform.Translate(change);
    }

    public override void RotateSprite(float angle)
    {
        _transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }
}
