using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class MoveSpeed : MonoBehaviour
    {
        ///Const
        [field: SerializeField]
        public float Current { get; private set; }
    }
}