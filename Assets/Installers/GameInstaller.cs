using VContainer;
using VContainer.Unity;
using UnityEngine;
using LeaderBoard;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private LeaderboardLoader _leaderboardLoader;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    protected override void Configure(IContainerBuilder builder)
    { 
        builder.RegisterComponent(_leaderboardLoader).As<LeaderboardLoader>();
    }
}