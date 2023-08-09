using System.Collections.Generic;

public class WeaponsLogic
{
    private List<WeaponModel> _weapons;

    public WeaponsLogic(EventService eventService, List<WeaponTemplate> weapons)
    {
        InitializeWeapons(weapons, eventService);
    }

    public void HandleUpdate(float dTime)
    {
        foreach(var weapon in _weapons)
        {
            weapon.HandleUpdate(dTime);
        }
    }

    public void WeaponAct(int index, ShootData data)
    {
        _weapons[index].TryToAct(data);
    }

    public void Reset()
    {
        foreach(var weapon in _weapons)
        {
            weapon.Reset();
        }
    }

    private void InitializeWeapons(List<WeaponTemplate> weapons, EventService eventService)
    {
        _weapons = new List<WeaponModel>();
        foreach(var weapon in weapons)
        {
            var newModel = new WeaponModel(weapon, eventService);
            _weapons.Add(newModel);
        }
    }
}
