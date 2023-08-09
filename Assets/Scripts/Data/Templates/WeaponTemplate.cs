using System;
using Newtonsoft.Json;

[Serializable]
public class WeaponTemplate
{
    [JsonProperty]
    private string _id;
    [JsonProperty]
    private string _spawnId;
    [JsonProperty]
    private float _chargeTime;
    [JsonProperty]
    private uint _stashSize;

    public string GetId()
    {
        return _id;
    }

    public string GetSpawnId()
    {
        return _spawnId;
    }

    public float GetChargeTime()
    {
        return _chargeTime;
    }

    public uint GetStashSize()
    {
        return _stashSize;
    }
}
