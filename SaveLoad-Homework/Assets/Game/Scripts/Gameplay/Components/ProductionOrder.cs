using System.Collections.Generic;
using Modules.Entities;
using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class ProductionOrder : MonoBehaviour
    {
        ///Variable
        [SerializeField]
        private List<EntityConfig> _queue;
        
        public IReadOnlyList<EntityConfig> Queue
        {
            get { return _queue; }
            set { _queue = new List<EntityConfig>(value); }
        }
    }
}