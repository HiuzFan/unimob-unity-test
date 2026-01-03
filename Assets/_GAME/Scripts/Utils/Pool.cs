using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pool
{
    static Dictionary<string, List<MonoBehaviour>> defaultPools = new Dictionary<string, List<MonoBehaviour>>();

    public static T Use<T>(this T prefab, List<T> pool) where T : MonoBehaviour
    {
        return Use(prefab, Vector3.zero, Quaternion.identity, null, pool);
    }

    public static T Use<T>(this T prefab, Transform parent, List<T> pool) where T : MonoBehaviour
    {
        return Use(prefab, Vector3.zero, Quaternion.identity, parent, pool);
    }

    public static T Use<T>(this T prefab, Vector3 position, Quaternion rotation, Transform parent, List<T> pool) where T : MonoBehaviour
    {
        foreach (var instance in pool)
        {
            if (!instance.gameObject.activeSelf)
            {
                instance.gameObject.SetActive(true);
                instance.transform.position = position;
                instance.transform.rotation = rotation;
                instance.transform.SetParent(parent);
                return instance;
            }
        }
        var newInstance = GameObject.Instantiate(prefab, position, rotation, parent);
        pool.Add(newInstance);
        return newInstance;
    }

    public static T Use<T>(this T prefab) where T : MonoBehaviour
    {
        if (!defaultPools.ContainsKey(prefab.name)) defaultPools.Add(prefab.name, new List<MonoBehaviour>());
        return (T)prefab.Use(defaultPools[prefab.name]);
    }

    public static T Use<T>(this T prefab, Transform parent) where T : MonoBehaviour
    {
        if (!defaultPools.ContainsKey(prefab.name)) defaultPools.Add(prefab.name, new List<MonoBehaviour>());
        return (T)prefab.Use(parent, defaultPools[prefab.name]);
    }

    public static T Use<T>(this T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : MonoBehaviour
    {
        if (!defaultPools.ContainsKey(prefab.name)) defaultPools.Add(prefab.name, new List<MonoBehaviour>());
        return (T)prefab.Use(position, rotation, parent, defaultPools[prefab.name]);
    }

    public static void Recycle<T>(this T instance) where T : MonoBehaviour
    {
        instance.gameObject.SetActive(false);
    }

    public static void Recycle<T>(this T instance, float delay) where T : MonoBehaviour
    {
        instance.StartCoroutine(instance.RecycleRoutine(delay));
    }

    public static void DeleteAll()
    {
        foreach (var pool in defaultPools)
        {
            foreach (var go in pool.Value)
            {
                if (go) GameObject.Destroy(go.gameObject);
            }
        }
        defaultPools.Clear();
    }

    public static void RecycleAll()
    {
        foreach (var pool in defaultPools)
        {
            foreach (var go in pool.Value)
            {
                go.Recycle();
            }
        }
    }

    public static void RecycleAll<T>(List<T> pool) where T : MonoBehaviour
    {
        foreach (var go in pool)
        {
            go.Recycle();
        }
    }

    static IEnumerator RecycleRoutine<T>(this T instance, float delay) where T : MonoBehaviour
    {
        yield return new WaitForSeconds(delay);
        instance.Recycle();
    }
}
