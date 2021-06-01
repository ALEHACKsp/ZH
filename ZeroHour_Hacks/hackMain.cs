using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _GUI;
using CustomTypes;


namespace ZeroHour_Hacks
{
    public partial class hackMain : MonoBehaviour
    {

        String buildNo = "v0.802";

        BreakerBoxSystem[] breakers;


        public void Start()
        {
            baseInfo += buildNo;
            killAll = false;
            populateWeps();
        }


        public void Update()
        {
            populateEntityLists_fast();
            populateEntityLists_late();
            menuTimerOperation();

#if PVT
            handleAimkey();
#endif
        }
        public void LateUpdate()
        {
#if PVT
            aimbot_Controller();
#endif
        }
        public void FixedUpdate()
        {
            bool weaponChanged = playerWeaponChanged();
            m_Types.trackedBoolExecution(noRecoil, _noRecoil, _noRecoilDisable, weaponChanged);
            m_Types.trackedBoolExecution(automaticWeapons, _automaticWeapons, _automaticWeaponsDisable, weaponChanged);
            m_Types.trackedBoolExecution(infStamina, _infStam, _infStamDisable, weaponChanged);
#if PVT
            m_Types.trackedBoolExecutionWithFloat(fireRate, fireRate_Multiplier, _fireRate, _fireRateDisable, weaponChanged);
            m_Types.trackedBoolExecutionWithFloat(bulletsPerShot, bulletsPerShot_Amount, _bulletsPerShot, _bulletsPerShotDisable, weaponChanged);
            m_Types.trackedBoolExecutionWithFloat(damageHack, damageHack_Amount_Multiplier, _damageHack, _damageHackDisable, weaponChanged);
            m_Types.trackedBoolExecution(instantHit, _instantHit, _instantHitDisable, weaponChanged);
#endif
        }
        public void OnGUI()
        {

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


/* 
 * 
player head
//user.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position

 * 
 *locked door esp
 *breaker box esp
inf stamina with weapon stamina drain?

stop using the managers for traps and civs? just find the game objects


    //player look direction
    user.Ik_Script.lookObj.position

 * */