using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatorController : MonoBehaviour
{
    Animator anim;
    [SerializeField] Enemy attackReceiver;
    [SerializeField] bool canMove = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            if (GetComponent<Rigidbody2D>().velocity != Vector2.zero)
            {
                anim.SetBool("Walk", true);
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }

        if (attackReceiver.triggerAttack)
        {
            anim.SetTrigger("Attack");
            attackReceiver.triggerAttack = false;
        }
    }
}
