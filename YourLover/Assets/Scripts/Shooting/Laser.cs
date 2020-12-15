using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float defDistanceRay = 100;
    public LayerMask layerMask;
    public Transform laserPoint;
    public LineRenderer line;
    Transform m_transform;
    public int damage = 5;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ShootLaser();
    }

    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(laserPoint.position, transform.up, defDistanceRay, layerMask);
            Draw2DRay(laserPoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserPoint.position, laserPoint.transform.right * defDistanceRay);
        }
    }

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                collision.GetComponent<PlayerInfo>().TakeDamage(damage);
                collision.GetComponent<PlayerInfo>().isGotSeriousInjury = true;
                collision.GetComponent<PlayerInfo>().RecoveryFromOther();
            }
        }
    }
}
