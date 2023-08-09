using System;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField]
    private BorderData _data;

    public BorderData GetData()
    {
        return _data;
    }
}

[Serializable]
public struct BorderData
{
    public Vector2 ChangeValue;
}