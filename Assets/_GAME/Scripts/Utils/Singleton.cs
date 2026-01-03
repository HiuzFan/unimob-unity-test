
using System;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static T Instance
    {
        get => _instance;
    }
    protected virtual void Awake()
    {
        if (!Application.isPlaying) return;
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (this != _instance)
        {
            Debug.LogError("Duplicate singleton error!");
        }
    }
}
