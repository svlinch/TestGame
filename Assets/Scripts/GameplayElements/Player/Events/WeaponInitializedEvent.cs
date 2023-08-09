public class WeaponInitializedEvent
{
    public CustomProperty<uint> Stash;
    public CustomProperty<float> Charge;

    public WeaponInitializedEvent(CustomProperty<uint> stash, CustomProperty<float> charge)
    {
        Stash = stash;
        Charge = charge;
    }
}
