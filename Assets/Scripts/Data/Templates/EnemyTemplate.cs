using System;
using Newtonsoft.Json;

[Serializable]
public class EnemyTemplate
{
    [JsonProperty]
    private string _id;
    [JsonProperty]
    private float _speed;
    [JsonProperty]
    private EEnemyType _enemyType;
    [JsonProperty]
    private uint _scoreReward;

    public string GetId()
    {
        return _id;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public EEnemyType GetEnemyType()
    {
        return _enemyType;
    }

    public uint GetScoreReward()
    {
        return _scoreReward;
    }
}

public enum EEnemyType
{
    Asteroid,
    BigAsteroid,
    Plate
}
