using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Core;
using Game.Scripts.GameObjects.View.ThrowUp;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.SpringboardObject
{
    public class SpringboardInstaller : MonoInstaller
    {
        [SerializeField] private float _force;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private TriggerEventReceiver _triggerEventReceiver;
        [SerializeField] private AudioSource _audio;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Springboard>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<SpringboardConditions>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<TossComponent>()
                .AsSingle()
                .WithArguments(_force, _direction);

            Container
                .BindInterfacesAndSelfTo<TriggerDetector>()
                .AsSingle()
                .WithArguments(_triggerEventReceiver);

            Container
                .BindInterfacesAndSelfTo<TossAudioComponent>()
                .AsSingle()
                .WithArguments(_audio);

            Container
                .BindInterfacesAndSelfTo<TriggerEventReceiver>()
                .FromInstance(_triggerEventReceiver)
                .AsSingle();
        }
    }
}