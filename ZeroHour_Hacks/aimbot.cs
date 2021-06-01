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
#if PVT
        private void window_AimbotFunct(int id)
        {
            aimbot = m_GUI.makeCheckbox(aimbot, "Silent Aimbot", 1);
            m_GUI.makeLabel("Aimbot FOV: " + aimbotFOV.ToString("F0"), 2);
            aimbotFOV = m_GUI.makeSlider(aimbotFOV, 10, 300, 3);
            showFOV = m_GUI.makeCheckbox(showFOV, "Show FOV", 4, true, aimbot);
            disableAimkey = m_GUI.makeCheckbox(disableAimkey, "Disable Aimkey", 5, true, aimbot);

            teleportBullets = m_GUI.makeCheckbox(teleportBullets, "Shoot Through Walls",7,true, aimbot);



            //modal window for aim key?
            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }
        }

        public bool disableAimkey = false;
        public bool teleportBullets = false;
        public float offset_BulletSpawner = 0.25f; //0.25f is a good start, seems to work well.
        public float aimbotFOV = 100f;
        public bool showFOV = true;
        public bool aimkeyPressed = false;
        UserInput playerAimTarget;

        public bool aimbot = false;
        public bool hasTarget = false;
       
        public void aimbot_Controller()
        {
            if (aimbot)
            {
                local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position;
                local_User.myWeaponManager.CurrentWeapon.BulletSpawner.rotation = local_User.myWeaponManager.AimPosition.rotation;

                if (aimkeyPressed || disableAimkey)
                {
                    Transform target;
                    if (playerAimTarget != null)
                    {
                        hasTarget = true;
                        target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                    }
                    else
                    {
                        target = local_User.myWeaponManager.CurrentWeapon.bulletAimReference; //restore position after target killed?
                    }

                    if (local_User.myWeaponManager.Aim || teleportBullets)
                    {

                        local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = Vector3.MoveTowards(target.position, local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position, offset_BulletSpawner);
                    }
                    else
                    {
                        local_User.myWeaponManager.CurrentWeapon.BulletSpawner.position = local_User.myWeaponManager.CurrentWeapon.bulletAimReference.position;

                        local_User.myWeaponManager.CurrentWeapon.BulletSpawner.LookAt(target);
                        local_User.myWeaponManager.CurrentWeapon.defaultVal = local_User.myWeaponManager.CurrentWeapon.BulletSpawner.localRotation;
                    }
                }
                //reset
                hasTarget = false;
                playerAimTarget = null; //can keep target without this?
            }
        }
        public void drawFOV()
        {
            if (aimbot && showFOV)
            {
                GUI.color = ((aimkeyPressed || disableAimkey)? Color.red : new Color(1, 1, 0, 0.2f));
                Vector2 pt = new Vector2((Screen.width / 2) - aimbotFOV, (Screen.height / 2) - aimbotFOV);
                Vector2 size = new Vector2(aimbotFOV * 2, aimbotFOV * 2);
                m_GUI.DrawBox(pt, size, 1, false);
            }
        }
        public void handleAimkey()
        {
            if (!disableAimkey)
            {
                if (Input.GetKey(KeyCode.Mouse3))
                {
                    aimkeyPressed = true;
                } //aimkey
                else
                {
                    aimkeyPressed = false;
                }
            }
        }
#endif

    }
}
