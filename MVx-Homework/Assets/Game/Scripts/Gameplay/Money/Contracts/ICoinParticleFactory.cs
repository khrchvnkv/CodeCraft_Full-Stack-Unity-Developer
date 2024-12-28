using System;
using UnityEngine;

namespace Game.Gameplay.Contracts
{
    public interface ICoinParticleFactory
    {
        public void SpawnEffect(in Vector2 from, in Vector2 to, Action callback = null);
    }
}