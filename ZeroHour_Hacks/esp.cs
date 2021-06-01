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
        public void playerHud()
        {
            GUISkin skin2 = GUI.skin;
            GUIStyle newStyle2 = skin2.GetStyle("Label");
            newStyle2.fontSize = 36;
            GUI.color = playerHudColor;
            GUI.Label(new Rect(Screen.width - 500, Screen.height - 500, 500, 500), local_User.myWeaponManager.CurrentWeapon.AmmoLeft.ToString() +
                        "/" + local_User.myWeaponManager.CurrentWeapon.Properties.Totalammo.ToString(), newStyle2);
            newStyle2.fontSize = 14;
        }
        public void crosshairDynamic()
        {
            if (!local_User.myWeaponManager.AimState && !local_User.myWeaponManager.Aim)
            {


                RaycastHit hit;
                Ray ray = new Ray(local_User.myWeaponManager.CurrentWeapon.bulletspawn.position, local_User.myWeaponManager.CurrentWeapon.bulletspawn.forward);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default", "Attacker", "Defender", "StairCollision", "Hitmark", "AIHitmark")))
                {
                    // m_GUI.DrawCircle(w2s(hit.point), 2, 20, new Color(1, 1, 1, 0.7f), true, 2);
                    _basicESP(hit.point, "+");
                }

            }
        }
        public void drawEnemyEsp(ZH_AINav enemy)
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
                 * IKSolver.Bone[] bones = enemy.ManualReference.AimIKscript.solver.bones; //get all bones
                 */
                // ** bone info ** //

                //Distance from player
                float distance = Vector3.Distance(m_Camera.transform.position, enemy.transform.position); //get distance

                //HEADBONE Position
                IKSolver.Bone headBone = enemy.ManualReference.AimIKscript.solver.bones[4];
                Vector3 headPos = m_Camera.WorldToScreenPoint(headBone.transform.position);

                //GUI.color = esp_Default;
                Color thisColor;
                bool isVisible = isVisibleToPlayer(headPos);

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


                String info = "";
                //make string first and display it all at once
                if (esp_AI_Distance)
                {
                    info += distance.ToString("F1") + "M";
                }

                info += "\nAI_Enemy";

                if (enemy.GotArrested)
                {
                    // GUI.Label(new Rect(info_Right.x, info_Right.y-12, info_Right.x + 60, info_Right.y + 100), "*Surrendered*");
                    info += "\n*Surrendered*";
                }
                if (enemy.takenHostage)
                {
                    //GUI.Label(new Rect(info_Right.x, info_Right.y-12, info_Right.x + 60, info_Right.y + 100), "*Has Hostage*");
                    info += "\n*Has Hostage*";
                }

                if (esp_AI_Box)
                {
                    //Bounding Box
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
                    //head dot
                    GUI.Label(new Rect(headPos.x, Screen.height - headPos.y + ((boxheight / 100) * 3), 50, 50), "o");
                }
            }
        }
        public void drawPlayerEsp(UserInput a_player)
        {
            if(!esp_DeadBodies && !a_player.myHealth.alive)
            {
                return;
            }

            Vector3 pos = m_Camera.WorldToScreenPoint(a_player.Player.position);

            
            if (pos.z > 0)
            {
                Color thisColor;
#if PVT
                bool sameTeam = false;
#endif
                bool isVisible = (
             _canHit(a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position,
             m_Camera.transform.position,
                local_User.myWeaponManager.CurrentWeapon.Properties.PenetrationLimit + 5));

                if (isVisible)
                {
                    if (a_player.myLogger.TeamID == local_User.myLogger.TeamID)
                    {
                        if (!esp_Team)
                        {
                            return;
                        }
                        thisColor = esp_Team_Vis;
#if PVT
                        sameTeam = true;
#endif
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
#if PVT
                        sameTeam = true;
#endif

                    }
                    else
                    {
                        thisColor = esp_Enemy_NoVis;
                    }
                }

                GUI.color = thisColor;
                float distance = Vector3.Distance(m_Camera.transform.position, a_player.Player.position);

                Vector3 headPos = w2s(a_player.Ik_Script.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head).position);

                if (distance > 0f)
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

#if PVT
                    if (aimbot)
                    {
                        if ( (teleportBullets || isVisible) && ( !sameTeam && isAlive && (((pos.x < (Screen.width / 2) + aimbotFOV) && (Screen.height - pos.y < (Screen.height / 2) + aimbotFOV) &&
                        (pos.x > (Screen.width / 2) - aimbotFOV) && (Screen.height - pos.y > (Screen.height / 2) - aimbotFOV))
                        || ((headPos.x < (Screen.width / 2) + aimbotFOV) && (Screen.height - headPos.y < (Screen.height / 2) + aimbotFOV))
                        && ((headPos.x > (Screen.width / 2) - aimbotFOV) && (Screen.height - headPos.y > (Screen.height / 2) - aimbotFOV)))))
                        {
                            playerAimTarget = a_player;
                            GUI.Label(new Rect(headPos.x, Screen.height - headPos.y + ((boxheight / 100) * 3), 50, 50), "X");
                        }
                    }
#endif
                    if (esp_Headdot)
                    {
                        GUI.Label(new Rect(headPos.x, Screen.height - headPos.y + ((boxheight / 100) * 3), 50, 50), "o");
                    }


                    String info = "";

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
                    }
                    else
                    {
                        info += "*DEAD*\n\n";
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
                    GUI.Label(new Rect(info_Right.x, info_Right.y, info_Right.x + 60, info_Right.y + 100), info);


                    if (isAlive && esp_HPBars)
                    {
                        /* test hp bars */
                        float boxScale = boxheight / 100;

                        float hpBarThiccness = boxScale * 3;

                        Vector2 hpBarStart = new Vector2(artificialTop.x - boxHorizontaloffset - hpBarThiccness - 1, Screen.height - artificialTop.y + 5);
                        Vector2 hpBarSize = new Vector2(hpBarThiccness, (boxScale * thisPlayerHP) - 10);
                        Color hpColor = new Color(1, 1, 1, 1);

                        if (thisPlayerHP > 50)
                        {
                            hpColor.b = 0f;
                            hpColor.g = 1f;
                            hpColor.r = 1 - ((thisPlayerHP - 50) / 50);
                        }
                        else if (thisPlayerHP < 50)
                        {
                            hpColor.b = 0f;
                            hpColor.g = (thisPlayerHP - 50) / 50;
                            hpColor.r = 1f;
                        }
                        else if (thisPlayerHP == 50)
                        {
                            hpColor = Color.yellow;
                        }

                        m_GUI.DrawBoxFill(hpBarStart, hpBarSize, hpColor);
                    }

#if TESTING

#endif

                }


            }
        }
        public void objectiveEsp(ZH_AIManager.CoopObjectiveVariables Obj)
        {
            int objType = (int)Obj.ObjectiveType; //we cast the type to an int because it pulls from an Obscured Enum type, the enum name and types change with each update, the values do not.

            if (!Obj.Completed && objType != 0 && objType != 2)
            {

                Vector3 pos = m_Camera.WorldToScreenPoint(Obj.TransformReference.position);
                /*objective types:
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

                    if (isVisibleToPlayer(Obj.TransformReference.position))
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
        public void drawCivEsp(ZH_Civillian civ)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(civ.transform.position);


            if (pos.z > 0)
            {
                if (isVisibleToPlayer(civ.transform.position))
                {
                    GUI.color = esp_Obj_Vis;
                }
                else
                {
                    GUI.color = esp_Obj_NoVis;
                }

                float distance = Vector3.Distance(m_Camera.transform.position, civ.transform.position);

                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "AI_CIV\n" + distance.ToString("F1"));
            }
        }
        public void throwableESP(ThrowableSystem throwable)
        {

            if (throwable.Thrown && !throwable.Blown) //add correct radius
            {
                Vector3 pos = m_Camera.WorldToScreenPoint(throwable.transform.position);

                if (pos.z > 0)
                {
                    if (isVisibleToPlayer(throwable.transform.position))
                    {
                        GUI.color = esp_Nade_Vis;
                    }
                    else
                    {
                        GUI.color = esp_Nade_NoVis;
                    }

                    float distance = Vector3.Distance(m_Camera.transform.position, throwable.transform.position);
                    String text = "";

                    if (throwable.BlowSettings.DoDamage)
                    {
                        text = "Grenade";

                        if (throwable.stuckToWall)
                        {
                            text = "C4";
                        }

                        //draw a sphere of points or something?
                    }

                    if (throwable.BlowSettings.DoFlash || throwable.BlowSettings.DoStun)
                    {
                        text = "FlashBang";

                        //draw a line here
                    }

                    if (throwable.BlowSettings.isSpyCam)
                    {
                        text = "Spy Camera";
                    }


                    //determine throwable type

                    GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50),
                        text + " " + distance.ToString("F1") + "M\n" + throwable.TimeSettings.currentTimer.ToString("F1"));
                }
            }
        }
        public void drawTrapEsp(DoorTrapSystem trap)
        {
            if (trap.ACTIVE)
            {
                Vector3 pos = m_Camera.WorldToScreenPoint(trap.transform.position);

                if (pos.z > 0)
                {
                    if (isVisibleToPlayer(trap.transform.position))
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
        public void DrawBreakerEsp(BreakerBoxSystem box)
        {
                Vector3 pos = m_Camera.WorldToScreenPoint(box.transform.position);
            GUI.color = esp_Obj_NoVis;
            if (pos.z > 0)
                {
                    float distance = Vector3.Distance(m_Camera.transform.position, box.transform.position);
                    GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "BREAKER\n" + distance.ToString("F1"));
                }
        }
        public bool isVisibleToPlayer(Vector3 objectPosition)
        {
            if (local_User == null || local_User.CameraScript == null || local_User.CameraScript.position == null)
            {
                return false;
            }

            RaycastHit hit;

            if (Physics.Linecast(objectPosition, local_User.CameraScript.position, out hit, -2146565069))
            {
                if (vec3InRange(hit.collider.gameObject.transform.position, local_User.myWeaponManager.BlockDetector.transform.position, 0.5f) ||
                     vec3InRange(hit.collider.gameObject.transform.position, local_User.CameraScript.position, 0.5f))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool _canHit(Vector3 pos, Vector3 origin, int penetration)
        {
            if (penetration < 1)
            {
                return false;
            }

            Ray ray = new Ray(origin, (pos - origin).normalized);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000f, -2146565069)) //good mask!
            {
                GameObject hitObject = hit.collider.gameObject;
                if (hitObject.GetComponent<Hitmark>() || hitObject.GetComponentInParent<Hitmark>())
                {
                    // make sure aimbot does not aim at local player
                    if (vec3InRange(hit.point, m_Camera.transform.position, 1.5f))
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
                            RaycastHit newHit;
                            if (Physics.Raycast(behindDoor, out newHit, 1000f, -2146565069))
                            {
                                if (_canHit(pos, newHit.point, penetration - 1) == true)
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
        public bool vec3InRange(Vector3 point, Vector3 reference, float tolerance)
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
        public void basicESP(Transform t, string text)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(t.position);
            if (pos.z > 0)
            {
                GUI.color = Color.white;
                float distance = Vector3.Distance(m_Camera.transform.position, t.position);
                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), text);
            }
        }
        public Vector3 w2s(Vector3 pos)
        {
            return m_Camera.WorldToScreenPoint(pos);
        }
        public void _basicESP(Vector3 pos_, string text)
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
