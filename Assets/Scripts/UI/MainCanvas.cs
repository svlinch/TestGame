using UnityEngine;
using Zenject;
using TMPro;
using UniRx;

public class MainCanvas : MonoBehaviour
{
    #region Injection
    private EventService _eventService;
    private SubscriptionHolder _subscriptions;

    [Inject]
    private void Inject(EventService eventService)
    {
        _eventService = eventService;

        _subscriptions = new SubscriptionHolder(_eventService);
        _subscriptions.Subscribe<ShipInitializedEvent>(HandleShipInitialized);
        _subscriptions.Subscribe<WeaponInitializedEvent>(HandleWeaponInitialized);
        _subscriptions.Subscribe<GameStateChangeEvent>(HandleGameStateChange);
    }
    #endregion

    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private TextMeshProUGUI _textPosition;
    [SerializeField]
    private TextMeshProUGUI _textAngle;
    [SerializeField]
    private TextMeshProUGUI _textSpeed;
    [SerializeField]
    private TextMeshProUGUI _textStash;
    [SerializeField]
    private TextMeshProUGUI _textCharge;

    private bool HandleShipInitialized(ShipInitializedEvent e)
    {
        e.Position.Subscribe(x => _textPosition.text =  x.ToString("F1"));
        e.Angle.Subscribe(x => _textAngle.text = x.ToString("F1"));
        e.Speed.Subscribe(x => _textSpeed.text = x.ToString("F1"));
        return true;
    }

    private bool HandleWeaponInitialized(WeaponInitializedEvent e)
    {
        e.Charge.Subscribe(x => _textCharge.text = x.ToString("F1"));
        e.Stash.Subscribe(x => _textStash.text = x.ToString("F1"));
        return true;
    }

    private bool HandleGameStateChange(GameStateChangeEvent e)
    {
        _canvas.enabled = e.State;
        return true;
    }
}
