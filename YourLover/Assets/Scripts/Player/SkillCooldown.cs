using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] Image cooldownImage;

    [HideInInspector] public float cooldown = 0;

    [HideInInspector] public bool isCooldown = false;
    
    void Update()
    {
        if (isCooldown)
        {
            cooldownImage.color = new Color32(0, 0, 0, 100);
            cooldownImage.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (cooldownImage.fillAmount == 0)
            {
                cooldownImage.color = new Color32(0, 0, 0, 0);
                cooldownImage.fillAmount = 1;
                isCooldown = false;
            }
        }
    }

    public bool CanUseSkill()
    {
        return cooldownImage.fillAmount == 1;
    }
}
