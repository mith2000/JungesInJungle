using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : Entity
{
    [SerializeField] Text healthText;

    [Header ("Energy")]
    int maxEnergy;
    int currentEnergy;
    int energyPerBar = 30;
    [SerializeField] Slider[] energyBar;
    int energyPerRegen = 1;
    public float delayRegenEnergy = 0.1f;
    float energyRegenTime = 0;

    [Header ("Armor")]
    public int maxArmor = 5;
    int currentArmor;
    [SerializeField] Slider armorBar;
    [SerializeField] Text armorText;
    int armorPerRegen = 1;
    float delayRegenArmor = 2;
    float armorRegenTime = 0;
    float delayStartRegenArmor = 5;
    float armorRegenStartTime = 0;

    [Header ("Coin")]
    public int currentCoin = 0;
    [SerializeField] TextMeshProUGUI coinText;

    PlayerController controller;
    Collider2D collider;

    [HideInInspector]public bool isGotSeriousInjury = false;
    float woundRecoveryTime = 0.5f;

    //for Game Master saver
    [HideInInspector] public bool firstTimeHealthPotion = true;
    [HideInInspector] public bool firstTimeHealthCrystal = true;
    [HideInInspector] public bool firstTimeArmorCrystal = true;
    [HideInInspector] public bool firstTimeAttackSpeedCrystal = true;
    [HideInInspector] public bool firstTimeRainbowCrystal = true;

    public override void Start()
    {
        base.Start();
        //Get saved data from GM
        if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            maxHealth = GameMaster.GetInstance().playerStat.maxHealth;
            //If player is instantiated through scenes while injured, get injured
            currentHealth = GameMaster.GetInstance().playerStat.currentHealth;
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth - currentHealth;
            maxArmor = GameMaster.GetInstance().playerStat.maxArmor;
            delayRegenEnergy = GameMaster.GetInstance().playerStat.delayRegenEnergy;

            firstTimeHealthPotion = GameMaster.GetInstance().playerStat.firstTimeHealthPotion;
            firstTimeHealthCrystal = GameMaster.GetInstance().playerStat.firstTimeHealthCrystal;
            firstTimeArmorCrystal = GameMaster.GetInstance().playerStat.firstTimeArmorCrystal;
            firstTimeAttackSpeedCrystal = GameMaster.GetInstance().playerStat.firstTimeAttackSpeedCrystal;
            firstTimeRainbowCrystal = GameMaster.GetInstance().playerStat.firstTimeRainbowCrystal;
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

        currentCoin = GameMaster.GetInstance().playerStat.currentCoin;

        controller = GetComponent<PlayerController>();
        collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
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
        if (currentArmor < maxArmor &&
            currentHealth > 0)
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
        ShowDamage(damageAmount.ToString());
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
            AudioManager.GetInstance().Play("GameOver");
            controller.UnableControl();
            collider.enabled = false;
            controller.anim.SetTrigger("Die");

            StartCoroutine(DelayShowDeadCanvas());
        }
    }

    IEnumerator DelayShowDeadCanvas()
    {
        yield return new WaitForSeconds(1.5f);
        Instantiate(PrefabContainer.GetInstance().deadCanvasPrefab, Vector3.zero, Quaternion.identity);
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
