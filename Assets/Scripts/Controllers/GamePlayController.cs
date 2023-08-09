using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GamePlayController : MonoBehaviour, IStarter
{
    #region Injection
    private ITimeService _timeService;
    private BulletsLogic _bulletsLogic;
    private EnemiesLogic _enemiesLogic;
    private PlayerLogic _playerLogic;

    private SubscriptionHolder _subscriptions;

    [Inject]
    private void Inject(ITimeService timeService, EventService eventService, BulletsLogic bulletsLogic, 
                EnemiesLogic enemiesLogic, PlayerLogic playerLogic)
    {
        _timeService = timeService;
        _bulletsLogic = bulletsLogic;
        _playerLogic = playerLogic;
        _enemiesLogic = enemiesLogic;

        _subscriptions = new SubscriptionHolder(eventService);
        _subscriptions.Subscribe<GameStateChangeEvent>(GameStateChangeHandle);
    }
    #endregion


    public IEnumerator Initialize()
    {
        yield break;
    }

    public Type Type()
    {
        return GetType();
    }

    private void Update()
    {
        if (_timeService.Pause)
        {
            return;
        }
        var deltaTime = _timeService.GetDeltaTime(false);
        _playerLogic.HandleUpdate(deltaTime);
        _enemiesLogic.HandleUpdate(deltaTime);
        _bulletsLogic.HandleUpdate(deltaTime);
    }

    private bool GameStateChangeHandle(GameStateChangeEvent e)
    {
        switch (e.State)
        {
            case true: _timeService.SetPauseState(false); break;
            case false: _timeService.SetPauseState(true); break;
        }
        return true;
    }
}
