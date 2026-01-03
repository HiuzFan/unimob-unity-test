using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Utils
{
    public static bool OverlapFlag(this Enum first, Enum second)
    {
        return (Convert.ToInt32(first) & Convert.ToInt32(second)) != 0;
    }

    public static bool OverlapFlag(this LayerMask mask, int layer)
    {
        return (mask & (1 << layer)) != 0;
    }

    public static Vector3 Abs(Vector3 value)
    {
        return new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
    }

    public static Vector3 Floor(Vector3 value)
    {
        return new Vector3(Mathf.Floor(value.x), Mathf.Floor(value.y), Mathf.Floor(value.z));
    }

    public static Vector3 Round(Vector3 value)
    {
        return new Vector3(Mathf.Round(value.x), Mathf.Round(value.y), Mathf.Round(value.z));
    }

    public static bool LessThan(Vector3 l, Vector3 r)
    {
        return l.x < r.x && l.y < r.y && l.z < r.z;
    }

    public static int GetRandomIndex(int[] weights)
    {
        if (weights.Length == 0) return -1;

        int sum = weights.Sum();
        int value = UnityEngine.Random.Range(0, sum);

        int currentSum = 0;
        for (int index = 0; index < weights.Length; ++index)
        {
            currentSum += weights[index];
            if (value < currentSum) return index;
        }
        return -1;
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool Overlap<T>(this T self, T flag) where T : Enum
    {
        return (Convert.ToUInt32(self) & Convert.ToUInt32(flag)) != 0;
    }

    public static bool Overlap(this LayerMask self, int layer)
    {
        return (self & (1 << layer)) != 0;
    }

    public static void ApplyMod(ref float current, Modifier mod, float baseValue)
    {
        switch (mod.type)
        {
            case ModifierType.Flat:
                current += mod.value;
                break;
            case ModifierType.Percentage:
                current += baseValue * mod.value;
                break;
            case ModifierType.Override:
                current = mod.value;
                break;
            default:
                Debug.LogError("Invalid!");
                break;
        }
    }


    public static bool IsPointerOverUIObject(Vector2 position)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = position;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public static int RandomWeights(float[] weights)
    {
        var total = weights.Sum();
        float value = UnityEngine.Random.Range(0, total);
        float current = 0.0f;
        for (int i = 0; i < weights.Length; ++i)
        {
            current += weights[i];
            if (value < current) return i;
        }
        return -1;
    }

    public static float GetRealCost(float baseValue, float multiPerLevel, float currentLevel)
    {
        return Mathf.Floor(baseValue * Mathf.Pow(multiPerLevel, currentLevel));
    }

    public static void SetParentNextFrame(this Transform tran, Transform parent)
    {
        GameManager.Instance.StartCoroutine(tran.SetParentNextFrameRoutine(parent));
    }

    static IEnumerator SetParentNextFrameRoutine(this Transform tran, Transform parent)
    {
        yield return null;
        tran.SetParent(parent);
    }

    public static string FormatNumber(long num)
    {
        if (num >= 100000000)
        {
            return (num / 1000000D).ToString("0.#M");
        }
        if (num >= 1000000)
        {
            return (num / 1000000D).ToString("0.##M");
        }
        if (num >= 100000)
        {
            return (num / 1000D).ToString("0.#k");
        }
        if (num >= 10000)
        {
            return (num / 1000D).ToString("0.##k");
        }

        return num.ToString("#,0");
    }
}