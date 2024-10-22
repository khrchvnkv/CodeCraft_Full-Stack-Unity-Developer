using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class Pool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _pool = new();

        private readonly HashSet<T> _activeInstances = new();

        public IEnumerable<T> ActiveInstances => _activeInstances;

        public Pool(in T prefab, in Transform container)
        {
            _prefab = prefab;
            _container = container;
        }

        public void Prepare(in int size)
        {
            _container.gameObject.SetActive(false);
            for (var i = 0; i < size; i++)
            {
                var instance = Object.Instantiate(_prefab, _container);
                _pool.Enqueue(instance);
            }
        }

        public T Spawn(in Transform parent)
        {
            if (_pool.TryDequeue(out var instance))
            {
                instance.transform.SetParent(parent);
            }
            else
            {
                instance = Object.Instantiate(_prefab, parent);
            }
            _activeInstances.Add(instance);
            
            return instance;
        }

        public void Despawn(in T instance)
        {
            _activeInstances.Remove(instance);
            instance.transform.SetParent(_container);
            _pool.Enqueue(instance);
        }
    }
}