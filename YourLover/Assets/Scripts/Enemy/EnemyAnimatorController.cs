using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private Enemy attackReceiver;
    [SerializeField]
    private bool canMove = true;

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
