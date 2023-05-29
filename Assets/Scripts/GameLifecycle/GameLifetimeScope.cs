using Gameplay.GameplayObjects;
using Gameplay.UI;
using Mics;
using Scripts.Gameplay.GameplayObjects;
using Unity.BossRoom.Infrastructure;
using VContainer;
using VContainer.Unity;

namespace GameLifcycle
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponentInHierarchy<ShareDataSO>();
            builder.RegisterComponentInHierarchy<Attacker>();
            builder.RegisterComponentInHierarchy<MyCamera>();
            builder.RegisterComponentInHierarchy<Victim>();
            builder.RegisterComponentInHierarchy<GameUI>();

            builder.RegisterInstance(new MessageChannel<ShareData.PlayGame>()).AsImplementedInterfaces();
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}