using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : Entity
{
    [SerializeField]
    private Text healthText;

    private int maxEnergy;
    private int currentEnergy;
    private int energyPerBar = 30;
    [SerializeField]
    private Slider[] energyBar;
    private int energyPerRegen = 1;
    public float delayRegenEnergy = 0.1f;
    private float energyRegenTime = 0;

    public int maxArmor = 5;
    private int currentArmor;
    [SerializeField]
    private Slider armorBar;
    [SerializeField]
    private Text armorText;
    private int armorPerRegen = 1;
    private float delayRegenArmor = 2;
    private float armorRegenTime = 0;
    private float delayStartRegenArmor = 5;
    private float armorRegenStartTime = 0;

    public int currentCoin = 0;
    [SerializeField]
    private Text coinText;

    [HideInInspector]
    public bool isGotSeriousInjury = false;
    private float woundRecoveryTime = 0.5f;

    //Test github line
    public override void Start()
    {
        base.Start();
        //If player is instanciated through scenes while injured, get injured
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            maxHealth = GameMaster.GetInstance().playerStat.maxHealth;
            currentHealth = GameMaster.GetInstance().playerStat.currentHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth - currentHealth;
            maxArmor = GameMaster.GetInstance().playerStat.maxArmor;
            delayRegenEnergy = GameMaster.GetInstance().playerStat.delayRegenEnergy;
            currentCoin = GameMaster.GetInstance().playerStat.currentCoin;
        }

        maxEnergy = energyPerBar * energyBar.Length;
        currentEnergy = maxEnergy;
        for (int i = 0; i < energyBar.Length; i++)
        {
            energyBar[i].maxValue = energyPerBar * (i + 1);
            energyBar[i].minValue = energyPerBar * i;
            energyBar[i].value = energyPerBar * i;
        }

        currentArmor = maxArmor;
        armorBar.maxValue = maxArmor;
        armorBar.value = 0;
    }

    public override void Update()
    {
        base.Update();
        RegenEnergy();
        DisplayEnergyByBar();
        RegenArmor();
        DisplayPlayerStatusByText();
    }

    private void DisplayPlayerStatusByText()
    {
        healthText.text = currentHealth + "/" + maxHealth;
        armorText.text = currentArmor + "/" + maxArmor;
        coinText.text = currentCoin.ToString();
    }

    private void RegenEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            if (Time.time >= energyRegenTime)
            {
                currentEnergy = Mathf.Min(currentEnergy + energyPerRegen, maxEnergy);
                energyRegenTime = Time.time + delayRegenEnergy;
            }
        }
    }

    //Setting energy bars' value like Brawl Star's style
    private void DisplayEnergyByBar()
    {
        for (int i = 0; i < energyBar.Length; i++)
        {
            int divisor = energyPerBar;
            int quotient = currentEnergy / divisor;
            int surplus = currentEnergy % divisor;
            if (i < quotient)
            {
                energyBar[i].value = divisor * i;
            }
            else if (i == quotient)
            {
                energyBar[i].value = divisor * (i + 1) - surplus;
            }
            else if (i > quotient)
            {
                energyBar[i].value = energyBar[i].maxValue;
            }
        }
    }

    public void ResetDelayStartRegenArmor()
    {
        armorRegenStartTime = Time.time + delayStartRegenArmor;
    }

    private void RegenArmor()
    {
        if (currentArmor < maxArmor)
        {
            //check if player is in non-combat state
            if (Time.time >= armorRegenStartTime)
            {
                if (Time.time >= armorRegenTime)
                {
                    currentArmor = Mathf.Min(currentArmor + armorPerRegen, maxArmor);
                    armorBar.value = maxArmor - currentArmor;
                    armorRegenTime = Time.time + delayRegenArmor;
                }
            }
        }
    }

    //Mac dinh la tru armor va health
    public override void TakeDamage(int damageAmount)
    {
        ResetDelayStartRegenArmor();
        if (currentArmor >= damageAmount)
        {
            currentArmor -= damageAmount;
            armorBar.value = maxArmor - currentArmor;
        }
        else
        {
            //Phan damage bi du ra sau khi bi tru giap
            int damageAmountToHealth = damageAmount - currentArmor;

            //Tru het giap
            currentArmor = 0;
            armorBar.value = maxArmor;

            ApplyHealth(damageAmountToHealth);
        }
    }

    public void ApplyHealth(int healthAmount)
    {
        currentHealth = Mathf.Min(currentHealth - healthAmount, maxHealth);
        healthBar.value = maxHealth - currentHealth;
        if (currentHealth <= 0)
        {
            //Die action
            //Destroy(gameObject);
        }
    }

    public void IncreaseHealth(int increaseAmount)
    {
        maxHealth += increaseAmount;
        currentHealth += increaseAmount;
        healthBar.maxValue = maxHealth;
    }

    public void IncreaseArmor(int increaseAmount)
    {
        maxArmor += increaseAmount;
        currentArmor += increaseAmount;
        armorBar.maxValue = maxArmor;
    }

    public void IncreaseAttackSpeed(float percent)
    {
        delayRegenEnergy *= percent;
    }

    public bool CanApplyEnergy(int energyAmount)
    {
        return currentEnergy >= energyAmount;
    }

    public void ApplyEnergy(int energyAmount)
    {
        currentEnergy = Mathf.Min(currentEnergy - energyAmount, maxEnergy);
    }

    public void ApplyCoin(int coinAmount)
    {
        currentCoin += coinAmount;
    }

    public void RecoveryFromOther()
    {
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(woundRecoveryTime);

        isGotSeriousInjury = false;
    }
}
