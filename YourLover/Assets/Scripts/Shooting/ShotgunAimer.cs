﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAimer : Aimer
{
    [Header ("Shotgun Settings")]
    [SerializeField] int shotgunBulletCount;
    [SerializeField] float spread = 5;

    public override void Shoot()
    {
        Quaternion newRot = transform.rotation;
        for (int i = 0; i < shotgunBulletCount; i++)
        {
            float addedOffset = (i - (shotgunBulletCount / 2)) * spread;
            newRot = Quaternion.Euler(transform.eulerAngles.x, 
                transform.eulerAngles.y, 
                transform.eulerAngles.z + addedOffset);
            Instantiate(projectile, shotPoint.position, newRot);
        }
    }
}
