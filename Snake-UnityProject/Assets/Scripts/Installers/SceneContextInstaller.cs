using Controllers.Coin.Spawn;
using Controllers.Snake.CoinCollecting;
using Controllers.Snake.Movement;
using Controllers.UI;
using Input;
using Modules;
using Observers.Snake;
using SnakeGame;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneContextInstaller : MonoInstaller
    {
        [SerializeField] private WorldBounds _worldBounds;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Snake _snake;
        [SerializeField] private Coin _coinPrefab;

        public override void InstallBindings()
        {
            BindCore();
            BindWorld();
            BindFactories();
        }

        private void BindCore()
        {
            Container
                .Bind<IDifficulty>()
                .To<Difficulty>()
                .AsSingle()
                .WithArguments(9);
            
            Container
                .Bind<IScore>()
                .To<Score>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<StandaloneInputAdapter>()
                .AsSingle();

            Container
                .BindInterfacesTo<SnakeMovementController>()
                .AsSingle();

            Container
                .BindInterfacesTo<SnakeDeathObserver>()
                .AsSingle();

            Container
                .BindInterfacesTo<CoinsStateController>()
                .AsSingle();

            Container
                .BindInterfacesTo<CoinCollectingController>()
                .AsSingle();

            Container
                .BindInterfacesTo<UIController>()
                .AsSingle();
        }
        
        private void BindWorld()
        {
            Container
                .Bind<IWorldBounds>()
                .FromInstance(_worldBounds)
                .AsSingle();
            
            Container
                .Bind<IGameUI>()
                .To<GameUI>()
                .FromInstance(_gameUI)
                .AsSingle();
            
            Container
                .Bind<ISnake>()
                .To<Snake>()
                .FromInstance(_snake)
                .AsSingle();
        }

        private void BindFactories() => 
            CoinInstaller.Install(Container, _coinPrefab);
    }
}