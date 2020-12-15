using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChasingPathFinding : MonoBehaviour
{
    [SerializeField]
    private Enemy brain;
    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    private float nextWaypointDistance = 3f;

    [SerializeField]
    private Transform enemyGFX;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D body;

    // Start is called before the first frame update
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

    // Update is called once per frame
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
