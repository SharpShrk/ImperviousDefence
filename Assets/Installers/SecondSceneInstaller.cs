using UserInterface;
using VContainer;
using VContainer.Unity;

public class SecondSceneInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameOverHandler>();
    }
}