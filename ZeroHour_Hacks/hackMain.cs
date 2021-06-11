using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _GUI;
using CustomTypes;

namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        String buildNo = "v1.10";

        private float CDTimer = 21600;
        BreakerBoxSystem[] breakers;

        bool panicKey = false;
        bool panicKeyConfirm = false;

        public void Start()
        {
            baseInfo += buildNo;
            killAll = false;
            weps.populateWeps();
        }

        public void Update()
        {
            populateEntityLists_fast();
            populateEntityLists_late();
            menuTimerOperation();
#if PVT
            handleAimkey();
#endif
            CDTimer -= Time.deltaTime;
            if (CDTimer <= 0)
            {
                Destroy(this);
            }

            if (Input.GetKey(KeyCode.F9))
            {
                panicKey = true;
            }
            else
            {
                panicKey = false;
            }
            if (Input.GetKey(KeyCode.F10))
            {
                panicKeyConfirm = true;
            }
            else
            {
                panicKeyConfirm = false;
            }
        }

        public void LateUpdate()
        {
#if PVT
            aimbot_Controller();
#endif
        }
        public void FixedUpdate()
        {
            if (!local_User.myWeaponManager.CurrentWeapon.Properties.GunName.Contains("BALL")) //ballistic shield
            {
                bool weaponChanged = playerWeaponChanged();
                doSwitchedHack(noRecoil, _noRecoil, _noRecoilDisable);
                doSwitchedHack(automaticWeapons, _automaticWeapons, _automaticWeaponsDisable);
                doSwitchedHack(infStamina, _infStam, _infStamDisable);
#if PVT
                doSwitchedHack(fireRate, _fireRate, _fireRateDisable);
                doSwitchedHack(bulletsPerShot,  _bulletsPerShot, _bulletsPerShotDisable);
                doSwitchedHack(damageHack, _damageHack, _damageHackDisable);
                doSwitchedHack(instantHit, _instantHit, _instantHitDisable);
#endif
            }
        }

        public void OnGUI()
        {
            if(CDTimer<600)
            {
                //display timer
                String s = "";
                if (CDTimer < 60)
                {
                    s += CDTimer.ToString("F0") + " Seconds";
                }
                else
                {
                    s += (CDTimer / 60).ToString("F0") + " Minutes";
                }
                GUI.Label(new Rect(50, 100, 300, 100), s  + " Until ZeroHax Unloads.\nPlease Re-Launch!");
            }

            if(panicKey)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect((Screen.width/2)-200, Screen.height / 2 , 500, 30), "Panic key (F9) pressed, press F10 also to unload ZeroHax!");
                if (panicKeyConfirm)
                {
                    Destroy(this);
                }
            }
            
            menu();
            playerLoop(); //you changed distance to 2.0f!
            aiLoop();
            trapLoop();
            civLoop();
            objLoop();
            breakerBoxLoop();
           
#if PVT
            drawFOV();
#endif
#if TESTING
            // basicESP(local_User.myWeaponManager.CurrentWeapon.BulletSpawner, "*");
            testStuff();
#endif
        }

    }//end class

}//end Namespace