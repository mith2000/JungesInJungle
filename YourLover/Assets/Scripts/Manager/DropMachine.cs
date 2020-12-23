using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMachine : MonoBehaviour
{
    [SerializeField] int dropRate = 50;
    [SerializeField] GameObject[] items;

    public void Drop(Vector3 position)
    {
        if (Random.Range(0, 100) < dropRate) //50% drop rate
        {
            GameObject randomItem = items[Random.Range(0, items.Length)];
            Instantiate(randomItem, position, transform.rotation);
        }
    }
}
