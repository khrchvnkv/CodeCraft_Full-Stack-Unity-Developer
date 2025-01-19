using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class Countdown : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public float Current { get; set; }

        ///Const
        [field: SerializeField]
        public float Duration { get; private set; }
    }
}