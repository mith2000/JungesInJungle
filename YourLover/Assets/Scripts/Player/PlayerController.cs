using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] protected float speed = 5f;
    protected float baseSpeed = 5f;
    protected float addSpeed = 1f;

    [Header ("Joysticks")]
    [SerializeField] protected Joystick moveJoystick;
    [SerializeField] protected Joystick aimJoystick;

    [Header ("Buttons")]
    [SerializeField] protected Button skillButton;
    [SerializeField] protected SkillCooldown skillCooldownScript;
    [SerializeField] protected float skillCooldown = 5f;
    [SerializeField] protected Button interactButton;
    protected GameObject interactObject;

    protected Rigidbody2D physicBody;
    protected Animator anim;

    protected Vector2 direction;
    protected Vector2 saveDirection;
    protected Vector2 moveAmount;

    [HideInInspector] public bool isStunned = false;

    float flipYAmount = 180f;

    private void Awake()
    {
        CacheReferences();
    }

    public virtual void CacheReferences()
    {
        //Cache References
        physicBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        moveJoystick.gameObject.SetActive(true);
        skillButton.gameObject.SetActive(true);
        interactButton.gameObject.SetActive(false);
    }

    void Update()
    {
        Walking();
        FlipSprite();
        CheckControlled();
    }

    private void CheckControlled()
    {
        if (isStunned)
        {
            skillButton.gameObject.SetActive(false);
            //interactButton.gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        Moving();
    }

    public virtual void Moving()
    {
        if (DialogSystem.GetInstance() != null && DialogSystem.GetInstance().isInDialog) return;
        if (!isStunned)
        {
            physicBody.MovePosition(physicBody.position + moveAmount * Time.fixedDeltaTime);
        }
    }

    public void Walking()
    {
        if (moveJoystick == null) return;

        direction = new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical);
        if (direction != Vector2.zero)
        {
            saveDirection = direction;
        }

        moveAmount = direction.normalized * speed * addSpeed;

        if (direction != Vector2.zero)
        {
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
    }

    public void FlipSprite()
    {
        if (moveJoystick == null || isStunned) return;

        if (moveJoystick.Horizontal > 0) //(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        else if (moveJoystick.Horizontal < 0) //(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = new Quaternion(0, flipYAmount, 0, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPotion") ||
            collision.gameObject.CompareTag("NextStageGate") ||
            collision.gameObject.CompareTag("HealthCrystal") ||
            collision.gameObject.CompareTag("ArmorCrystal") ||
            collision.gameObject.CompareTag("AttackSpeedCrystal") ||
            collision.gameObject.CompareTag("RainbowCrystal"))
        {
            if (!isStunned)
            {
                skillButton.gameObject.SetActive(false);
                interactObject = collision.gameObject;
                interactButton.gameObject.SetActive(true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HealthPotion") ||
            collision.gameObject.CompareTag("NextStageGate") ||
            collision.gameObject.CompareTag("HealthCrystal") ||
            collision.gameObject.CompareTag("ArmorCrystal") ||
            collision.gameObject.CompareTag("AttackSpeedCrystal") ||
            collision.gameObject.CompareTag("RainbowCrystal"))
        {
            if (!isStunned)
            {
                skillButton.gameObject.SetActive(true);
                interactButton.gameObject.SetActive(false);
            }
        }
    }

    public void Interact_OnClick()
    {
        PlayerInfo info = this.gameObject.GetComponent<PlayerInfo>();
        if (interactObject.tag == "HealthPotion")
        {
            if (info.firstTimeHealthPotion == true)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.healthPotionSentences);
                DialogSystem.GetInstance().StartText();
                info.firstTimeHealthPotion = false;
            }
            else
            {
                info.ApplyHealth(-interactObject.GetComponent<HealthPotion>().healthAmount);
                interactObject.GetComponent<HealthPotion>().DestroyPotion();
            }
        }
        else if (interactObject.tag == "HealthCrystal")
        {
            if (GameMaster.GetInstance().playerStat.useCrystalTime >= GameMaster.GetInstance().maxCrystalPlayerUse)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.enoughCrystalSentences);
                DialogSystem.GetInstance().StartText();
                return; 
            }

            if (info.firstTimeHealthCrystal == true)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.healthCrystalSentences);
                DialogSystem.GetInstance().StartText();
                info.firstTimeHealthCrystal = false;
            }
            else
            {
                GameMaster.GetInstance().playerStat.useCrystalTime++;
                info.IncreaseHealth(interactObject.GetComponent<HealthCrystal>().healthAmount);
                interactObject.GetComponent<HealthCrystal>().DestroyPotion();
            }
        }
        else if (interactObject.tag == "ArmorCrystal")
        {
            if (GameMaster.GetInstance().playerStat.useCrystalTime >= GameMaster.GetInstance().maxCrystalPlayerUse)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.enoughCrystalSentences);
                DialogSystem.GetInstance().StartText();
                return;
            }

            if (info.firstTimeArmorCrystal == true)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.armorCrystalSentences);
                DialogSystem.GetInstance().StartText();
                info.firstTimeArmorCrystal = false;
            }
            else
            {
                GameMaster.GetInstance().playerStat.useCrystalTime++;
                info.IncreaseArmor(interactObject.GetComponent<ArmorCrystal>().armorAmount);
                interactObject.GetComponent<ArmorCrystal>().DestroyPotion();
            }
        }
        else if (interactObject.tag == "AttackSpeedCrystal")
        {
            if (GameMaster.GetInstance().playerStat.useCrystalTime >= GameMaster.GetInstance().maxCrystalPlayerUse)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.enoughCrystalSentences);
                DialogSystem.GetInstance().StartText();
                return;
            }

            if (info.firstTimeAttackSpeedCrystal == true)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.attackSpeedCrystalSentences);
                DialogSystem.GetInstance().StartText();
                info.firstTimeAttackSpeedCrystal = false;
            }
            else
            {
                GameMaster.GetInstance().playerStat.useCrystalTime++;
                info.IncreaseAttackSpeed(interactObject.GetComponent<AttackSpeedCrystal>().DecreaseDREPercent());
                interactObject.GetComponent<AttackSpeedCrystal>().DestroyPotion();
            }
        }
        else if (interactObject.tag == "RainbowCrystal")
        {
            if (GameMaster.GetInstance().playerStat.useCrystalTime >= GameMaster.GetInstance().maxCrystalPlayerUse)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.enoughCrystalSentences);
                DialogSystem.GetInstance().StartText();
                return;
            }

            if (info.firstTimeRainbowCrystal == true)
            {
                DialogSystem.GetInstance().AddSentences(DialogSystem.GetInstance().container.rainbowCrystalSentences);
                DialogSystem.GetInstance().StartText();
                info.firstTimeRainbowCrystal = false;
            }
            else
            {
                GameMaster.GetInstance().playerStat.useCrystalTime++;
                if (interactObject.GetComponent<RainbowCrystal>().isGood)
                {
                    info.IncreaseHealth(interactObject.GetComponent<RainbowCrystal>().healthAmount);
                    info.IncreaseArmor(interactObject.GetComponent<RainbowCrystal>().armorAmount);
                    info.IncreaseAttackSpeed(interactObject.GetComponent<RainbowCrystal>().DecreaseDREPercent());
                }
                else
                {
                    info.IncreaseHealth(interactObject.GetComponent<RainbowCrystal>().loseHealthAmount);
                }
                interactObject.GetComponent<RainbowCrystal>().DestroyPotion();
            }
        }
        else if (interactObject.tag == "NextStageGate")
        {
            interactObject.GetComponent<NextStageGate>().ExitStage();
        }
    }

    public void UnStunFromOther(float delay)
    {
        StartCoroutine(Unstun(delay));
    }

    IEnumerator Unstun(float delay)
    {
        yield return new WaitForSeconds(delay);

        isStunned = false;
        skillButton.gameObject.SetActive(true);
    }

    public void SlowFromOther(float percent, float duration = 60f)
    {
        addSpeed -= percent;
        StartCoroutine(Unslow(duration));
    }

    IEnumerator Unslow(float delay)
    {
        yield return new WaitForSeconds(delay);
        addSpeed = 1;
    }

    public void UnslowFromOther()
    {
        addSpeed = 1;
    }

    public void UnableControl()
    {
        moveJoystick.gameObject.SetActive(false);
        aimJoystick.gameObject.SetActive(false);
        skillButton.gameObject.SetActive(false);
        interactButton.gameObject.SetActive(false);
    }

    public void EnableControl()
    {
        moveJoystick.gameObject.SetActive(true);
        aimJoystick.gameObject.SetActive(true);
        skillButton.gameObject.SetActive(true);
    }
}
