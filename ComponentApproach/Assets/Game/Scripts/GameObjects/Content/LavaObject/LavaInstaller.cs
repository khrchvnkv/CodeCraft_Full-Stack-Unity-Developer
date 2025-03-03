using Game.Scripts.GameObjects.Core;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content.LavaObject
{
    public class LavaInstaller : MonoInstaller
    {
        [SerializeField] private TriggerEventReceiver _triggerEventReceiver;
        [SerializeField] private AudioSource _audio;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<Lava>()
                .AsSingle()
                .WithArguments(_audio);

            Container
                .BindInterfacesAndSelfTo<TriggerEventReceiver>()
                .FromInstance(_triggerEventReceiver)
                .AsSingle();
        }
    }
}