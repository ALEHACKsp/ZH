using System;
using UnityEngine;
using CustomTypes;

namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        bool noRecoil = false;
        bool automaticWeapons = false;
        bool infStamina = false;
        String lastWeapon;
#if PVT
        bool fireRate = false;
        bool bulletsPerShot = false;
        bool damageHack = false;
        bool instantHit = false;
        m_Types.trackedFloat bulletsPerShot_Amount = new m_Types.trackedFloat(1);
        m_Types.trackedFloat fireRate_Multiplier = new m_Types.trackedFloat(1);
        m_Types.trackedFloat damageHack_Amount_Multiplier = new m_Types.trackedFloat(1);
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




        /////////////

        bool esp_AI_Master = false;
        bool esp_AI_Distance = true;
        bool esp_AI_Box = true;
        bool esp_AI_Headdot = false;
        bool esp_Objective = false;
        bool esp_Traps = false;
        bool esp_Civs = false;

        bool esp_Master = false;
        bool esp_Team = true;
        bool esp_Distance = true;
        bool esp_Box = true;
        bool esp_Headdot = false;
        bool esp_HPBars = false;
        bool esp_Throwables = false;
        bool esp_Weapon = false;
        bool esp_Name = false;
        bool esp_HPNums = false;
        bool esp_DeadBodies  = false;
        bool esp_Breakers = false;
        bool esp_Skeleton = false;
        bool esp_HPSkeleton = false;
        float skeletonThickness = 1f;
        bool killAll;


        bool general_Crosshair = false;
        bool general_Ammo = false;

        bool showMenu = true;


    }
}
