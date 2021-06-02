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

        String buildNo = "v0.850";

        BreakerBoxSystem[] breakers;


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