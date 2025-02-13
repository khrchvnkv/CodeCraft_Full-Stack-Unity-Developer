using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class SpiderInstaller : MonoInstaller
    {
        [SerializeField] private Spider _spider;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Spider>()
                .FromInstance(_spider)
                .AsSingle();
        }
    }
}