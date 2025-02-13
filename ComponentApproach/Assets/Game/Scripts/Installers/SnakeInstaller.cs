using Game.Scripts.Objects;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private Snake _snake;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();
        }
    }
}