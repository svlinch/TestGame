using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;
using UniRx;

public class LoseCanvas : MonoBehaviour
{
    #region Injection
    private EventService _eventService;
    private SubscriptionHolder _subscriptions;

    [Inject]
    private void Inject(EventService eventService)
    {
        _eventService = eventService;
        _subscriptions = new SubscriptionHolder(_eventService);
        _subscriptions.Subscribe<GameStateChangeEvent>(GameStateChangeHandle);
        _subscriptions.Subscribe<PlayerInitializedEvent>(PlayerInitHandle);
    }
    #endregion

    [SerializeField]
    private Canvas _canvas;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private TextMeshProUGUI _textScore;

    private uint _currentScore;

    private void Awake()
    {
        _restartButton.onClick.AddListener(RestartHandle);
        _canvas.enabled = false;
    }

    private void RestartHandle()
    {
        _eventService.SendMessage(new GameStateChangeEvent(true));
    }

    private bool GameStateChangeHandle(GameStateChangeEvent e)
    {
        if (!e.State)
        {
            _textScore.text = _currentScore.ToString();
        }

        _canvas.enabled = !e.State;
        return true;
    }

    private bool PlayerInitHandle(PlayerInitializedEvent e)
    {
        e.Score.Subscribe(x => _currentScore = x);
        return true;
    }
}
