using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle().NonLazy();
        Container.Bind<TargetStats>().AsSingle().NonLazy();
        Container.Bind<SaveSystem>().AsSingle().NonLazy();
        
    }
}