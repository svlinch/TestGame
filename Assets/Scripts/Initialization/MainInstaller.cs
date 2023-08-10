using UnityEngine;
using Assets.Scripts.Factories;
using CustomContainer;

public class MainInstaller : Installer
{
    private void Awake()
    {
        _container.Register<EventService>();
        _container.Register<MainFactory>();
        _container.Register<BalanceManager>();
        _container.Register<BulletsLogic>();
        _container.Register<TimeService>();
        _container.Register<EnemiesLogic>();
        _container.Register<PlayerLogic>();
    }
}
