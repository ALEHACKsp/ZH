using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using _GUI;
using CustomTypes;

namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        void killAi()
        {
            killAll = true;
            return;
        }
        void unlockDoors()
        {
            foreach (InteractiveDoorSystem door in m_DoorManager.Doors)
            {
                door.Locked = false;
            }
        }
        void killEnemy(ZH_AINav enemy)
        {
            enemy.Surrendering = false;
            enemy.ActiveShooter = true;
            enemy.OnDeath();
            enemy.enabled = false;
        }
        void arrestEnemies()
        {
            foreach (ZH_AINav enemy in aiMan.AliveEnemies)
            {
                if (enemy.takenHostage) // release Hostage
                {
                    enemy.OnFlashed();
                }
                if (enemy.Proned)
                {
                    enemy.Proned = false;
                }
                if (!enemy.isObjective)
                {
                    enemy.OnArrested();
                    enemy.GotArrested = true; //makes no difference?
                    enemy.TriggerArrestAnim();
                    aiMan.ArrestedEnemies.Add(enemy);
                }
            }
        }
        void completeObjs()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
            {
                Obj.Completed = true;
            }
        }
        void disarmTraps()
        {
            foreach (DoorTrapSystem trap in trapMan.Traps)
            {
                trap.ACTIVE = false;
            }
        }
        void collectWeapons()
        {
            aiMan.SecuredWeapons = aiMan.SpawnedEnemies.Count;
        }
        void extraPoints()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
            {
                if (Obj.ExtraPoints > 0)
                {
                    Obj.ExtraPoints *= 2;
                }
                else
                {
                    Obj.ExtraPoints = 20;
                }
            }
        }
        void doNVG()
        {
            GameSettings[] settings = FindObjectsOfType<GameSettings>();
            foreach (GameSettings set in settings)
            {
                set.ANVGScript.On = !set.ANVGScript.On;
            }
        }
         bool playerWeaponChanged()
        {
            String currentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            if (lastWeapon == currentWeapon)
            {
                return false;
            }
            lastWeapon = currentWeapon;
            return true;
        }
         void _infStam()
        {
            local_User.myWeaponManager.CurrentWeapon.ex_Weight = 0;
        }
         void _infStamDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.ex_Weight = weapon.ex_weight;
                    return;
                }
            }
        }
         void _noRecoil()
        {
            m_cameraRig.RecoilX = 0f;
            m_cameraRig.RecoilY = 0f;
            local_User.myWeaponManager.CurrentWeapon.ex_Recoil = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray = 0f;
        }
         void _noRecoilDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.ex_Recoil = weapon.ex_Recoil;
                    local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate = weapon.HeatRate;
                    local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount = weapon.recoilAmount;
                    local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime = weapon.recoilRecoverTime;
                    local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray = weapon.MaxSpray;
                    local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray = weapon.MinSpray;
                    return;
                }
            }
        }
         void _automaticWeapons()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = true;
        }
         void _automaticWeaponsDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {

                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = weapon.Automatic;
                    return;
                }
            }
        }
         void doSwitchedHack(bool item, Action enabled, Action disabled)
        {
            if (item)
            {
                enabled();
            }
            else
            {
                disabled();
            }

        }

         void doSwitchedHackFloat(bool item, Action enabled, Action disabled)
        {
            if (item )
            {
                enabled();
            }
            else
            {
                disabled();
            }

        }
#if PVT
         void _instantHit()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Speed = 10000f;
        }
         void _instantHitDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Speed = weapon.speed;
                    return;
                }
            }
        }
         void _damageHack()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Damage = weapon.Damage * damageHack_Amount_Multiplier.currentValue;
                    local_User.myWeaponManager.CurrentWeapon.ex_Dmg = 0f;
                    return;
                }
            }
        }
         void _damageHackDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Damage = weapon.Damage;
                    local_User.myWeaponManager.CurrentWeapon.ex_Dmg = weapon.ex_Dmg;
                    return;
                }
            }
        }
         void _bulletsPerShot()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = Mathf.RoundToInt(bulletsPerShot_Amount.currentValue);
        }
         void _bulletsPerShotDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = weapon.BulletsPerShot;
                    return;
                }
            }
        }
         void _fireRate()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate / fireRate_Multiplier.currentValue;
                    return;
                }
            }
        }
         void _fireRateDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate;
                    return;
                }
            }
        }
#endif
    }
}
