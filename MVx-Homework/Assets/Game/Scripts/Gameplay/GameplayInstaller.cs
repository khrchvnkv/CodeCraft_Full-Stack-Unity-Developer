using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
    //Don't modify
    [CreateAssetMenu(
        fileName = "GameplayInstaller",
        menuName = "Zenject/New GameplayInstaller"
    )]
    public sealed class GameplayInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private int _initialMoney = 300;

        public override void InstallBindings()
        {
            MoneyInstaller.Install(this.Container, _initialMoney);
        }
    }
}