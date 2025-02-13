using Game.Scripts.Components;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class JumpInstaller : MonoInstaller
    {
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _jumpCooldown;

        public override void InstallBindings()
        {
            Container
                .Bind<JumpComponent>()
                .AsSingle()
                .WithArguments(_jumpForce, _jumpCooldown);
        }
    }
}