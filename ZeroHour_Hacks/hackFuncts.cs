using Photon.Pun;
using Steamworks;
using System;
using UnityEngine;

namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        private void ExecuteSwitchedHack(bool item, Action enabled, Action disabled)
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

        private void UnlimitedGrenades()
        {
            if (local_User.myWeaponManager.UsedGadget1 || local_User.myWeaponManager.UsedGadget2)
            {

                local_User.myWeaponManager.UsedGadget1 = false;
                local_User.myWeaponManager.UsedGadget2 = false;

                local_User.myWeaponManager.US_Script.MyPhoton.RPC("SetGadgetSlot", RpcTarget.All, new object[]
                {
                    local_User.myWeaponManager.gadgetSlotselected
                });
            }
        }

        private void UnlimitedTraps()
        {
            local_User.myWeaponManager.PlacedTrap = false;
        }

        private void ForceSetFOV()
        {
            m_GameSettings.GameplayFOV = customFOV;
            PlayerPrefs.SetFloat("GameplayFOV", customFOV);

        }

        private void AntiSoftBan_Funct()
        {
            if (antiSoftBan)
            {
                m_GameNetwork.SavedDatas.Thok = 0;
                SteamUserStats.SetStat(m_GameNetwork.ThokStat.name, 0);
            }
        }

        private void KillAIDefender()
        {
            killAll = true;
            return;
        }

        private void UnlockAllDoors_SP()
        {
            foreach (InteractiveDoorSystem door in m_DoorManager.Doors)
            {
                door.Locked = false;
            }
        }

        private void KillAIEntity(ZH_AINav entity)
        {
            entity.Surrendering = false;
            entity.ActiveShooter = true;
            entity.OnDeath();
            entity.enabled = false;
        }

        private void ArrestAIDefenders()
        {
            foreach (ZH_AINav enemy in m_ZH_AIManager.AliveEnemies)
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
                    m_ZH_AIManager.ArrestedEnemies.Add(enemy);
                }
            }
        }

        private void ForceCompleteObjectives_SP()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in m_ZH_AIManager.Objectives)
            {
                Obj.Completed = true;
            }
        }

        private void DisarmAllDoorTraps_SP()
        {
            foreach (DoorTrapSystem trap in m_DoorTrapManager.Traps)
            {
                trap.ACTIVE = false;
            }
        }

        private void CollectDroppedAIWeapons()
        {
            m_ZH_AIManager.SecuredWeapons = m_ZH_AIManager.SpawnedEnemies.Count;
        }

        private void ExtraSPPoints()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in m_ZH_AIManager.Objectives)
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

        private void ForceToggleNVG()
        {
             m_GameSettings.ANVGScript.On = !m_GameSettings.ANVGScript.On;
        }

        private bool HasPlayerWeaponChanged()
        {
            string currentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            if (lastWeapon == currentWeapon)
            {
                return false;
            }
            lastWeapon = currentWeapon;
            return true;
        }

        private void ApplyInfiniteStamina()
        {
            local_User.myWeaponManager.CurrentWeapon.ex_Weight = 0;
        }

        private void RemoveInfiniteStamina()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.ex_Weight = weapon.ex_weight;
                    return;
                }
            }
        }

        private void ApplyNoRecoil()
        {
            m_cameraRig = local_User.myWeaponManager.CamScript;
            m_cameraRig.RecoilX = 0f;
            m_cameraRig.RecoilY = 0f;
            local_User.myWeaponManager.CurrentWeapon.ex_Recoil = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray = 0f;
        }

        private void RemoveNoRecoil()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
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

        private void ApplyFullAutoWeapons()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = true;
        }

        private void RemoveFullAutoWeapons()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {

                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = weapon.Automatic;
                    return;
                }
            }
        }

        private void ApplyInstantHit()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Speed = 10000f;
        }

        private void RemoveInstantHit()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Speed = weapon.speed;
                    return;
                }
            }
        }

        private void ApplyDamageHack()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
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

        private void RemoveDamageHack()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
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

        private void ApplyBulletsPerShot()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = Mathf.RoundToInt(bulletsPerShot_Amount.currentValue) * weapon.BulletsPerShot;
                    return;
                }
            }
        }

        private void RemoveBulletsPerShot()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = weapon.BulletsPerShot;
                    return;
                }
            }
        }

        private void ApplyFireRate()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate / fireRate_Multiplier.currentValue;
                    return;
                }
            }
        }

        private void RemoveFireRate()
        {
            string curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weps.weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate;
                    return;
                }
            }
        }

    }
}
