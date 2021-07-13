using _GUI;
using UnityEngine;


namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {

        private void AimbotController()
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
                        target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                        switch (aimTargetDropDown.selection)
                        {
                            case "Head":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
                                break;
                            case "Chest":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest);
                                break;
                            case "Pelvis":
                                target = playerAimTarget.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Spine);
                                break;
                        }

                    }
                    else
                    {
                        target = local_User.myWeaponManager.CurrentWeapon.bulletAimReference; //restore position
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

                playerAimTarget = null;
            }
        }

        private void AimbotFOVHandler()
        {
            if (aimbot && showFOV)
            {
                m_GUI.DrawCircle(new Vector2((Screen.width / 2), (Screen.height / 2)), aimbotFOV, 200, (((aimkeyPressed || disableAimkey) ? Color.red : new Color(1, 1, 0, 0.2f))), true, 2);
            }
        }

        private void AimkeyHandler()
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
                }
                else
                {
                    aimkeyPressed = false;
                }
            }
        }

    }
}
