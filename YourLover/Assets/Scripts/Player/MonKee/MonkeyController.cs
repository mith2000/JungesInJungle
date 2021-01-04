using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyController : PlayerController
{
    [SerializeField] GameObject clone;

    public override void CacheReferences()
    {
        base.CacheReferences();
        Debug.Log("Monkey cached references");

        skillCooldownScript.cooldown = skillCooldown;
    }

    //public override void Moving()
    //{
    //    if (usingSkill)
    //        physicBody.MovePosition(physicBody.position + saveDirection * addSpeed * Time.fixedDeltaTime);
    //    else
    //        base.Moving();
    //}

    public void Skill_OnClick()
    {
        if (!skillCooldownScript.CanUseSkill() || 
            GameObject.FindGameObjectWithTag("MonkeyClone") != null) return;

        anim.SetTrigger("Skill");

        skillCooldownScript.isCooldown = true;

        MonkeeInstantiateClone();
    }

    void MonkeeInstantiateClone()
    {
        Vector3 clonePos = Vector3.zero;
        if (Random.Range(0, 100) < 50)
            clonePos.x = Random.Range(-2f, -1f);
        else
            clonePos.x = Random.Range(2f, 1f);
        if (Random.Range(0, 100) < 50)
            clonePos.y = Random.Range(-2f, -1f);
        else
            clonePos.y = Random.Range(2f, 1f);
        GameObject monkeyClone = Instantiate(clone, transform.position + clonePos, Quaternion.identity);
        monkeyClone.GetComponent<MonkeyClone>().parent = gameObject;
    }

}
