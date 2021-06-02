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
#if PVT
        void window_AimbotFunct(int id)
        {

            aimbot = m_GUI.makeCheckbox(aimbot, "Silent Aimbot", 1);
            m_GUI.makeLabel("Aimbot FOV: " + aimbotFOV.ToString("F0"), 2);
            aimbotFOV = m_GUI.makeSlider(aimbotFOV, 10, 300, 3);
            showFOV = m_GUI.makeCheckbox(showFOV, "Show FOV", 4, true, aimbot);
            disableAimkey = m_GUI.makeCheckbox(disableAimkey, "Disable Aimkey", 5, true, aimbot);

            teleportBullets = m_GUI.makeCheckbox(teleportBullets, "Shoot Through Walls",7,true, aimbot);
            m_GUI.makeLabel("Aim Target", 8);
            aimTargetDropDown.makeDropper(9);
            if (!aimTargetDropDown.show)
            {
                m_GUI.makeLabel("Aim Key", 10);
                aimKeyDropDown.makeDropper(11);
            }


            //modal window for aim key?
            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }
        }

        bool disableAimkey = false;
        bool teleportBullets = false;
        float offset_BulletSpawner = 0.25f; //0.25f is a good start, seems to work well.
        float aimbotFOV = 100f;
        bool showFOV = true;
        bool aimkeyPressed = false;
        UserInput playerAimTarget;

        bool aimbot = false;
        bool hasTarget = false;
       
        void aimbot_Controller()
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
                        switch (aimTargetDropDown.selection)
                        {
                            case "Head":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                                break;
                            case "Chest":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
                                break;
                            case "Dick":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Spine);
                                break;
                        }
                        
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
        void drawFOV()
        {
            if (aimbot && showFOV)
            {
                GUI.color = ((aimkeyPressed || disableAimkey)? Color.red : new Color(1, 1, 0, 0.2f));
                Vector2 pt = new Vector2((Screen.width / 2) - aimbotFOV, (Screen.height / 2) - aimbotFOV);
                Vector2 size = new Vector2(aimbotFOV * 2, aimbotFOV * 2);
                m_GUI.DrawBox(pt, size, 1, false);
            }
        }
        void handleAimkey()
        {
            if (!disableAimkey)
            {
                KeyCode key = KeyCode.Mouse3;
                switch (aimKeyDropDown.selection)
                {
                    case "Mouse 4":
                        key = KeyCode.Mouse3;
                        break;
                    case "Mouse 5":
                        key = KeyCode.Mouse4;
                        break;
                    case "Left Alt":
                        key = KeyCode.LeftAlt;
                        break;
                    case "Right Mouse":
                        key = KeyCode.Mouse1;
                        break;
                }

                if (Input.GetKey(key))
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
