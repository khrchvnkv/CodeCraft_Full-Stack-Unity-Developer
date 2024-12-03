using Zenject;

namespace Score
{
    public class ScoreInstaller : Installer<ScoreInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesTo<Modules.Score>()
                .AsSingle();
        }
    }
}