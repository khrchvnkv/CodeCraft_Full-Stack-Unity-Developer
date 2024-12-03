using Coin;
using Difficulty;
using GameCycle;
using Input;
using Score;
using Snake;
using UI;
using UnityEngine;
using WorldBounds;
using Zenject;

namespace EntryPoint
{
    public class RootInstaller : MonoInstaller
    {
        [SerializeField] private Modules.Coin _coinPrefab;

        public override void InstallBindings()
        {
            CoinInstaller.Install(Container, _coinPrefab);
            DifficultyInstaller.Install(Container);
            GameCycleInstaller.Install(Container);
            InputInstaller.Install(Container);
            ScoreInstaller.Install(Container);
            SnakeInstaller.Install(Container);
            UIInstaller.Install(Container);
            WorldBoundsInstaller.Install(Container);
        }
    }
}