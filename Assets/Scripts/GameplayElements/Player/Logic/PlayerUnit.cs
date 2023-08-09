using System;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    [SerializeField]
    private Transform _back;
    [SerializeField]
    private Transform _face;

    private Transform _transform;
    private Action<BorderData> _borderCallback;
    private Action _enemyCallback;

    private void Awake()
    {
        _transform = transform;
    }

    public void SetCallbacks(Action<BorderData> callback, Action enemyCallback)
    {
        _borderCallback = callback;
        _enemyCallback = enemyCallback;
    }

    public void HandleUpdate(Vector3 angle, Vector3 translation)
    {
        _transform.rotation = Quaternion.Euler(angle);
        _transform.Translate(translation);
    }

    public void ChangePosition(Vector2 change, bool update = false)
    {
        if (update)
        {
            _transform.position = new Vector3(change.x, change.y, 0f);
        }
        else
        {
            _transform.position += new Vector3(change.x, change.y, 0f);
        }
    }

    public Vector2 GetPosition()
    {
        return _transform.position;
    }
    
    public Vector2 GetDirection()
    {
        return _face.position - _back.position;
    }

    public void Reset()
    {
        _transform.position = Vector3.zero;
        _transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals(StaticTranslator.BORDER_TAG))
        {
            var border = collision.GetComponent<Border>();
            _borderCallback.Invoke(border.GetData());
        }
        else if (collision.tag.Equals(StaticTranslator.ENEMY_TAG))
        {
            _enemyCallback.Invoke();
        }
    }
}
