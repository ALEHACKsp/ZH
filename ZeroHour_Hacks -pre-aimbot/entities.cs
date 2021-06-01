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
        public ZH_AIManager aiMan;
        public DoorTrapManager trapMan;
        public CameraRig m_cameraRig;
        public Camera m_Camera;
        public UserInput[] m_Users;
        public UserInput local_User;
        public ZH_Civillian[] m_Civs;
        public DoorManager m_DoorManager;
        public float updateTimer = 5f; //entity lists update
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
            if (aiMan != null)
            {
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

        }
        public void populateEntityLists()
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
    }
}
