using UnityEngine;


namespace ZeroHour_Hacks
{

    public partial class gameObj : MonoBehaviour
    {
        private void HumanPlayersLoop()
        {
            if (m_Users.Length > 0)
            {

                foreach (UserInput a_player in m_Users)
                {
                    if (a_player.MyPhoton.IsMine)
                    {
                        local_User = a_player;

                        if (general_Ammo)
                        {
                            try
                            {
                                RenderPlayerHUD();
                            }
                            catch { }
                        }
                        if (general_Crosshair)
                        {
                            try
                            {
                                RenderDynamicCrosshair();
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
                                RenderPlayerESP(a_player);
                            }
                            catch { }
                        }
                    }


                    foreach (ThrowableSystem throwable in a_player.myWeaponManager.ThrowableWeapon)
                    {
                        if (esp_Throwables)
                        {
                            try
                            {
                                RenderThrowableESP(throwable);
                            }
                            catch { }
                        }

                        try
                        {
                            if (noFlash)
                            {
                                throwable.BlowSettings.DoFlash = false;
                                throwable.BlowSettings.DoStun = false;
                            }
                        }
                        catch { }
                    }

                }//end player loop

            }//end players
        }

        private void AIDefendersLoop()
        {
            if (esp_AI_Master)
            {
                if (m_ZH_AIManager != null)
                {
                    if (m_ZH_AIManager.AliveEnemies.Count > 0)
                    {
                        try
                        {
                            foreach (ZH_AINav enemy in m_ZH_AIManager.AliveEnemies)
                            {
                                RenderAIDefenderESP(enemy);
                                if (killAll)
                                {
                                    KillAIEntity(enemy);
                                }
                            }
                        }
                        catch { }

                        if (killAll) { killAll = false; }
                    }
                }
            }
        }

        private void ObjectivesLoop()
        {
            if (esp_Objective)
            {
                //Objectives
                if (m_ZH_AIManager.Objectives.Length > 0)
                {
                    try
                    {
                        foreach (ZH_AIManager.CoopObjectiveVariables Obj in m_ZH_AIManager.Objectives)
                        {
                            RenderObjectiveESP_SP(Obj);

                        }
                    }
                    catch { }
                }
            }

        }

        private void CivilliansLoop()
        {
            if (esp_Civs)
            {
                if (m_Civs.Length > 0)
                {
                    try
                    {
                        foreach (ZH_Civillian civ in m_Civs)
                        {
                            RenderCivillianESP(civ.transform);
                        }
                    }
                    catch { }
                }
            }
        }

        private void HostagesLoop()
        {
            if (esp_Hostages)
            {
                if (m_BombHostageTriggers.Length > 0)
                {

                    foreach (BombHostageTrigger hostage in m_BombHostageTriggers)
                    {
                        try
                        {
                            RenderCivillianESP(hostage.transform);
                        }
                        catch { }
                    }

                }
            }
        }

        private void DoorTrapsLoop()
        {
            if (esp_Traps)
            {
                //Door Traps
                if (m_DoorTrapManager.Traps.Count > 0)
                {
                    try
                    {
                        foreach (DoorTrapSystem trap in m_DoorTrapManager.Traps)
                        {
                            RenderTrapESP(trap);
                        }
                    }
                    catch { }
                }
            }
        }

        private void BreakerBoxLoop()
        {
            if (esp_Breakers)
            {
                foreach (BreakerBoxSystem box in m_BreakerBoxSystem)
                {
                    try
                    {
                        DrawBreakerEsp(box);
                    }
                    catch { }
                }
            }
        }

        private void PopulateHumanPlayers()
        {
            populateHumanPlayers_Timer -= Time.deltaTime;
            if (populateHumanPlayers_Timer <= 0f)
            {
                try
                {
                    m_Users = FindObjectsOfType<UserInput>(); //must be first!
                }
                catch { }

                populateHumanPlayers_Timer = 1f;
            }
        }

        private void PopulateEntityLists()
        {
            populateEntityLists_Timer -= Time.deltaTime;
            if (populateEntityLists_Timer <= 0f)
            {
                try
                {
                    m_Camera = Camera.main;
                }
                catch { }

                try
                {

                    m_DoorTrapManager = FindObjectOfType<DoorTrapManager>();
                }
                catch { }

                try
                {

                    m_DoorManager = FindObjectOfType<DoorManager>();
                }
                catch { }

                try
                {
                    m_Civs = FindObjectsOfType<ZH_Civillian>();
                }
                catch { }

                try
                {
                    m_ZH_AIManager = FindObjectOfType<ZH_AIManager>();
                }
                catch { }

                try
                {
                    m_BreakerBoxSystem = FindObjectsOfType<BreakerBoxSystem>();
                }
                catch { }

                try
                {
                    m_BombHostageTriggers = FindObjectsOfType<BombHostageTrigger>();
                }
                catch { }


                try
                {
                    m_GameNetwork = FindObjectOfType<GameNetworks>();
                }
                catch { }

                try
                {
                    m_GameSettings = FindObjectOfType<GameSettings>();
                }
                catch { }

                populateEntityLists_Timer = 5f;
            }
        }

    }
}
