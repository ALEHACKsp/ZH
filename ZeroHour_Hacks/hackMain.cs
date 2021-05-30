#define PRIVATE_BUILD


/// add debug tag. etcS

using RootMotion.FinalIK;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using _GUI;
using CustomTypes;




namespace ZeroHour_Hacks
{

    public class hackMain : MonoBehaviour
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
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0f)
            {

                m_Users = FindObjectsOfType<UserInput>(); //must be first!

                if (esp_Civs && m_Users.Length < 6)
                {
                    m_Civs = FindObjectsOfType<ZH_Civillian>();
                }
                if (esp_AI_Master && m_Users.Length < 6)
                {
                    aiMan = FindObjectOfType<ZH_AIManager>();
                }
                if (m_Camera == null)
                {
                    m_Camera = Camera.main;
                }
                if (esp_Traps && m_Users.Length < 6)
                {
                    trapMan = FindObjectOfType<DoorTrapManager>();
                }
                if (m_DoorManager == null)
                {
                    m_DoorManager = FindObjectOfType<DoorManager>();
                }

                if (Input.GetKeyDown(KeyCode.Insert))
                {
                    showMenu = !showMenu;
                }

                updateTimer = 5f;
            }

        }
        public void LateUpdate()
        {

        }
        public void FixedUpdate()
        {

#if PRIVATE_BUILD

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
            m_GUI.setDefaultskin();
            showMenu = GUI.Toggle(new Rect(120, 10, 100, 30), showMenu, "Show Menu");


            GUI.Label(new Rect(10, 10, 100, 100), baseInfo );
            if (showMenu)
            {
                window_Solo = GUI.Window(0, window_Solo, window_SoloFunct, "Solo Only");
                window_Coop = GUI.Window(1, window_Coop, window_CoopFunct, "Coop");
                window_Multi = GUI.Window(2, window_Multi, window_MultiFunct, "Multiplayer");
                window_General = GUI.Window(3, window_General, window_GeneralFunct, "General");
              //  window_test = GUI.Window(4, window_test, window_TestFunct, "Test");
            }
            playerLoop();
            aiLoops();
        }

        /* Windows */
        private void window_SoloFunct(int id)
        {
            m_GUI.makeButton(unlockDoors, "Unlock Doors", 1);
            m_GUI.makeButton(disarmTraps, "Disarm Traps", 2);
            m_GUI.makeButton(arrestEnemies, "Arrest Enemies", 3);
            m_GUI.makeButton(killAi, "Kill Enemies", 4);
            m_GUI.makeButton(completeObjs, "Complete Objectives", 5);
            m_GUI.makeButton(collectWeapons, "Collect Weapons", 6);
            m_GUI.makeButton(extraPoints, "Extra Points", 7);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        private void window_CoopFunct(int id)
        {
            m_GUI.makeLabel("AI ESP", 1);
            esp_AI_Master = m_GUI.makeCheckbox(esp_AI_Master, "Enable ESP", 2);
            esp_AI_Distance = m_GUI.makeCheckbox(esp_AI_Distance, "AI Distance", 3, true, esp_AI_Master);
            esp_AI_Box = m_GUI.makeCheckbox(esp_AI_Box, "AI Box", 4, true, esp_AI_Master);
            esp_Objective = m_GUI.makeCheckbox(esp_Objective, "Objectives", 5, true, esp_AI_Master);
            esp_Traps = m_GUI.makeCheckbox(esp_Traps, "Door Traps", 6, true, esp_AI_Master);
            esp_Civs = m_GUI.makeCheckbox(esp_Civs, "Civillians", 7, true, esp_AI_Master);
            esp_AI_Headdot = m_GUI.makeCheckbox(esp_AI_Headdot, "Head Dot", 8, true, esp_AI_Master);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        private void window_MultiFunct(int id)
        {
            m_GUI.makeLabel("Player ESP", 1);
            esp_Master = m_GUI.makeCheckbox(esp_Master, "Enable ESP", 2);
            esp_Team = m_GUI.makeCheckbox(esp_Team, "Show Team", 3, true, esp_Master);
            esp_Box = m_GUI.makeCheckbox(esp_Box, "Box", 4, true, esp_Master);
            esp_Distance = m_GUI.makeCheckbox(esp_Distance, "Distance", 5, true, esp_Master);
            esp_HPBars = m_GUI.makeCheckbox(esp_HPBars, "HP Bars", 6, true, esp_Master);
            esp_HPNums = m_GUI.makeCheckbox(esp_HPNums, "HP Numbers", 7, true, esp_Master);
            esp_Name = m_GUI.makeCheckbox(esp_Name, "Name", 8, true, esp_Master);
            esp_Headdot = m_GUI.makeCheckbox(esp_Headdot, "Head Marker", 9, true, esp_Master);
            esp_Weapon = m_GUI.makeCheckbox(esp_Weapon, "Weapon", 10, true, esp_Master);
            esp_Throwables = m_GUI.makeCheckbox(esp_Throwables, "Throwables", 11);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        private void window_GeneralFunct(int id)
        {
            general_Crosshair = m_GUI.makeCheckbox(general_Crosshair, "Dynamic Aimpoint", 1);

            general_Ammo = m_GUI.makeCheckbox(general_Ammo, "Ammo Counter", 2);

            m_GUI.makeLabel("Force NVGs", 3);
            m_GUI.makeButton(doNVG, "Toggle NVG", 4);

#if PRIVATE_BUILD
            noRecoil.currentValue = m_GUI.makeCheckbox(noRecoil.currentValue, "No Recoil", 5);

            fireRate.currentValue = m_GUI.makeCheckbox(fireRate.currentValue, "Fire Rate x" + fireRate_Multiplier.currentValue.ToString("F0"), 6);
            fireRate_Multiplier.currentValue = m_GUI.makeSlider(fireRate_Multiplier.currentValue, 1, 20, 7, fireRate, true);

            bulletsPerShot.currentValue = m_GUI.makeCheckbox(bulletsPerShot.currentValue, "Bullets Per Shot " + bulletsPerShot_Amount.currentValue.ToString("F0"), 8);
            bulletsPerShot_Amount.currentValue = m_GUI.makeSlider(bulletsPerShot_Amount.currentValue, 1, 20, 9, bulletsPerShot, true);

            instantHit.currentValue = m_GUI.makeCheckbox(instantHit.currentValue, "Instant Hit", 10);

            damageHack.currentValue = m_GUI.makeCheckbox(damageHack.currentValue, "Damage Hack x" + damageHack_Amount_Multiplier.currentValue.ToString("F0"), 11);
            damageHack_Amount_Multiplier.currentValue = m_GUI.makeSlider(damageHack_Amount_Multiplier.currentValue, 1, 100, 12, damageHack, true);

            automaticWeapons.currentValue = m_GUI.makeCheckbox(automaticWeapons.currentValue, "Force Full Auto", 13);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
#endif
        }
        private void window_TestFunct(int id)
        {


         //   m_GUI.makeButton(increasePlane, "planedis", 1);
         //   m_GUI.makeButton(movet, "asds", 2);

         //   newPos.x = GUI.HorizontalSlider(new Rect(10, 100, 400, 30), newPos.x, 0-(Screen.width/2), (Screen.width / 2));
         //   newPos.y = GUI.HorizontalSlider(new Rect(10, 150, 400, 30), newPos.y, 0 - (Screen.height / 2),  (Screen.height / 2));
         //   GUI.Label(new Rect(100, 150, 400, 30), "x-" + newPos.x + "y-" + newPos.y);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        /* ESP */
        public void playerLoop()
        {
            if (m_Users.Length > 0)
            {

                foreach (UserInput a_player in m_Users)
                {
                    //con.WriteLine("in a_Player loop");
                    if (a_player.MyPhoton.IsMine)
                    {
                        local_User = a_player;
                        m_cameraRig = local_User.myWeaponManager.CamScript;
                        if (general_Ammo)
                        {
                            playerHud();
                        }
                        if (general_Crosshair)
                        {
                            crosshairDynamic();
                        }
                    }
                    else
                    {
                        if (esp_Master)
                        {
                            drawPlayerEsp(a_player);
                        }
                    }

                    /* grenades */
                    if (esp_Throwables)
                    {
                        foreach (ThrowableSystem throwable in a_player.myWeaponManager.ThrowableWeapon)
                        {
                            throwableESP(throwable);
                        }
                    }

                }//end player loop

            }//end players
        }
        public void aiLoops()
        {
            //AI/COOP
            if (esp_AI_Master)
            {
                if (aiMan.AliveEnemies.Count > 0)
                {
                    foreach (ZH_AINav enemy in aiMan.AliveEnemies)
                    {
                        drawEnemyEsp(enemy);
                        if (killAll)
                        {
                            killEnemy(enemy);
                        }
                    }
                    if (killAll) { killAll = false; }
                }

                //Civillian ESP
                if (esp_Civs)
                {
                    if (m_Civs.Length > 0)
                    {
                        foreach (ZH_Civillian civ in m_Civs)
                        {
                            drawCivEsp(civ);
                        }
                    }
                }
                if (esp_Traps)
                {
                    //Door Traps
                    if (trapMan.Traps.Count > 0)
                    {
                        foreach (DoorTrapSystem trap in trapMan.Traps)
                        {
                            drawTrapEsp(trap);
                        }
                    }
                }
                if (esp_Objective)
                {
                    //Objectives
                    if (aiMan.Objectives.Length > 0)
                    {
                        foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
                        {
                            objectiveEsp(Obj);

                        }
                    }
                }

            }

        }
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

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Default","Attacker","Defender","StairCollision","Hitmark","AIHitmark")))
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
                if (isVisibleToPlayer(headBone.transform.position) || isVisibleToPlayer(enemy.transform.position))
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
                    GUI.Label(new Rect(headPos.x, Screen.height - headPos.y + ((boxheight/100)*3),50,50),"o");
                }
            }
        }
        public void drawPlayerEsp(UserInput a_player)
        {
            Vector3 pos = m_Camera.WorldToScreenPoint(a_player.Player.position);
            bool isVisible = (isVisibleToPlayer(a_player.CameraScript.position) || isVisibleToPlayer(pos));
            

            if (pos.z > 0)
            {
                Color thisColor;

                if (isVisible)
                {
                    if (a_player.myLogger.TeamID == local_User.myLogger.TeamID)
                    {
                        if(!esp_Team)
                        {
                            return;
                        }
                        thisColor = esp_Team_Vis;
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

                    }
                    else
                    {
                        thisColor = esp_Enemy_NoVis;
                    }
                }

                GUI.color = thisColor;
                float distance = Vector3.Distance(m_Camera.transform.position, a_player.Player.position);

                Vector3 hPos = m_Camera.WorldToScreenPoint(a_player.CameraScript.position);

                if (distance > 1.0f)
                {
                    Vector3 artificialTop = m_Camera.WorldToScreenPoint(a_player.CameraScript.position + (new Vector3(0f, 0.2f, 0f)));
                    Vector3 artificialBottom = m_Camera.WorldToScreenPoint(a_player.Player.position + (new Vector3(0f, 0.05f, 0f)));

                    float dotSize = 21 - ((distance - 5) / (5 - 50) * (5 - 20) + 5);

                    if (esp_Headdot)
                    {
                        m_GUI.DrawCircle(new Vector2(hPos.x, Screen.height - hPos.y), dotSize, 3, thisColor, true, 2);
                    }

                    float boxheight = Vector3.Distance(artificialTop, artificialBottom);
                    float boxWidth = boxheight / 3f;
                    float boxHorizontaloffset = boxWidth / 2f;
                    Vector2 info_Right = new Vector2(artificialTop.x + boxHorizontaloffset + 5, Screen.height - artificialTop.y + 1);

                    float thisPlayerHP = a_player.myLogger.LocalHealth;
                    bool isAlive = a_player.myHealth.alive;

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
        public bool isVisibleToPlayer(Vector3 objectPosition)
        {
            if (local_User == null || local_User.CameraScript == null || local_User.CameraScript.position == null)
            {
                return false;
            }

            RaycastHit hit;

            if (Physics.Linecast(objectPosition, local_User.CameraScript.position, out hit, LayerMask.GetMask("DoorInteractor", "DoorPlayerDetector", "StairCollision","Entity", "Attacker", "Defender", "Default")))
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
                float distance = Vector3.Distance(m_Camera.transform.position,t.position);
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
                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50),  text);
            }
        }


        /* Hacks */
#if PRIVATE_BUILD
        public bool playerWeaponChanged()
        {
            String currentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            if(lastWeapon == currentWeapon)
            {
                return false;
            }
            lastWeapon = currentWeapon;
            return true;
        }

        public void _instantHit() 
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Speed = 10000f;
        }
        public void _instantHitDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Speed = weapon.speed;
                    return;
                }
            }
        }
        public void _damageHack() 
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Damage = weapon.Damage * damageHack_Amount_Multiplier.currentValue;
                    local_User.myWeaponManager.CurrentWeapon.ex_Dmg = 0f;
                    return;
                }
            }
        }
        public void _damageHackDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Damage = weapon.Damage;
                    local_User.myWeaponManager.CurrentWeapon.ex_Dmg = weapon.ex_Dmg;
                    return;
                }
            }
        }
        public void _bulletsPerShot()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = Mathf.RoundToInt(bulletsPerShot_Amount.currentValue);
        }
        public void _bulletsPerShotDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot = weapon.BulletsPerShot;
                    return;
                }
            }
        }
        public void _fireRate()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate / fireRate_Multiplier.currentValue;
                    return;
                }
            }
        }
        public void _fireRateDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate = weapon.fireRate;
                    return;
                }
            }
        }
        public void _noRecoil()
        {
            m_cameraRig.RecoilX = 0f;
            m_cameraRig.RecoilY = 0f;
            local_User.myWeaponManager.CurrentWeapon.ex_Recoil = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray = 0f;
            local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray = 0f;
        }
        public void _noRecoilDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach(WeaponInfo.wep weapon in weaponDatas)
            {
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.ex_Recoil = weapon.ex_Recoil;
                    local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate = weapon.HeatRate;
                    local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount = weapon.recoilAmount;
                    local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime = weapon.recoilRecoverTime;
                    local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray = weapon.MaxSpray;
                    local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray = weapon.MinSpray;
                    return;
                }
            }
        }
        public void logWeaponAttributes()
        {
            /*
            con.WriteLine("[Weapon: " + local_User.myWeaponManager.CurrentWeapon.Properties.GunName + "]");
            con.WriteLine("\t[noRecoil]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.ex_Recoil: " + local_User.myWeaponManager.CurrentWeapon.ex_Recoil.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate: " + local_User.myWeaponManager.CurrentWeapon.Properties.HeatRate.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount: " + local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime: " + local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray: " + local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray: " + local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray.ToString() + "]");
            con.WriteLine("\t[makeAutomatic]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.Automatic: " + local_User.myWeaponManager.CurrentWeapon.Properties.Automatic.ToString() + "]");
            con.WriteLine("\t[fireRate]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.fireRate: " + local_User.myWeaponManager.CurrentWeapon.Properties.fireRate.ToString() + "]");
            con.WriteLine("\t[BulletsPerShot]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot: " + local_User.myWeaponManager.CurrentWeapon.Properties.BulletsPerShot.ToString() + "]");
            con.WriteLine("\t[Damage]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.Damage: " + local_User.myWeaponManager.CurrentWeapon.Properties.Damage.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.ex_Dmg: " + local_User.myWeaponManager.CurrentWeapon.ex_Dmg.ToString() + "]");
            con.WriteLine("\t\t[local_User.myWeaponManager.CurrentWeapon.Properties.Speed: " + local_User.myWeaponManager.CurrentWeapon.Properties.Speed.ToString() + "]");
            con.WriteLine("[END: " + local_User.myWeaponManager.CurrentWeapon.Properties.GunName + "]\n");
            */
        }
        public void _automaticWeapons()
        {
            local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = true;
        }
        public void _automaticWeaponsDisable()
        {
            String curentWeapon = local_User.myWeaponManager.CurrentWeapon.Properties.GunName;
            foreach (WeaponInfo.wep weapon in weaponDatas)
            {
               
                if (curentWeapon == weapon.name)
                {
                    local_User.myWeaponManager.CurrentWeapon.Properties.Automatic = weapon.Automatic;
                    return;
                }
            }
        }
#endif
        public void killAi()
        {
            killAll = true;
            return;
        }
        public void unlockDoors()
        {
            foreach (InteractiveDoorSystem door in m_DoorManager.Doors)
            {
                door.Locked = false;
            }
        }
        public void killEnemy(ZH_AINav enemy)
        {
            enemy.Surrendering = false;
            enemy.ActiveShooter = true;
            enemy.OnDeath();
            enemy.enabled = false;
        }
        public void arrestEnemies()
        {
            foreach (ZH_AINav enemy in aiMan.AliveEnemies)
            {
                if (enemy.takenHostage) // release Hostage
                {
                    enemy.OnFlashed();
                }
                if (enemy.Proned)
                {
                    enemy.Proned = false;
                }
                if (!enemy.isObjective)
                {
                    enemy.OnArrested();
                    enemy.GotArrested = true; //makes no difference?
                    enemy.TriggerArrestAnim();
                    aiMan.ArrestedEnemies.Add(enemy);
                }
            }
        }
        public void completeObjs()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
            {
                Obj.Completed = true;
            }
        }
        public void disarmTraps()
        {
            foreach (DoorTrapSystem trap in trapMan.Traps)
            {
                trap.ACTIVE = false;
            }
        }
        public void collectWeapons()
        {
            aiMan.SecuredWeapons = aiMan.SpawnedEnemies.Count;
        }
        public void extraPoints()
        {
            foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
            {
                if (Obj.ExtraPoints > 0)
                {
                    Obj.ExtraPoints *= 2;
                }
                else
                {
                    Obj.ExtraPoints = 20;
                }
            }
        }
        public void doNVG()
        {
            GameSettings[] settings = FindObjectsOfType<GameSettings>();
            foreach (GameSettings set in settings)
            {
                set.ANVGScript.On = !set.ANVGScript.On;
            }
        }


      //  public static ConsoleWriter     con = new ConsoleWriter();
        public ZH_AIManager             aiMan;
        public DoorTrapManager          trapMan;
        public CameraRig                m_cameraRig;
        public Camera                   m_Camera;
        public UserInput[]              m_Users;
        public UserInput                local_User;
        public ZH_Civillian[]           m_Civs;
        public DoorManager              m_DoorManager;

        public bool killAll;

        public float updateTimer = 5f;
        Color esp_AI_Vis        =   Color.red;
        Color esp_AI_NoVis      =   new Color(1, 0.6f, 0f, 0.7f);

        Color esp_Enemy_Vis     =   Color.red;
        Color esp_Enemy_NoVis   =   new Color(1, 0.6f, 0f, 0.7f);

        Color esp_Nade_Vis      =   Color.red;
        Color esp_Nade_NoVis    =   new Color(1, 1f, 0f, 0.7f);

        Color esp_Obj_Vis       =   Color.green;
        Color esp_Obj_NoVis     =   new Color(0, 1f, 0.7f, 0.7f);

        Color esp_Team_Vis      =   new Color(0, 1f, 0.3f, 1);
        Color esp_Team_NoVis    =   new Color(0, 0.7f, 1, 0.7f);

        Rect window_Solo = new Rect(10, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
        Rect window_Coop = new Rect(10, 300, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 9 + m_GUI.windowHorizontalBuffer);
        Rect window_Multi = new Rect(10, 500, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 12 + m_GUI.windowHorizontalBuffer);
        Rect window_General = new Rect(10, 750, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 14 + m_GUI.windowHorizontalBuffer);

        //test stuff//
        WeaponInfo weps = new WeaponInfo();
        List<WeaponInfo.wep> weaponDatas = new List<WeaponInfo.wep>();
        
        public void populateWeps()
        {
            weaponDatas.Add(weps.m17);
            weaponDatas.Add(weps.falcon);
            weaponDatas.Add(weps.sr762);
            weaponDatas.Add(weps.m4);
            weaponDatas.Add(weps.kyanite);
            weaponDatas.Add(weps.es36);
            weaponDatas.Add(weps.rattler);
            weaponDatas.Add(weps.oppressor);
            weaponDatas.Add(weps.shotgun);
            weaponDatas.Add(weps.mac10);
            weaponDatas.Add(weps.shield);
        }
        
        Rect window_test = new Rect(200, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer );
#if PRIVATE_BUILD
        public m_Types.trackedBool noRecoil = new m_Types.trackedBool(false);
        public m_Types.trackedBool fireRate = new m_Types.trackedBool(false);
        public m_Types.trackedBool bulletsPerShot = new m_Types.trackedBool(false);
        public m_Types.trackedBool damageHack = new m_Types.trackedBool(false);
        public m_Types.trackedBool instantHit = new m_Types.trackedBool(false);
        public m_Types.trackedBool automaticWeapons = new m_Types.trackedBool(false);
        public m_Types.trackedFloat bulletsPerShot_Amount = new m_Types.trackedFloat(1);
        public m_Types.trackedFloat fireRate_Multiplier = new m_Types.trackedFloat(1);
        public m_Types.trackedFloat damageHack_Amount_Multiplier = new m_Types.trackedFloat(1);
        String lastWeapon;
#endif
        /////////////

        public bool esp_AI_Master = false;
        public bool esp_AI_Distance = false;
        public bool esp_AI_Box = false;
        public bool esp_AI_Headdot = false;
        public bool esp_Objective = false;
        public bool esp_Traps = false;
        public bool esp_Civs = false;

        public bool esp_Master = false;
        public bool esp_Team = false;
        public bool esp_Distance = false;
        public bool esp_Box = false;
        public bool esp_Headdot = false;
        public bool esp_HPBars = false;
        public bool esp_Throwables = false;
        public bool esp_Weapon = false;
        public bool esp_Name = false;
        public bool esp_HPNums = false;

        public bool general_Crosshair = false;
        public bool general_Ammo = false;
        public bool showMenu = true;

        String baseInfo = "Трискит | proton\n";

        Color playerHudColor = Color.white;

    }//end class

}//end Namespace


/*  todo
 * maybe send RPC requests for killall/etc?
 * make stuff turn off.
 * 
 * 
 * 
 * 
 * 
 * SO_Weapons
 *  local_User.myWeaponManager.CurrentWeapon.Properties.MaxSpray
    local_User.myWeaponManager.CurrentWeapon.Properties.MinSpray

    local_User.myWeaponManager.CurrentWeapon.Properties.StaminaDrain
    local_User.myWeaponManager.CurrentWeapon.Properties.WeaponMovementSway
    local_User.myWeaponManager.CurrentWeapon.Properties.MaxSwaySpeed
    local_User.myWeaponManager.CurrentWeapon.Properties.PenetrationLimit
    local_User.myWeaponManager.CurrentWeapon.Properties.fireRate
    local_User.myWeaponManager.CurrentWeapon.Properties.recoilAmount
    local_User.myWeaponManager.CurrentWeapon.Properties.recoilRecoverTime
    local_User.myWeaponManager.CurrentWeapon.Properties.defaultVal          //something to do with spread

    local_User.myWeaponManager.CurrentWeapon.Properties.ADS = 0f; //ads speed?
    public float WeaponInertiaADS = 8.75f;


    weaponmanager:
	// Token: 0x040008D3 RID: 2259
	public float WeaponMovementSpeed = 1f;

	// Token: 0x040008D4 RID: 2260
	public float WeaponAimSpeed = 1f;


	[HideInInspector]
	public float ex_Recoil;

	// Token: 0x040008C4 RID: 2244
	[HideInInspector]
	public float ex_Dmg;

	// Token: 0x040008C5 RID: 2245
	[HideInInspector]
	public float ex_Spread;*/