using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabContainer : MonoBehaviour
{
    private static PrefabContainer instance;

    public GameObject monkeyPrefab;
    public GameObject catPrefab;
    public GameObject damageInfoPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static PrefabContainer GetInstance()
    {
        return instance;
    }
}
