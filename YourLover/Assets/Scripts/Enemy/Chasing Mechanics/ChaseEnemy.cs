using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Homemade chasing mechanic
public class ChaseEnemy : MonoBehaviour
{
    [SerializeField] Enemy brain;
    [SerializeField] Transform enemyGFX;

    [Header ("Chase Settings")]
    [SerializeField] float chaseRange = 20f;
    [SerializeField] float stopRange = 2f;
    [SerializeField] float chaseSpeed = 6f;

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
