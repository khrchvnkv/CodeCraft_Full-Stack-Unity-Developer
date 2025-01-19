using Modules.Entities;
using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }
    }
}