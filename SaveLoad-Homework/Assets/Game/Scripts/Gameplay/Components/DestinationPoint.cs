using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }
    }
}