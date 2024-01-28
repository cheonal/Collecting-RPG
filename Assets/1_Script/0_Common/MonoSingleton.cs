using System;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this.GetComponent<T>();
    }
}