using Game.Scripts.GameContext;
using Game.Scripts.GameObjects.Content.Triggers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.GameObjects.Content
{
    public class LavaInstaller : MonoInstaller
    {
        [SerializeField] private TriggerEventReceiver _triggerEventReceiver;
        [SerializeField] private AudioSource _audio;

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<KillZoneController>()
                .AsSingle()
                .WithArguments(_triggerEventReceiver, _audio);
        }
    }
}