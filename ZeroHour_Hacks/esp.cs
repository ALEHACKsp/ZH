using _GUI;
using RootMotion.FinalIK;
using UnityEngine;


namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        private void RenderPlayerHUD()
        {
            GUISkin skin2 = GUI.skin;
            GUIStyle newStyle2 = skin2.GetStyle("Label");
            newStyle2.fontSize = 36;
            GUI.color = PlayerHUDColor;
            GUI.Label(new Rect(Screen.width - 500, Screen.height - 500, 500, 500), local_User.myWeaponManager.CurrentWeapon.AmmoLeft.ToString() +
                        "/" + local_User.myWeaponManager.CurrentWeapon.Properties.Totalammo.ToString(), newStyle2);
            newStyle2.fontSize = 14;
        }

        private void RenderDynamicCrosshair()
        {
            if (!local_User.myWeaponManager.AimState && !local_User.myWeaponManager.Aim)
            {


                Ray ray = new Ray(local_User.myWeaponManager.CurrentWeapon.bulletspawn.position, local_User.myWeaponManager.CurrentWeapon.bulletspawn.forward);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Default", "Attacker", "Defender", "StairCollision", "Hitmark", "AIHitmark")))
                {
                    Vector3_ESP(hit.point, "+");
                }

            }
        }

        private void RenderAIDefenderESP(ZH_AINav enemy)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(enemy.transform.position);

            if (pos.z > 0) //in camera view
            {
                /* Bones:
                 * 0 - pelvis
                 * 1 - stomach
                 * 2 - chest
                 * 3 - neck
                 * 4 - head
                 * IKSolver.Bone[] bones = enemy.ManualReference.AimIKscript.solver.bones;
                 */

                // ** bone info ** //

                //Distance from player
                float distance = Vector3.Distance(m_Camera.transform.position, enemy.transform.position); //get distance

                //HeadboneE Position
                IKSolver.Bone headBone = enemy.ManualReference.AimIKscript.solver.bones[4];
                Vector3 headPos = m_Camera.WorldToScreenPoint(headBone.transform.position);

                Color thisColor;
                bool isVisible = VisCheckNoPenetration(headPos);

                if (isVisible)
                {
                    thisColor = esp_AI_Vis;

                }
                else
                {
                    thisColor = esp_AI_NoVis;
                }
                GUI.color = thisColor;

                //BOX calculations
                Vector3 artificialTop = m_Camera.WorldToScreenPoint(headBone.transform.position + (new Vector3(0f, 0.2f, 0f)));
                Vector3 artificialBottom = m_Camera.WorldToScreenPoint(enemy.transform.position + (new Vector3(0f, 0.05f, 0f)));
                float boxheight = Vector3.Distance(artificialTop, artificialBottom);
                float boxWidth = boxheight / 3f;
                float boxHorizontaloffset = boxWidth / 2f;

                Vector2 info_Right = new Vector2(artificialTop.x + boxHorizontaloffset + 5, Screen.height - artificialTop.y + 1);

                string info = "";

                if (esp_AI_Distance)
                {
                    info += distance.ToString("F1") + "M";
                }

                info += "\nAI_Enemy";

                if (enemy.GotArrested)
                {
                    info += "\n*Surrendered*";
                }
                if (enemy.takenHostage)
                {
                    info += "\n*Has Hostage*";
                }

                if (esp_AI_Box)
                {
                    if (enemy.Crouched)
                    {
                        boxWidth = boxheight / 2f;
                    }

                    if (enemy.Proned)
                    {

                        GUI.Label(new Rect(artificialTop.x - boxHorizontaloffset, Screen.height - artificialTop.y,
                             artificialTop.x - boxHorizontaloffset + 100, Screen.height - artificialTop.y + 20), "*PRONE*");
                    }

                    m_GUI.DrawBox(new Vector2(artificialTop.x - boxHorizontaloffset, Screen.height - artificialTop.y), new Vector2(boxWidth, boxheight), 1f, true);


                }

                //Information
                GUI.Label(new Rect(info_Right.x, info_Right.y, info_Right.x + 60, info_Right.y + 100), info);


                if (esp_AI_Headdot)
                {
                    GUI.Label(new Rect(headPos.x, Screen.height - headPos.y + ((boxheight / 100) * 3), 50, 50), "o");
                }
            }
        }

        private void RenderPlayerESP(UserInput a_player)
        {
            if (!esp_DeadBodies && !a_player.myHealth.alive)
            {
                return;
            }

            Vector3 pos = m_Camera.WorldToScreenPoint(a_player.Player.position);

            if (pos.z > 1)
            {
                Color thisColor;

                bool sameTeam = false;
                bool isVisible = false;
                try
                {
                    isVisible = (
                        CanHitWIthCurrentBasllistics(a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position,
                        m_Camera.transform.position,
                        local_User.myWeaponManager.CurrentWeapon.Properties.PenetrationLimit + 5));
                }
                catch { }

                if (isVisible)
                {
                    if (a_player.myLogger.TeamID == local_User.myLogger.TeamID)
                    {
                        if (!esp_Team)
                        {
                            return;
                        }
                        thisColor = esp_Team_Vis;
                        sameTeam = true;
                    }
                    else
                    {
                        thisColor = esp_Enemy_Vis;
                    }
                }
                else
                {
                    if (a_player.myLogger.TeamID == local_User.myLogger.TeamID)
                    {
                        if (!esp_Team)
                        {
                            return;
                        }
                        thisColor = esp_Team_NoVis;
                        sameTeam = true;


                    }
                    else
                    {
                        thisColor = esp_Enemy_NoVis;
                    }
                }

                GUI.color = thisColor;
                float distance = Vector3.Distance(m_Camera.transform.position, a_player.Player.position);

                Vector3 headPos = W2S_MainCamera(a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position);

                if (distance > 1f)
                {
                    Vector3 artificialTop = m_Camera.WorldToScreenPoint(a_player.CameraScript.position + (new Vector3(0f, 0.2f, 0f)));
                    Vector3 artificialBottom = m_Camera.WorldToScreenPoint(a_player.Player.position + (new Vector3(0f, 0.05f, 0f)));

                    float dotSize = 21 - ((distance - 5) / (5 - 50) * (5 - 20) + 5);
                    float boxheight = Vector3.Distance(artificialTop, artificialBottom);
                    float boxWidth = boxheight / 3f;
                    float boxHorizontaloffset = boxWidth / 2f;
                    Vector2 info_Right = new Vector2(artificialTop.x + boxHorizontaloffset + 5, Screen.height - artificialTop.y + 1);

                    float thisPlayerHP = a_player.myLogger.LocalHealth;
                    bool isAlive = a_player.myHealth.alive;

#if TESTING


#endif



                    if (aimbot)
                    {
                        if ((teleportBullets || isVisible) && (aimAtTeam ? true : !sameTeam) && isAlive && (Vector2.Distance(headPos, new Vector2((Screen.width / 2), (Screen.height / 2))) < aimbotFOV))
                        {
                            playerAimTarget = a_player;
                            Vector3 target = a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position;
                            switch (aimTargetDropDown.selection)
                            {
                                case "Head":
                                    target = a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position;
                                    break;
                                case "Chest":
                                    target = a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Chest).position;
                                    break;
                                case "Dick":
                                    target = a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Spine).position;
                                    break;
                            }
                            Vector3_ESP(target, "X");
                        }
                    }

                    string info = "";

                    if (isAlive)
                    {
                        if (a_player.crouched)
                        {
                            boxWidth = boxheight / 2f;
                        }

                        if (a_player.proned)
                        {
                            boxWidth = boxheight;
                            info += "*PRONE*\n";
                        }

                        if (esp_Box)
                        {
                            m_GUI.DrawBox(new Vector2(artificialTop.x - boxHorizontaloffset, Screen.height - artificialTop.y), new Vector2(boxWidth, boxheight), 1f, true);
                        }
                        if (esp_Distance)
                        {
                            info += distance.ToString("F1") + "M\n";
                        }

                        if (esp_Name)
                        {
                            info += a_player.myLogger.name + "\n";
                        }

                        if (esp_HPNums)
                        {
                            info += "HP:" + thisPlayerHP.ToString("F0") + "\n";
                        }

                        if (esp_Weapon)
                        {
                            info += a_player.myWeaponManager.CurrentWeapon.Properties.GunName;
                        }

                        if (esp_HPBars || esp_Skeleton || esp_Headdot)
                        {
                            Color hpColor = new Color(1, 1, 1, 1);//declare and setup here so we can do HP color on skeleton if we want

                            if (thisPlayerHP > 50)
                            {
                                hpColor.b = 0f;
                                hpColor.g = 1f;
                                hpColor.r = 1 - ((thisPlayerHP - 50) / 50);
                            }
                            else if (thisPlayerHP < 50)
                            {
                                hpColor.b = 0f;
                                hpColor.g = (thisPlayerHP * 2) / 100;
                                hpColor.r = 1f;
                            }
                            else if (thisPlayerHP == 50)
                            {
                                hpColor = Color.yellow;
                            }
                            if (esp_HPBars)
                            {
                                float boxScale = boxheight / 100;
                                float hpBarThiccness = boxScale * 3;
                                Vector2 hpBarStart = new Vector2(artificialTop.x - boxHorizontaloffset - hpBarThiccness - 1, Screen.height - artificialTop.y + 5);
                                Vector2 hpBarSize = new Vector2(hpBarThiccness, (boxScale * thisPlayerHP) - 10);
                                m_GUI.DrawBoxFill(hpBarStart, hpBarSize, hpColor);
                            }

                            if (esp_Skeleton)
                            {
                                if (!esp_HPSkeleton)
                                { GUI.color = thisColor; }
                                else { GUI.color = hpColor; }
                                RenderSkeletonESP(a_player);
                                GUI.color = hpColor;
                            }
                            if (esp_Headdot)
                            {
                                if (!esp_HPSkeleton)
                                { GUI.color = thisColor; }
                                else { GUI.color = hpColor; }
                                m_GUI.DrawCircle(new Vector2(headPos.x, Screen.height - headPos.y), ((boxheight / 100) * 5), 30, GUI.color, true, skeletonThickness);
                            }
                        }
                    }
                    else
                    {
                        info += "*DEAD*\n\n";
                    }
                    GUI.color = thisColor;
                    GUI.Label(new Rect(info_Right.x, info_Right.y, info_Right.x + 60, info_Right.y + 100), info);
#if TESTING

#endif
                }


            }
        }

        private void RenderSkeletonESP(UserInput a_player)
        {
            // head->neck->R/L shoulder->chest ->spine -> r/l upper leg -> r/l lower leg / r/l foot -> r/l toes
            // r/l shoulders -> r/l upper arms -> r/l lower arms -> r/l hands

            Render2dLineFromBones(a_player, HumanBodyBones.Head, HumanBodyBones.Neck);

            Render2dLineFromBones(a_player, HumanBodyBones.Neck, HumanBodyBones.LeftUpperArm);
            Render2dLineFromBones(a_player, HumanBodyBones.Neck, HumanBodyBones.RightUpperArm);
            Render2dLineFromBones(a_player, HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftUpperLeg);
            Render2dLineFromBones(a_player, HumanBodyBones.RightUpperArm, HumanBodyBones.RightUpperLeg);
            Render2dLineFromBones(a_player, HumanBodyBones.RightUpperLeg, HumanBodyBones.LeftUpperLeg);

            Render2dLineFromBones(a_player, HumanBodyBones.LeftUpperLeg, HumanBodyBones.LeftLowerLeg);
            Render2dLineFromBones(a_player, HumanBodyBones.RightUpperLeg, HumanBodyBones.RightLowerLeg);
            Render2dLineFromBones(a_player, HumanBodyBones.LeftLowerLeg, HumanBodyBones.LeftFoot);
            Render2dLineFromBones(a_player, HumanBodyBones.RightLowerLeg, HumanBodyBones.RightFoot);
            Render2dLineFromBones(a_player, HumanBodyBones.LeftFoot, HumanBodyBones.LeftToes);
            Render2dLineFromBones(a_player, HumanBodyBones.RightFoot, HumanBodyBones.RightToes);


            Render2dLineFromBones(a_player, HumanBodyBones.LeftUpperArm, HumanBodyBones.LeftLowerArm);
            Render2dLineFromBones(a_player, HumanBodyBones.RightUpperArm, HumanBodyBones.RightLowerArm);
            Render2dLineFromBones(a_player, HumanBodyBones.LeftLowerArm, HumanBodyBones.LeftHand);
            Render2dLineFromBones(a_player, HumanBodyBones.RightLowerArm, HumanBodyBones.RightHand);

        }

        private void Render2dLineFromBones(UserInput user, HumanBodyBones a, HumanBodyBones b)
        {

            RenderLine3d2d(user.Ik_Script.GetComponent<Animator>().GetBoneTransform(a).position,
                user.Ik_Script.GetComponent<Animator>().GetBoneTransform(b).position,
                Mathf.RoundToInt(skeletonThickness));
        }

        private void RenderLine3d2d(Vector3 origin, Vector3 end, int thickness)
        {
            Vector3 start, finish;
            start = W2S_MainCamera(origin);
            finish = W2S_MainCamera(end);

            m_GUI._DrawLine(new Vector2(start.x, Screen.height - start.y), new Vector2(finish.x, Screen.height - finish.y), thickness);
        }

        private void RenderObjectiveESP_SP(ZH_AIManager.CoopObjectiveVariables Obj)
        {
            int objType = (int)Obj.ObjectiveType; //we cast the type to an int because it pulls from an Obscured Enum type, the enum name and types change with each update, the values do not.

            if (!Obj.Completed && objType != 0 && objType != 2)
            {

                Vector3 pos = m_Camera.WorldToScreenPoint(Obj.TransformReference.position);
                    /* SP objective types:
                    *  0 - kill all enemies (expludes special targets)
                    *  1 - Hostage / Ambassador
                    *  2 - Kill special target
                    *  3 - ?
                    *  4 - Secure Intel
                    *  5 - Secure Document
                    */

                float distance = Vector3.Distance(m_Camera.transform.position, Obj.TransformReference.position);

                if (pos.z > 0)
                {

                    if (VisCheckNoPenetration(Obj.TransformReference.position))
                    {
                        GUI.color = esp_Obj_Vis;
                    }
                    else
                    {
                        GUI.color = esp_Obj_NoVis;
                    }


                    if (objType == 1)
                    {
                        GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "\n\n" + Obj.ObjectiveName);

                    }
                    else
                    {
                        GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "Objective\n" + distance.ToString("F1") + "\n" + Obj.ObjectiveName);
                    }
                }
            }

        }

        private void RenderCivillianESP(Transform t)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(t.position);


            if (pos.z > 0)
            {
                if (VisCheckNoPenetration(t.position))
                {
                    GUI.color = esp_Obj_Vis;
                }
                else
                {
                    GUI.color = esp_Obj_NoVis;
                }

                float distance = Vector3.Distance(m_Camera.transform.position, t.position);

                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "Hostage\n" + distance.ToString("F1"));
            }
        }

        private void RenderThrowableESP(ThrowableSystem throwable)
        {

            if (throwable.Thrown && !throwable.Blown) //add correct radius
            {
                Vector3 pos = m_Camera.WorldToScreenPoint(throwable.transform.position);

                if (pos.z > 0)
                {
                    if (VisCheckNoPenetration(throwable.transform.position))
                    {
                        GUI.color = esp_Nade_Vis;
                    }
                    else
                    {
                        GUI.color = esp_Nade_NoVis;
                    }

                    float distance = Vector3.Distance(m_Camera.transform.position, throwable.transform.position);
                    string text = throwable.name;

                    GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50),
                        text + " " + distance.ToString("F1") + "M\n" + throwable.TimeSettings.currentTimer.ToString("F1"));
                }
            }
        }

        private void RenderTrapESP(DoorTrapSystem trap)
        {
            if (trap.ACTIVE)
            {
                Vector3 pos = m_Camera.WorldToScreenPoint(trap.transform.position);

                if (pos.z > 0)
                {
                    if (VisCheckNoPenetration(trap.transform.position))
                    {
                        GUI.color = esp_Nade_Vis;
                    }
                    else
                    {
                        GUI.color = esp_Nade_NoVis;
                    }
                    float distance = Vector3.Distance(m_Camera.transform.position, trap.transform.position);
                    GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "TRAP\n" + distance.ToString("F1"));
                }

            }
        }

        private void DrawBreakerEsp(BreakerBoxSystem box)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(box.transform.position);
            GUI.color = esp_Obj_NoVis;
            if (pos.z > 0)
            {
                float distance = Vector3.Distance(m_Camera.transform.position, box.transform.position);
                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "BREAKER\n" + distance.ToString("F1"));
            }
        }

        private bool VisCheckNoPenetration(Vector3 objectPosition)
        {
            return CanHitWIthCurrentBasllistics(objectPosition, m_Camera.transform.position, 1);
        }

        private bool CanHitWIthCurrentBasllistics(Vector3 pos, Vector3 origin, int penetration)
        {
            if (penetration < 1)
            {
                return false;
            }

            Ray ray = new Ray(origin, (pos - origin).normalized);


            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, -2146565069)) //good mask!
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Hitmark>() || hitObject.GetComponentInParent<Hitmark>())
                {
                    // make sure aimbot does not aim at local player
                    if (Vector3InRange(hit.point, m_Camera.transform.position, 1.5f))
                    {
                        return false;
                    }
                    return true;
                }
                else if (hitObject.GetComponent<ZH_Hitmark>() || hitObject.GetComponent<ZH_AINav>()) //AI?
                {
                    return true;
                }
                else //some object
                {
                    SurfaceType Surface;
                    if (hitObject.GetComponent<SurfaceType>())
                    {
                        Surface = hitObject.GetComponent<SurfaceType>();
                        int surfaceCast = ((int)Surface.TypeOfSurface);

                        if ((surfaceCast == 2 || surfaceCast == 5 || surfaceCast == 6) && Surface.Penetratable)
                        {
                            Ray behindDoor = new Ray(hit.point + hit.normal, (pos - hit.point).normalized);
                            if (Physics.Raycast(behindDoor, out RaycastHit newHit, 1000f, -2146565069))
                            {
                                if (CanHitWIthCurrentBasllistics(pos, newHit.point, penetration - 1) == true)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        //type 2 is solid penatrable
                        //type 5 is glass
                        //type 6 , unknown ATM but penetreble after some shots
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private bool Vector3InRange(Vector3 point, Vector3 reference, float tolerance)
        {
            if (point.x > reference.x - tolerance && point.x < reference.x + tolerance)
            {
                if (point.y > reference.y - tolerance && point.y < reference.y + tolerance)
                {
                    if (point.z > reference.z - tolerance && point.z < reference.z + tolerance)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void Transform_ESP(Transform t, string text)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(t.position);
            if (pos.z > 0)
            {
                GUI.color = Color.white;
                float distance = Vector3.Distance(m_Camera.transform.position, t.position);
                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), text);
            }
        }

        private Vector3 W2S_MainCamera(Vector3 pos)
        {
            return m_Camera.WorldToScreenPoint(pos);
        }

        private void Vector3_ESP(Vector3 pos_, string text)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(pos_);
            if (pos.z > 0)
            {
                GUI.color = Color.white;
                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), text);
            }
        }
    }
}
