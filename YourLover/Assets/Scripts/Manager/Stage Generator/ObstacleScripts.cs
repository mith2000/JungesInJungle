using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScripts : MonoBehaviour
{
    [Header ("Normal rooms")]
    public GameObject[] forestObstacleGroups;
    public GameObject[] sandObstacleGroups;
    public GameObject[] urbanObstacleGroups;

    [Header("Boss rooms")]
    public GameObject forestBossOG;
    public GameObject sandBossOG;
    public GameObject urbanBossOG;
}
