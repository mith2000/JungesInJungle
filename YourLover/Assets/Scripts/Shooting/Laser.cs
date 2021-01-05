using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : AimLine
{
    public int damage = 5;

    public override void RaycastHitted()
    {
        base.RaycastHitted();
        RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.up, lineLenght, layerMask);

        if (_hit.collider.CompareTag("Player"))
        {
            if (!_hit.collider.GetComponent<PlayerInfo>().isGotSeriousInjury)
            {
                _hit.collider.GetComponent<PlayerInfo>().TakeDamage(damage);
                _hit.collider.GetComponent<PlayerInfo>().isGotSeriousInjury = true;
                _hit.collider.GetComponent<PlayerInfo>().RecoveryFromOther();
            }
        }
        if (_hit.collider.CompareTag("MonkeyClone"))
        {
            _hit.collider.GetComponent<Entity>().TakeDamage(damage);
        }
    }
}
