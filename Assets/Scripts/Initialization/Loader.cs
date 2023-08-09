using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Loader : MonoBehaviour
{
    private List<IStarter> _startList;
    private EventService _eventService;
    
    [Inject]
    private void Inject(EventService eventService, GamePlayController gamePlayController, BalanceManager balanceManager,
            PlayerLogic playerLogic, EnemiesLogic enemiesLogic)
    {
        _eventService = eventService;

        _startList = new List<IStarter>();
        _startList.Add(balanceManager as IStarter);
        _startList.Add(gamePlayController as IStarter);
        _startList.Add(enemiesLogic as IStarter);
        _startList.Add(playerLogic as IStarter);
    }

    private IEnumerator Start()
    {
        foreach (var toStart in _startList)
        {
            yield return StartCoroutine(toStart.Initialize());
            Debug.Log(string.Format("{0} initialized", toStart.GetType().ToString()));
        }
        _eventService.SendMessage(new GameStateChangeEvent(true));
    }
}
