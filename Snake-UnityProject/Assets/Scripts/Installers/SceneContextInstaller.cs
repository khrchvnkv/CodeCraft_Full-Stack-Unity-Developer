using Controllers;
using Input;
using Modules;
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
                .AsSingle();
            
            Container
                .Bind<IScore>()
                .To<Score>()
                .AsSingle();
            
            Container
                .BindInterfacesTo<StandaloneInputAdapter>()
                .AsSingle();

            Container
                .BindInterfacesTo<SnakeController>()
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