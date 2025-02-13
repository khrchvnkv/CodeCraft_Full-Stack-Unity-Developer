using Game.Scripts.Components;
using UnityEngine;

namespace Game.Scripts.Objects
{
    public class Springboard : MonoBehaviour,
        ThrowUpComponent.ICondition
    {
        bool ThrowUpComponent.ICondition.Invoke() => true;
    }
}