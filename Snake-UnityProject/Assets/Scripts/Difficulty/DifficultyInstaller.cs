using Zenject;

namespace Difficulty
{
    public class DifficultyInstaller : Installer<DifficultyInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Modules.Difficulty>()
                .AsSingle()
                .WithArguments(9);
        }
    }
}