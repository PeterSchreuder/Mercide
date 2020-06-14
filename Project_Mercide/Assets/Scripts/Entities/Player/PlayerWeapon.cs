using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;


public class PlayerWeapon : EntityWeapon
{
    public override bool Shoot()
    {
        bool _return = false;

        if (base.Shoot())
        {
            _return = true;

            CameraShaker.Instance.ShakeOnce(weaponTemplate.wpRecoil, weaponTemplate.wpRecoilRougness, weaponTemplate.wpRecoilFadeInTime, weaponTemplate.wpRecoilFadeOutTime);
        }

        return _return;
    }
}
