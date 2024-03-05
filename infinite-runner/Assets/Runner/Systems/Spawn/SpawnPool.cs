using System.Collections.Generic;
using UnityEngine;

namespace Runner.Systems.Spawn
{
    /// <summary>
    ///     Auto release pool implementation. Expands dynamically
    /// </summary>
    public sealed class SpawnPool : MonoBehaviour
    {
        private const float AutoReleaseDelay = 5f;
        [SerializeField] private Transform _root;
        private readonly Dictionary<string, Stack<SpawnPoolElement>> _instances = new();

        public static void Collect(Transform location)
        {
            for (var i = 0; i < location.childCount; i++)
            {
                var child = location.GetChild(i);
                if (child.TryGetComponent(out SpawnPoolElement element))
                {
                    element.Release();
                }
            }
        }

        private static void Prepare(GameObject instance, Transform parent)
        {
            instance.SetActive(false);
            instance.transform.parent = parent;
            instance.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        }

        public GameObject Pop(string key, GameObject template)
        {
            if (_instances.TryGetValue(key, out var result) && result.Count > 0)
            {
                return Activate(key, result.Pop());
            }
            return Create(key, Instantiate(template, null));
        }

        public void Release(string key, SpawnPoolElement instance)
        {
            Prepare(instance.gameObject, _root);
            Push(key, instance);
        }

        private GameObject Activate(string key, SpawnPoolElement instance)
        {
            instance.Initialize(key, this, AutoReleaseDelay);
            return instance.gameObject;
        }

        private GameObject Create(string key, GameObject instance)
        {
            Prepare(instance, null);
            return Activate(key, instance.AddComponent<SpawnPoolElement>());
        }

        private void Push(string key, SpawnPoolElement instance)
        {
            if (!_instances.TryGetValue(key, out var result))
            {
                _instances[key] = result = new Stack<SpawnPoolElement>();
            }
            result.Push(instance);
        }
    }
}