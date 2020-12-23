using UnityEngine;

public class Aimer : MonoBehaviour
{
    [SerializeField] Joystick aimJoystick;
    [SerializeField] GameObject aimLine;
    [SerializeField] protected Transform shotPoint;
    [SerializeField] protected GameObject projectile;

    [SerializeField] Vector2 deadZone = new Vector2(.2f, .2f);

    [Header ("Player References")]
    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] PlayerController playerController;

    float shotTime;
    float attackRate = 6f;
    int shotCost = 30;

    void Update()
    {
        if (aimJoystick == null) return;
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
            aimLine.SetActive(true);
        }
        else
        {
            aimLine.SetActive(false);
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
