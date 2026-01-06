using UnityEngine.InputSystem;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TargetStats>().AsSingle();
        Container.Bind<TargetStatsView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputReader>().FromComponentInHierarchy().AsSingle();
        Container.Bind<TargetManager>().FromComponentInHierarchy().AsSingle();
    }
}
