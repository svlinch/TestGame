using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

[Serializable]
public class PlayerTemplate
{
    [JsonProperty]
    private List<string> _weaponsIds;
    [JsonProperty]
    private float _maxSpeed;
    [JsonProperty]
    private float accelerationSpeed;
    [JsonProperty]
    private float _momentum;
    [JsonProperty]
    private float _rotationSpeed;

    public ReadOnlyCollection<string> GetWeapons()
    {
        return _weaponsIds.AsReadOnly();
    }

    public float GetMaxSpeed()
    {
        return _maxSpeed;
    }
    
    public float GetAccelerationSpeed()
    {
        return accelerationSpeed;
    }

    public float GetMomentum()
    {
        return _momentum;
    }

    public float GetRotationSpeed()
    {
        return _rotationSpeed;
    }
}
