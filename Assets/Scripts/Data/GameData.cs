using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class GameData
{
    private PlayerTemplate _playerTemplate;
    private WeaponTemplates _weapons;
    private EnemyTemplates _enemies;
    private BulletTemplates _bullets;
    private GameSettings _settings;

    public GameData()
    {
        _playerTemplate = JsonConvert.DeserializeObject<PlayerTemplate>(Resources.Load<TextAsset>("PlayerTemplate").ToString());
        _weapons = JsonConvert.DeserializeObject<WeaponTemplates>(Resources.Load<TextAsset>("Weapons").ToString());
        _enemies = JsonConvert.DeserializeObject<EnemyTemplates>(Resources.Load<TextAsset>("Enemies").ToString());
        _bullets = JsonConvert.DeserializeObject<BulletTemplates>(Resources.Load<TextAsset>("Bullets").ToString());
        _settings = JsonConvert.DeserializeObject<GameSettings>(Resources.Load<TextAsset>("GameSettings").ToString());
    }

    public WeaponTemplate GetWeaponTemplate(string id)
    {
        return _weapons.GetTemplates().FirstOrDefault(x => x.GetId().Equals(id));
    }

    public PlayerTemplate GetPlayerTemplate()
    {
        return _playerTemplate;
    }

    public BulletTemplate GetBulletTemplate(string id)
    {
        return _bullets.GetTemplates().FirstOrDefault(x => x.GetId().Equals(id));
    }

    public GameSettings GetSettings()
    {
        return _settings;
    }

    public EnemyTemplates GetEnemies()
    {
        return _enemies;
    }

    public EnemyTemplate GetEnemyTemplate(string id)
    {
        return _enemies.GetTemplates().FirstOrDefault(x => x.GetId().Equals(id));
    }
}
