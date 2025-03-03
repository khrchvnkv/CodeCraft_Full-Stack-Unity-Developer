using UnityEngine;

namespace Game.Scripts.GameObjects.Core
{
    public class DestroyableComponent
    {
        private readonly GameObject _deactivatingGameObject;

        public bool IsDestroyable { get; private set; }
        
        public DestroyableComponent(GameObject deactivatingGameObject)
        {
            _deactivatingGameObject = deactivatingGameObject;
        }

        public void Destroy()
        {
            _deactivatingGameObject.SetActive(false);
            IsDestroyable = true;
        }
    }
}