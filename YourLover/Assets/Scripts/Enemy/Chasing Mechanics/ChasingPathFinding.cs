using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//Link: https://www.youtube.com/watch?v=jvtFUfJ6CP8&ab_channel=Brackeys
public class ChasingPathFinding : MonoBehaviour
{
    [SerializeField] Enemy brain;
    [SerializeField] float speed = 300f;
    [SerializeField] float nextWaypointDistance = 3f;

    [SerializeField] Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D body;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        body = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            if (brain.target != null)
                seeker.StartPath(body.position, brain.target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null) return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - body.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        body.AddForce(force);

        float distance = Vector2.Distance(body.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (body.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (body.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
