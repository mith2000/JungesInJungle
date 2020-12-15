using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer sprite;
    [SerializeField] 
    private Vector2 deadZone = new Vector2(.2f,.2f);

    [SerializeField] 
    protected GameObject projectile;
    [SerializeField] 
    protected Transform shotPoint;
    private float attackRate = 6f;
    private int shotCost = 30;

    [SerializeField]
    Joystick aimJoystick;

    [SerializeField]
    private PlayerInfo playerInfo;
    [SerializeField]
    private PlayerController playerController;

    float shotTime;

    private void Awake()
    {
        //Cache References
        sprite = GetComponent<SpriteRenderer>();
        aimJoystick.gameObject.SetActive(true);
    }

    void Start()
    {
        sprite.enabled = false;
    }

    void Update()
    {
        if (aimJoystick == null) return;
        //Debug.Log(aimJoystick.isRelease);
        RenderBaseOnJoystick();
        RotateBaseOnJoystick();
        ShootWhenRelease();
    }

    bool Aiming()
    {
        return Mathf.Abs(aimJoystick.Horizontal) >= deadZone.x || Mathf.Abs(aimJoystick.Vertical) >= deadZone.y;
    }

    void RenderBaseOnJoystick()
    {
        if (Aiming())
        {
            sprite.enabled = true;
        }
        else
        {
            sprite.enabled = false;
        }
    }

    void RotateBaseOnJoystick()
    {
        Vector2 direction = new Vector2(aimJoystick.Horizontal, aimJoystick.Vertical);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
    }

    void ShootWhenRelease()
    {
        if (Aiming())
        {
            if (aimJoystick.isRelease)
            {
                if (playerInfo.CanApplyEnergy(shotCost) && !playerController.isStunned)
                {
                    if (Time.time - shotTime >= 1 / attackRate)
                    {
                        Shoot();
                        playerInfo.ApplyEnergy(shotCost);
                        //playerInfo.ResetDelayStartRegenArmor();
                        shotTime = Time.time;
                    }
                }
            }
        }
    }

    public virtual void Shoot()
    {
        Instantiate(projectile, shotPoint.position, transform.rotation);
    }
}
