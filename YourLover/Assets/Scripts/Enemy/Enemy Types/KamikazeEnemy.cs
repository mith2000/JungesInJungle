using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : Enemy
{
    [Header ("Kamikaze Settings")]
    [SerializeField] GameObject bomb;

    public override void Start()
    {
        base.Start();
        FindPlayer();
        GameObject danger = Instantiate(bomb, transform.position, Quaternion.identity);
        danger.GetComponent<Bomb>().isTaken = true;
        danger.GetComponent<Bomb>().ownerTransform = transform;

        AudioManager.GetInstance().Play("BombTick");

        InvokeRepeating("FindTarget", 0f, 1f);
    }

    public override void Update()
    {
        base.Update();
    }
}
