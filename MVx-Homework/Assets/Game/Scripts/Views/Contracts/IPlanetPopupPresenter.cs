using System;
using UnityEngine;

namespace Game.Views.Contracts
{
    public interface IPlanetPopupPresenter
    {
        event Action ButtonInteractableUpdated;
        event Action PlanetUpgraded;
        event Action PlanetUnlocked;
        event Action PlanetPopulationChanged;
        event Action PlanetIncomeChanged;
        event Action PopupShowed;
        
        void Hide();
        
        void UpgradePlanet();

        Sprite GetPlanetIcon(); 
        
        string GetTitleText();

        string GetPopulationText();

        string GetLevelText();

        string GetIncomeText();

        bool IsMaxLevelUpgrade();

        string GetPriceText();

        bool IsUpgradeButtonInteractable();
    }
}