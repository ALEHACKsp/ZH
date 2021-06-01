using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using _GUI;
using CustomTypes;

namespace ZeroHour_Hacks
{
    public partial class hackMain : MonoBehaviour
    {
        public m_Types.trackedBool noRecoil = new m_Types.trackedBool(false);
        public m_Types.trackedBool automaticWeapons = new m_Types.trackedBool(false);
        public m_Types.trackedBool infStamina = new m_Types.trackedBool(false);
        String lastWeapon;
#if PVT
        public m_Types.trackedBool fireRate = new m_Types.trackedBool(false);
        public m_Types.trackedBool bulletsPerShot = new m_Types.trackedBool(false);
        public m_Types.trackedBool damageHack = new m_Types.trackedBool(false);
        public m_Types.trackedBool instantHit = new m_Types.trackedBool(false);
        public m_Types.trackedFloat bulletsPerShot_Amount = new m_Types.trackedFloat(1);
        public m_Types.trackedFloat fireRate_Multiplier = new m_Types.trackedFloat(1);
        public m_Types.trackedFloat damageHack_Amount_Multiplier = new m_Types.trackedFloat(1);
#endif

        Color playerHudColor = Color.white;
        Color esp_AI_Vis = Color.red;
        Color esp_AI_NoVis = new Color(1, 0.6f, 0f, 0.7f);

        Color esp_Enemy_Vis = Color.red;
        Color esp_Enemy_NoVis = new Color(1, 0.6f, 0f, 0.7f);

        Color esp_Nade_Vis = Color.red;
        Color esp_Nade_NoVis = new Color(1, 1f, 0f, 0.7f);

        Color esp_Obj_Vis = Color.green;
        Color esp_Obj_NoVis = new Color(0, 1f, 0.7f, 0.7f);

        Color esp_Team_Vis = new Color(0, 1f, 0.3f, 1);
        Color esp_Team_NoVis = new Color(0, 0.7f, 1, 0.7f);


        
        WeaponInfo weps = new WeaponInfo();
        List<WeaponInfo.wep> weaponDatas = new List<WeaponInfo.wep>();

        public void populateWeps()
        {
            weaponDatas.Add(weps.m17);
            weaponDatas.Add(weps.falcon);
            weaponDatas.Add(weps.sr762);
            weaponDatas.Add(weps.m4);
            weaponDatas.Add(weps.kyanite);
            weaponDatas.Add(weps.es36);
            weaponDatas.Add(weps.rattler);
            weaponDatas.Add(weps.oppressor);
            weaponDatas.Add(weps.shotgun);
            weaponDatas.Add(weps.mac10);
            weaponDatas.Add(weps.shield);
        }



        /////////////

        public bool esp_AI_Master = false;
        public bool esp_AI_Distance = false;
        public bool esp_AI_Box = false;
        public bool esp_AI_Headdot = false;
        public bool esp_Objective = false;
        public bool esp_Traps = false;
        public bool esp_Civs = false;

        public bool esp_Master = false;
        public bool esp_Team = false;
        public bool esp_Distance = false;
        public bool esp_Box = false;
        public bool esp_Headdot = false;
        public bool esp_HPBars = false;
        public bool esp_Throwables = false;
        public bool esp_Weapon = false;
        public bool esp_Name = false;
        public bool esp_HPNums = false;
        public bool esp_DeadBodies  = false;
        public bool esp_Breakers = false;
        public bool killAll;

        public bool general_Crosshair = false;
        public bool general_Ammo = false;

        public bool showMenu = true;


    }
}
