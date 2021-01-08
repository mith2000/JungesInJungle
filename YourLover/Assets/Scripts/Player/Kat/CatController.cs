using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CatController : PlayerController
{
    VolumeProfile profile;
    ChromaticAberration chromaticAberration;
    Vignette vignette;

    [SerializeField] float skillDuration = 1f;

    public override void CacheReferences()
    {
        base.CacheReferences();
        Debug.Log("Cat cached references");
        profile = GameObject.Find("Post Processing").GetComponent<UnityEngine.Rendering.Volume>().profile;
        profile.TryGet(out chromaticAberration);
        profile.TryGet(out vignette);

        skillCooldownScript.cooldown = skillCooldown;
    }

    public void Skill_OnClick()
    {
        if (!skillCooldownScript.CanUseSkill()) return;

        int rand = Random.Range(0, 100);
        if (rand <= 50)
        {
            AudioManager.GetInstance().Play("KatSkill1");
        }
        else
        {
            AudioManager.GetInstance().Play("KatSkill2");
        }

        anim.SetTrigger("Skill");

        skillCooldownScript.isCooldown = true;

        TimeManager.GetInstance().DoSlowmo(skillDuration);
        StopEnemyBullet();

        //make player not slowdown like timescale
        addSpeed = 1 / TimeManager.GetInstance().Factor;
        chromaticAberration.active = true;
        vignette.active = true;
    }

    private void StopEnemyBullet()
    {
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (GameObject bullet in enemyBullets)
        {
            float saveSpeed = bullet.GetComponent<EnemyProjectile>().speed;
            bullet.GetComponent<EnemyProjectile>().speed = 0;
            StartCoroutine(ReflectBullet(bullet.GetComponent<EnemyProjectile>(), -saveSpeed));
        }
    }

    IEnumerator ReflectBullet(EnemyProjectile bullet, float reverseSpeed)
    {
        yield return new WaitForSeconds(skillDuration);
        bullet.isReverse = true;
        bullet.speed = reverseSpeed;
    }

    public void EndSkill()
    {
        chromaticAberration.active = false;
        vignette.active = false;
        addSpeed = 1f;
    }

}
