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
        ZH_AIManager aiMan;
        DoorTrapManager trapMan;
        CameraRig m_cameraRig;
        Camera m_Camera;
        UserInput[] m_Users;
        UserInput local_User;
        ZH_Civillian[] m_Civs;
        DoorManager m_DoorManager;
        float updateTimer = 1f; //entity lists update
        float updateTimer_ = 10f; //entity lists update
        void playerLoop()
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
                            try
                            {
                                playerHud();
                            }
                            catch { }
                        }
                        if (general_Crosshair)
                        {
                            try
                            {
                                crosshairDynamic();
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        if (esp_Master)
                        {
                            try
                            {
                                drawPlayerEsp(a_player);
                            }
                            catch { }
                        }
                    }

                    /* grenades */
                    if (esp_Throwables)
                    {
                        foreach (ThrowableSystem throwable in a_player.myWeaponManager.ThrowableWeapon)
                        {
                            try
                            {
                                throwableESP(throwable);
                            }
                            catch { }
                        }
                    }

                }//end player loop

            }//end players
        }
        void aiLoop()
        {
            if (esp_AI_Master)
            {
                if (aiMan != null)
                {
                    if (aiMan.AliveEnemies.Count > 0)
                    {
                        try
                        {
                            foreach (ZH_AINav enemy in aiMan.AliveEnemies)
                            {
                                drawEnemyEsp(enemy);
                                if (killAll)
                                {
                                    killEnemy(enemy);
                                }
                            }
                        }
                        catch { }

                        if (killAll) { killAll = false; }
                    }
                }
            }
        }
        void objLoop()
        {
            if (esp_Objective)
            {
                //Objectives
                if (aiMan.Objectives.Length > 0)
                {
                    try
                    {
                        foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
                        {
                            objectiveEsp(Obj);

                        }
                    }
                    catch { }
                }
            }

        }
        void civLoop()
        {
            if (esp_Civs)
            {
                if (m_Civs.Length > 0)
                {
                    try
                    {
                        foreach (ZH_Civillian civ in m_Civs)
                        {
                            drawCivEsp(civ);
                        }
                    }
                    catch { }
                }
            }
        }
        void trapLoop()
        {
            if (esp_Traps)
            {
                //Door Traps
                if (trapMan.Traps.Count > 0)
                {
                    try
                    {
                        foreach (DoorTrapSystem trap in trapMan.Traps)
                        {
                            drawTrapEsp(trap);
                        }
                    }
                    catch { }
                }
            }
        }
        void breakerBoxLoop()
        {
            if (esp_Breakers)
            {
                foreach (BreakerBoxSystem box in breakers)
                {
                    try
                    {
                        DrawBreakerEsp(box);
                    }
                    catch { }
                }
            }
        }
        void populateEntityLists_fast()
        {
            updateTimer -= Time.deltaTime;
            if (updateTimer <= 0f)
            {
                try
                {
                    m_Users = FindObjectsOfType<UserInput>(); //must be first!
                }
                catch { }

                try
                {

                    if (esp_Traps)
                    {
                        trapMan = FindObjectOfType<DoorTrapManager>();
                    }
                }
                catch { }

                try
                {
                    if (m_DoorManager == null)
                    {
                        m_DoorManager = FindObjectOfType<DoorManager>();
                    }
                }
                catch { }

                updateTimer = 1f;
            }
        }
        void populateEntityLists_late()
        {
            updateTimer_ -= Time.deltaTime;
            if (updateTimer_ <= 0f)
            {
                try
                {
                    if (m_Camera == null)
                    {
                        m_Camera = Camera.main;
                    }
                }
                catch { }

                try
                {
                    if (esp_Civs)
                    {
                        m_Civs = FindObjectsOfType<ZH_Civillian>();
                    }
                }
                catch { }

                try
                {
                    if (esp_AI_Master)
                    {
                        aiMan = FindObjectOfType<ZH_AIManager>();
                    }
                }
                catch { }

                try
                {
                    if (esp_Breakers)
                    {
                        breakers = FindObjectsOfType<BreakerBoxSystem>();
                    }
                }
                catch { }

                updateTimer_ = 5f;
            }
        }

    }
}
