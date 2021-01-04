using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabContainer : MonoBehaviour
{
    private static PrefabContainer instance;

    [Header ("Character Prefabs")]
    public GameObject monkeyPrefab;
    public GameObject catPrefab;

    [Header ("Canvas Prefab")]
    public GameObject damageInfoPrefab;
    public GameObject deadCanvasPrefab;

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
