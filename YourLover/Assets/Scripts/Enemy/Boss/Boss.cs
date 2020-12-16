using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Enemy
{
    [SerializeField]
    private Transform enemyGFX;
    [SerializeField]
    private GameObject status;
    [SerializeField]
    private GameObject patrol;
    [HideInInspector]
    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Start()
    {
        Instantiate(status, transform.position, Quaternion.identity);

        healthBar = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<Slider>();
        healthBar.gameObject.SetActive(true);
        base.Start();
        FindPlayer();

        InvokeRepeating("FindTarget", 0f, 1f);

        Instantiate(patrol, transform.position, Quaternion.identity);
    }

    public override void Update()
    {
        if (DialogSystem.GetInstance().isInDialog) return;

        base.Update();
        
        if (target != null)
        {
            anim.SetBool("Walk", true);
            if (target.position.x > transform.position.x)
            {
                enemyGFX.rotation = new Quaternion(0, 180, 0, 1);
            }
            else
            {
                enemyGFX.rotation = new Quaternion(0, 0, 0, 1);
            }
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public override void DieAction()
    {
        healthBar.gameObject.SetActive(false);
        base.DieAction();
    }
}
