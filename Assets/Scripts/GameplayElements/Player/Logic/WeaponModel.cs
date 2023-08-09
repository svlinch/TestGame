using Assets.Scripts.Utility;
using UniRx;

public class WeaponModel
{
    private EventService _eventService;

    private WeaponTemplate _template;
    private GameTimer _chargeTimer;
    private ReactiveProperty<uint> _stash;
    private ReactiveProperty<float> _charge;

    public WeaponModel(WeaponTemplate template, EventService eventService)
    {
        _eventService = eventService;
        _template = template;
        _stash = new ReactiveProperty<uint>(0);
        _charge = new ReactiveProperty<float>(0);

        if (_template.GetStashSize() > 0)
        {
            _chargeTimer = new GameTimer(_template.GetChargeTime());
            if (_template.GetId().Equals(StaticTranslator.LASER_ID))
            {
                _eventService.SendMessage(new WeaponInitializedEvent(_stash, _charge));
            }
        }
    }

    public void HandleUpdate(float dTime)
    {
        if (_stash.Value == _template.GetStashSize())
        {
            return;
        }

        _charge.Value = _chargeTimer.GetRemains();
        if (_chargeTimer.HandleUpdate(dTime))
        {
            _stash.Value++;
            _chargeTimer.CheckoutTimer(_template.GetChargeTime());
        }
    }

    public void TryToAct(ShootData data)
    {
        if (_stash.Value > 0)
        {
            _stash.Value--;
        }
        else if (_template.GetStashSize() != 0)
        {
            return;
        }
        _eventService.SendMessage(new WeaponActEvent(_template, data));
    }

    public void Reset()
    {
        if (_template.GetStashSize() > 0)
        {
            _stash.Value = 0;
            _chargeTimer.CheckoutTimer(_template.GetChargeTime());
            _charge.Value = _chargeTimer.GetRemains();
        }
    }
}
