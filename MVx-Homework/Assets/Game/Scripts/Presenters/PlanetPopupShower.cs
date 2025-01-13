using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupShower : IInitializable
    {
        private readonly PlanetPopupPresenter _presenter;
        private readonly PlanetPopup _view;

        public PlanetPopupShower(
            PlanetPopupPresenter presenter, 
            PlanetPopup view)
        {
            _presenter = presenter;
            _view = view;
        }

        void IInitializable.Initialize() => _view.Hide();

        public void Show(in Planet planet)
        {
            _presenter.SetPlanet(planet);
            _view.Show();
        }
    }
}