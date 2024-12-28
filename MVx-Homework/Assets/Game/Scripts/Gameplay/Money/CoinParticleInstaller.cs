using Game.Gameplay.Contracts;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    public class CoinParticleInstaller : MonoInstaller
    {
        [SerializeField] private CoinParticleEffect _prefab;
        [SerializeField] private Transform _poolContainer;
        
        public override void InstallBindings()
        {
            Container
                .BindMemoryPool<CoinParticleEffect, CoinParticleEffect.Pool>()
                .WithMaxSize(5)
                .FromComponentInNewPrefab(_prefab)
                .UnderTransform(_poolContainer)
                .AsSingle();

            Container
                .Bind<ICoinParticleFactory>()
                .To<CoinParticleEffect.Pool>()
                .FromResolve();
        }
    }
}