using UniRx;

public class WeaponInitializedEvent
{
    public ReactiveProperty<uint> Stash;
    public ReactiveProperty<float> Charge;

    public WeaponInitializedEvent(ReactiveProperty<uint> stash, ReactiveProperty<float> charge)
    {
        Stash = stash;
        Charge = charge;
    }
}
