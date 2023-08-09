using Zenject;
using Assets.Scripts.Factories;

public class MainInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<EventService>().AsSingle().NonLazy();
        Container.Bind<BalanceManager>().AsSingle().NonLazy();
        Container.Bind<BulletsLogic>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<TimeService>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<MainFactory>().AsSingle().NonLazy();
        Container.Bind<EnemiesLogic>().AsSingle().NonLazy();
        Container.Bind<PlayerLogic>().AsSingle().NonLazy();
    }
}
