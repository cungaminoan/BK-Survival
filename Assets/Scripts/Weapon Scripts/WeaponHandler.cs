using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;
    public WeaponAim weaponAim;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource shootSound, reloadSound;
    public WeaponFireType fireType;
    public WeaponBulletType bulletType;
    public GameObject attackPoint;
    // Start is called before the first frame update

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void ShootAnimation()
    {
        animator.SetTrigger(AnimationTags.SHOOT_TRIGGER);
    }
    
    public void Aim(bool canAim)
    {
        animator.SetBool(AnimationTags.AIM_PARAMETER, canAim);
    }

    protected void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }

    protected void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    protected void PlayShootSound()
    {
        shootSound.Play();
    }

    protected void PlayReloadSound()
    {
        reloadSound.Play();
    }

    protected void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    
    protected void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
