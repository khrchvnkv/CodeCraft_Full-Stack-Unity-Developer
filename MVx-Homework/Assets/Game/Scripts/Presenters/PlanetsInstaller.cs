using System;
using System.Collections.Generic;
using Game.Gameplay;
using Game.Views;
using Modules.Planets;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetsInstaller : MonoInstaller<PlanetsInstaller>
    {
        [SerializeField] private PlanetData[] _planetDatas;

        [SerializeField] private PlanetCatalog _catalog;
        
        [Serializable]
        private class PlanetData
        {
            [field: SerializeField, ValueDropdown(nameof(GetAllPlanetsIDs))] public string Id { get; private set; }
            [field: SerializeField] public PlanetView View { get; private set; }
            
            private PlanetCatalog _catalog;

            public void SetCatalog(in PlanetCatalog planetCatalog) => _catalog = planetCatalog;
            
            private IEnumerable<string> GetAllPlanetsIDs()
            {
                if (_catalog != null)
                {
                    foreach (var planet in _catalog)
                    {
                        yield return planet.Name;
                    }
                }
            }
        }

        private void OnValidate()
        {
            foreach (var data in _planetDatas)
            {
                data.SetCatalog(_catalog);
            }
        }

        public override void InstallBindings()
        {
            foreach (var planetData in _planetDatas)
            {
                Container
                    .BindInterfacesAndSelfTo<PlanetPresenter>()
                    .AsCached()
                    .WithArguments(planetData.View, planetData.Id)
                    .NonLazy();
            }
            
            PlanetInstaller.Install(Container, _catalog);
        }
    }
}