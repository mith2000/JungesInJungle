using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRoom : MonoBehaviour
{
    //Finding room that setted in Boss Scripts
    [HideInInspector] public GameObject[] specialRooms;
    [HideInInspector] public GameObject[] edgeRooms;

    [SerializeField] bool isBossStage = false;

    [Header ("Spawn Reference")]
    [SerializeField] GameObject portal;
    [SerializeField] GameObject[] chestRooms;
    [SerializeField] GameObject[] interactRooms;
    [SerializeField] GameObject chestRoomMinimapIcon;
    [SerializeField] GameObject interactRoomMinimapIcon;

    private void Awake()
    {
        specialRooms = GameObject.FindGameObjectsWithTag("SpecialRoom");
        edgeRooms = GameObject.FindGameObjectsWithTag("EdgeRoom");
    }

    void Start()
    {
        if (!isBossStage)
            InstantiateStageFeatures();
        else
            InstantiateBossStageFeatures();
    }

    void InstantiateStageFeatures()
    {
        if (edgeRooms.Length == 1)
        {
            Instantiate(portal, edgeRooms[0].transform.position, Quaternion.identity);
            
            if (specialRooms.Length == 1)
            {
                ChestInstantiate(specialRooms[0].transform.position);
            }
            else if (specialRooms.Length == 2)
            {
                if (Random.Range(0, 100) < 50)
                {

                    InteractInstantiate(specialRooms[0].transform.position);
                    ChestInstantiate(specialRooms[1].transform.position);
                }
                else
                {
                    InteractInstantiate(specialRooms[1].transform.position);
                    ChestInstantiate(specialRooms[0].transform.position);
                }
            }
        }
        else if (edgeRooms.Length == 2)
        {
            if (Random.Range(0, 100) < 50)
            {
                Instantiate(portal, edgeRooms[0].transform.position, Quaternion.identity);
                ChestInstantiate(edgeRooms[1].transform.position);
            }
            else
            {
                Instantiate(portal, edgeRooms[1].transform.position, Quaternion.identity);
                ChestInstantiate(edgeRooms[0].transform.position);
            }

            if (specialRooms.Length == 1)
            {
                InteractInstantiate(specialRooms[0].transform.position);
            }
        }
        else if (edgeRooms.Length == 3)
        {
            int portRand = Random.Range(0, 100);
            if (portRand < 33)
            {
                Instantiate(portal, edgeRooms[0].transform.position, Quaternion.identity);
                if (Random.Range(0, 100) < 50)
                {
                    InteractInstantiate(edgeRooms[1].transform.position);
                    ChestInstantiate(edgeRooms[2].transform.position);
                }
                else
                {
                    InteractInstantiate(edgeRooms[2].transform.position);
                    ChestInstantiate(edgeRooms[1].transform.position);
                }
            }
            else if (portRand > 66)
            {
                Instantiate(portal, edgeRooms[1].transform.position, Quaternion.identity);
                if (Random.Range(0, 100) < 50)
                {
                    InteractInstantiate(edgeRooms[0].transform.position);
                    ChestInstantiate(edgeRooms[2].transform.position);
                }
                else
                {
                    InteractInstantiate(edgeRooms[2].transform.position);
                    ChestInstantiate(edgeRooms[0].transform.position);
                }
            }
            else
            {
                Instantiate(portal, edgeRooms[2].transform.position, Quaternion.identity);
                if (Random.Range(0, 100) < 50)
                {
                    InteractInstantiate(edgeRooms[1].transform.position);
                    ChestInstantiate(edgeRooms[0].transform.position);
                }
                else
                {
                    InteractInstantiate(edgeRooms[0].transform.position);
                    ChestInstantiate(edgeRooms[1].transform.position);
                }
            }
        }
    }

    void InstantiateBossStageFeatures()
    {
        if (edgeRooms.Length == 1 && specialRooms.Length == 1)
        {
            InteractInstantiate(specialRooms[0].transform.position);
            ChestInstantiate(edgeRooms[0].transform.position);
        }
        else if (edgeRooms.Length == 2)
        {
            InteractInstantiate(edgeRooms[0].transform.position);
            ChestInstantiate(edgeRooms[1].transform.position);
        }
        else if (specialRooms.Length == 2)
        {
            InteractInstantiate(specialRooms[0].transform.position);
            ChestInstantiate(specialRooms[1].transform.position);
        }
    }

    public void ChestInstantiate(Vector3 position)
    {
        int rand = Random.Range(0, chestRooms.Length);
        Instantiate(chestRooms[rand], position, Quaternion.identity);
        Instantiate(chestRoomMinimapIcon, position, Quaternion.identity);
    }

    public void InteractInstantiate(Vector3 position)
    {
        int rand = Random.Range(0, interactRooms.Length);
        Instantiate(interactRooms[rand], position, Quaternion.identity);
        Instantiate(interactRoomMinimapIcon, position, Quaternion.identity);
    }
}
