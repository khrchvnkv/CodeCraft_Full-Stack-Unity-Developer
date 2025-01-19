using Game.Scripts.Common;
using UnityEngine;

namespace Game.Scripts.Gameplay.Components
{
    //Can be extended
    public sealed class Team : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }
    }
}