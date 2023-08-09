using System;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    private Transform _transform;
    private Action<Bullet> _bulletCallback;
    private Action<BorderData> _borderCallback;

    private void Awake()
    {
        _transform = transform;
    }

    public void Initialize(Action<Bullet> bulletCallback, Action<BorderData> borderCallback)
    {
        _bulletCallback = bulletCallback;
        _borderCallback = borderCallback;
    }

    public void HandleUpdate(Vector2 change)
    {
        _transform.position += new Vector3(change.x, change.y, 0f);
    }
    
    public Vector2 GetPosition()
    {
        return _transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(StaticTranslator.BORDER_TAG))
        {
            _borderCallback.Invoke(collision.GetComponent<Border>().GetData());
        }
        else if (collision.tag.Equals(StaticTranslator.BULLET_TAG))
        {
            _bulletCallback.Invoke(collision.GetComponent<BulletUnit>().BulletLogic);
        }
    }
}
