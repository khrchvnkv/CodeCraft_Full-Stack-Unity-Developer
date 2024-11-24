using Controllers.Snake.CoinCollecting;
using Factory;
using Modules;
using Zenject;

namespace Installers
{
    public class CoinInstaller : Installer<Coin, CoinInstaller>
    {
        [Inject] private Coin _coinPrefab;
        
        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<Coin, CoinFactory>()
                .WithInitialSize(5)
                .WithMaxSize(10)
                .ExpandByOneAtATime()
                .FromComponentInNewPrefab(_coinPrefab)
                .AsSingle();

            Container
                .Bind<ICoinFactory>()
                .To<CoinFactory>()
                .FromResolve();

            Container
                .Bind<ICoinCollision>()
                .To<CoinFactory>()
                .FromResolve();
        }
    }
}