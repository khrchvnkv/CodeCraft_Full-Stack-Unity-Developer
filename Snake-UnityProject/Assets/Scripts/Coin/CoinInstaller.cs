using Zenject;

namespace Coin
{
    public class CoinInstaller : Installer<Modules.Coin, CoinInstaller>
    {
        [Inject] private readonly Modules.Coin _coinPrefab; 
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<CoinsDifficultyController>()
                .AsSingle();

            Container
                .BindInterfacesTo<CoinCollectingController>()
                .AsSingle();
            
            Container
                .BindMemoryPool<Modules.Coin, CoinPool>()
                .WithInitialSize(5)
                .WithMaxSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .UnderTransformGroup("[World]")
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<CoinManager>()
                .AsSingle();
        }
    }
}