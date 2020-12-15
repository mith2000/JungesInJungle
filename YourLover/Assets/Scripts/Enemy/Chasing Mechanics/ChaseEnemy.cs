using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Homemade chasing mechanic
public class ChaseEnemy : MonoBehaviour
{
    [SerializeField]
    private Enemy brain;
    [SerializeField]
    private Transform enemyGFX;
    [SerializeField]
    private float chaseRange = 20f;
    [SerializeField]
    private float stopRange = 2f;
    [SerializeField]
    private float chaseSpeed = 6f;

    public virtual void Update()
    {
        if (brain.target != null)
            ChaseTarget();
    }

    public void ChaseTarget()
    {
        if (Vector2.Distance(transform.position, brain.target.position) < chaseRange &&
                   Vector2.Distance(transform.position, brain.target.position) > stopRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, brain.target.position, chaseSpeed * Time.deltaTime);
        }

        if (transform.position.x < brain.target.position.x)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
