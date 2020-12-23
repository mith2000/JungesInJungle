using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(LineRenderer))]
public class AimLine : MonoBehaviour
{
    [Header("Laser Settings")]
    [SerializeField] protected float lineLenght = 10;
    protected LineRenderer line;
    [SerializeField] protected LayerMask layerMask;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        if (Physics2D.Raycast(transform.position, transform.up, lineLenght, layerMask))
        {
            RaycastHitted();
        }
        else
        {
            RaycastNotHitted();
        }
    }

    public virtual void RaycastHitted()
    {
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.up, lineLenght, layerMask);
        line.SetPosition(1, new Vector3(0f, Vector3.Distance(transform.position, _hit.point), 0f));
    }

    public virtual void RaycastNotHitted()
    {
        line.SetPosition(1, new Vector3(0f, lineLenght, 0f));
    }
}
