using System;
using Newtonsoft.Json;

[Serializable]
public class BulletTemplate
{
    [JsonProperty]
    private string _id;

    [JsonProperty]
    private float _lifeTime;

    [JsonProperty]
    private float _speed;

    [JsonProperty]
    private bool _letality;

    [JsonProperty]
    private bool _destructible;

    public string GetId()
    {
        return _id;
    } 
    
    public float GetLifeTime()
    {
        return _lifeTime;
    } 
    
    public float GetSpeed()
    {
        return _speed;
    } 
    
    public bool GetLetality()
    {
        return _letality;
    }

    public bool GetDestructible()
    {
        return _destructible;
    }
}