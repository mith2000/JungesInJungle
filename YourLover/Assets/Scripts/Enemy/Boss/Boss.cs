using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Boss : Enemy
{
    [Header ("Boss Settings")]
    [SerializeField] Transform enemyGFX;
    //Boss Status: Healthbar, ...
    [SerializeField] GameObject status;
    //Instantiate patrol points for animation Walking
    [SerializeField] GameObject patrol;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Start()
    {
        Instantiate(status, transform.position, Quaternion.identity);

        //Boss's health bar inside Boss Status
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
        
        //if found target, start walking potrol points
        if (target != null)
        {
            anim.SetBool("Walk", true);
            if (target.position.x > transform.position.x)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
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
