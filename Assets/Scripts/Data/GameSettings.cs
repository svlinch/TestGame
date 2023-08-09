using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

[Serializable]
public class GameSettings
{
    [JsonProperty]
    private float _enemySpawnInterval;
    [JsonProperty]
    private uint _maxEnemiesCount;
    [JsonProperty]
    private List<string> _availableEnemies;
    
    public ReadOnlyCollection<string> GetEnemies()
    {
        return _availableEnemies.AsReadOnly();
    }

    public float GetSpawnInterval()
    {
        return _enemySpawnInterval;
    }

    public uint GetMaxEnemies()
    {
        return _maxEnemiesCount;
    }
}
