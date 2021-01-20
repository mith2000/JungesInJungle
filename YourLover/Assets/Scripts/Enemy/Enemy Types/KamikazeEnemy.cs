using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [Header ("Kamikaze Settings")]
    [SerializeField] GameObject bomb;
    [SerializeField] Transform bombPosition;

    public override void Start()
    {
        base.Start();
        FindPlayer();
        GameObject danger = Instantiate(bomb, bombPosition.position, Quaternion.identity);
        danger.GetComponent<Bomb>().isTaken = true;
        danger.GetComponent<Bomb>().ownerTransform = bombPosition;

        AudioManager.GetInstance().Play("BombTick");

        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public override void Update()
    {
        base.Update();
    }
}
