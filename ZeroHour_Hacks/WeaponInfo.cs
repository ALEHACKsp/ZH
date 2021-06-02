using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _GUI;
using CustomTypes;

namespace ZeroHour_Hacks
{
     class WeaponInfo
    {
        public class wep
        {
            public String name;
            public int BulletsPerShot;
            public bool Automatic;
            public float ex_Recoil;
            public float HeatRate;
            public float recoilAmount;
            public float recoilRecoverTime;
            public float MaxSpray;
            public float MinSpray;
            public float fireRate;
            public float Damage;
            public float ex_Dmg;
            public float speed;
            public float ex_weight;
            public wep(String _name, int _BulletsPerShot, bool _Automatic,
                        float _ex_Recoil, float _HeatRate, float _recoilAmount,
                        float _recoilRecoverTime, float _MaxSpray, float _MinSpray,
                        float _fireRate, float _Damage, float _ex_Dmg,float _speed,float _ex_weight)
            {
                BulletsPerShot = _BulletsPerShot;
                Automatic = _Automatic;
                ex_Recoil = _ex_Recoil;
                HeatRate = _HeatRate;
                recoilAmount = _recoilAmount;
                recoilRecoverTime = _recoilRecoverTime;
                MaxSpray = _MaxSpray;
                MinSpray = _MinSpray;
                fireRate = _fireRate;
                Damage = _Damage;
                ex_Dmg = _ex_Dmg;
                name = _name;
                speed = _speed;
                ex_weight = _ex_weight;
            }
        }

            wep m17     = new wep("MOCK 17", 1, false, -1.6f, 1, 5, 0.25f, 0, 0,                     0.16f, 25, -2, 37,0.1405f);
            wep falcon  = new wep(".50 FALCON", 1, false, -2, 3, 7, 0.25f, 0, 0,                  0.3f, 50, -10 , 37,0.153f);
            wep sr762   = new wep("SR 7.62", 1, true, -0.1f, 1, 1.25f, 0.25f, 0.15f, -0.15f,       0.12f, 35, -2, 37,0.195f);
            wep m4      = new wep("M4", 1, true, -0.1f, 0.75f, 1.23f, 0.25f, 0.1f, -0.1f,             0.1f, 30, -3, 37,0.1705f);
            wep kyanite = new wep("Kyanite", 1, true, -0.1f, 1, 1.2f, 0.25f, 0.12f, -0.12f,      0.1f, 30, -6, 37,0.183f);
            wep es36    = new wep("ES36", 1, true, 0, 0.6f, 1.26f, 0.22f, 0.25f, -0.25f,            0.1f, 26, 0, 37,0.171f);
            wep rattler = new wep("RATTLER", 1, true, -0.15f, -.3f, 1.15f, 0.23f, 0.9f,  -0.9f,  0.088f, 20, -3, 37,0.1585f);
            wep oppressor = new wep("OPPRESSOR", 1, true, 0, 0.5f, 1f, 0.27f, 0.2f, -0.2f,       0.09f, 25, 0, 37,0.161f);
            wep shotgun = new wep("TACTICAL SHOTGUN", 8, false, -0.25f, 5, 2, 0.25f, 3, -3,      1, 30, -5, 37,0.2045f);
            wep mac10   = new wep("MAC-10", 1, true, -0.15f, 0.35f, 2, 0.25f, 0.8f, -0.8f,         0.072f, 20, -3, 37,0.137f);
            wep shield  = new wep("BALLISTIC SHIELD", 0, true, 0, 1, 1.2f, 0.25f, 0, 0,           0, 0, 0, 37,0.21f);

        public List<wep> weaponDatas = new List<wep>();

        public void populateWeps()
        {
            weaponDatas.Add(m17);
            weaponDatas.Add(falcon);
            weaponDatas.Add(sr762);
            weaponDatas.Add(m4);
            weaponDatas.Add(kyanite);
            weaponDatas.Add(es36);
            weaponDatas.Add(rattler);
            weaponDatas.Add(oppressor);
            weaponDatas.Add(shotgun);
            weaponDatas.Add(mac10);
            weaponDatas.Add(shield);
        }

    }

}
