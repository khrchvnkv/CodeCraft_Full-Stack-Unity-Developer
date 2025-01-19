using System;

namespace Game.Scripts.App.SaveLoad.Storage.Dto
{
    [Serializable]
    public struct ProductionOrderData
    {
        public string[] Queue { get; set; }
    }
}