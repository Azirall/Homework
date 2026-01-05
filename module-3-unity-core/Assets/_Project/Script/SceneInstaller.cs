using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TargetStats>().AsSingle().NonLazy();
        Container.Bind<TargetStatsView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputReader>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TargetManager>().FromComponentInHierarchy().AsSingle();
    }
}