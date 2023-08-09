using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

[Serializable]
public abstract class Templates<T> where T: class
{
    [JsonProperty]
    private List<T> _templates;

    public ReadOnlyCollection<T> GetTemplates()
    {
        return _templates.AsReadOnly();
    }
}

[Serializable]
public class WeaponTemplates : Templates<WeaponTemplate> { }

[Serializable]
public class EnemyTemplates : Templates<EnemyTemplate> { }

[Serializable]
public class BulletTemplates : Templates<BulletTemplate> { }