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
        String buildNo = "v0.72";

        public void Start()
        {
            baseInfo += buildNo;
            killAll = false;
            populateWeps();
        }


        public void Update()
        {
            
            populateEntityLists();


            if (Input.GetKey(KeyCode.Mouse3))
            {
                aimkeyPressed = true;
            }
            else
            {
                aimkeyPressed = false;
            }


            testStuff();


        }
        public void LateUpdate()
        {
#if TESTING
            testStuff();


#endif
        }
        public void FixedUpdate()
        {
            

#if PVT
            bool weaponChanged = playerWeaponChanged();
            m_Types.trackedBoolExecution(noRecoil, _noRecoil, _noRecoilDisable, weaponChanged);
            m_Types.trackedBoolExecutionWithFloat(fireRate, fireRate_Multiplier, _fireRate, _fireRateDisable, weaponChanged);
            m_Types.trackedBoolExecutionWithFloat(bulletsPerShot, bulletsPerShot_Amount, _bulletsPerShot, _bulletsPerShotDisable, weaponChanged);
            m_Types.trackedBoolExecutionWithFloat(damageHack, damageHack_Amount_Multiplier, _damageHack, _damageHackDisable, weaponChanged);
            m_Types.trackedBoolExecution(instantHit, _instantHit, _instantHitDisable, weaponChanged);
            m_Types.trackedBoolExecution(automaticWeapons, _automaticWeapons, _automaticWeaponsDisable, weaponChanged);
#endif
        }
        public void OnGUI()
        {

            menu();
            playerLoop(); //you changed distance to 2.0f!
            aiLoops();

            basicESP(local_User.myWeaponManager.CurrentWeapon.BulletSpawner, "*");

        }

        Rect window_test = new Rect(200, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2))+500, m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer + 500);
#if TESTING

        private void window_TestFunct(int id)
        {

            test_item_1 = m_GUI.makeCheckbox(test_item_1, "test_item_", 1);
            test_item_2 = m_GUI.makeCheckbox(test_item_2, "test_item_2", 2);
            test_item_3 = m_GUI.makeCheckbox(test_item_3, "test_item_3", 3);
            test_item_4 = m_GUI.makeCheckbox(test_item_4, "test_item_4", 4);
            test_item_5 = m_GUI.makeCheckbox(test_item_5, "test_item_5", 5);

            offset_BulletSpawner = GUI.HorizontalSlider(new Rect(10, 300, 500, 20), offset_BulletSpawner, 0, 1f);
            m_GUI.makeLabel(offset_BulletSpawner.ToString("F8"), 6);
            //   m_GUI.makeButton(increasePlane, "planedis", 1);
            //   m_GUI.makeButton(movet, "asds", 2);

            //   newPos.x = GUI.HorizontalSlider(new Rect(10, 100, 400, 30), newPos.x, 0-(Screen.width/2), (Screen.width / 2));
            //   newPos.y = GUI.HorizontalSlider(new Rect(10, 150, 400, 30), newPos.y, 0 - (Screen.height / 2),  (Screen.height / 2));
            //   GUI.Label(new Rect(100, 150, 400, 30), "x-" + newPos.x + "y-" + newPos.y);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
#endif

        public bool test_item_1 = false;
        public bool test_item_2 = false;
        public bool test_item_3 = false;
        public bool test_item_4 = false;
        public bool test_item_5 = false;

        public float offset_BulletSpawner = 0.25f; //0.25f is a good start, seems to work well.
        public float aimbotFOV = 200f;
        ZH_AINav aimTarget;
        public bool aimkeyPressed = false;
        UserInput playerAimTarget;

        public void testStuff() 
        {
            if (aimkeyPressed)
            {
                Transform target;
                bool valid = false;
                if (playerAimTarget!= null)
                {
                    //target  = aimTarget.ManualReference.AimIKscript.solver.bones[4].transform; //AI
                    target = playerAimTarget.CameraScript;
                    valid = true;
                }
                else
                {
                    target = local_User.myWeaponManager.CurrentWeapon.bulletAimReference; //restore position after target killed?
                }


                if (test_item_1)
                {




                }

                if (test_item_2)
                {

                    if (local_User.myWeaponManager.Aim)
                    {

                        local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = Vector3.MoveTowards(target.position, local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position, offset_BulletSpawner);
                    }
                    else
                    {
                        local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position;
                        if (valid)
                        {
                            local_User.myWeaponManager.CurrentWeapon.BulletSpawner.LookAt(target);
                            local_User.myWeaponManager.CurrentWeapon.defaultVal = local_User.myWeaponManager.CurrentWeapon.BulletSpawner.localRotation;
                        }
                    }

                }

                if (test_item_3)
                {
                }

                if (test_item_4)
                {
                    //reset aim position
                    local_User.myWeaponManager.CurrentWeapon.BulletSpawner.LookAt(local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position);
                    local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position;

                    test_item_4 = false;
                }

                if (test_item_5)
                {

                }
            }

            aimTarget = null;
        }



    }//end class

}//end Namespace


/* 
 *              //silent aim, working when not aimed in.
                local_User.myWeaponManager.CurrentWeapon.BulletSpawner.LookAt(target);
                local_User.myWeaponManager.CurrentWeapon.defaultVal = local_User.myWeaponManager.CurrentWeapon.BulletSpawner.localRotation;

 * 
 *locked door esp
 *breaker box esp

    
 * */