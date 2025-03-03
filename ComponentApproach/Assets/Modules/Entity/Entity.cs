using UnityEngine;
using Zenject;

namespace Modules.Entity
{
    public sealed class Entity : MonoBehaviour, IEntity
    {
        [SerializeField] private GameObjectContext _context;

        public T Get<T>()
        {
            return _context.Container.Resolve<T>();
        }

        public bool TryGet<T>(out T component) where T : class
        {
            component = _context.Container.TryResolve<T>();
            return component != null;
        }
    }
}