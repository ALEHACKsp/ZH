using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace ZeroHour_Hacks
{
    
    public class hackMain : MonoBehaviour
    {
        public static ConsoleWriter con = new ConsoleWriter();
        public ZH_AIManager aiMan;
        public DoorTrapManager trapMan;

        public CameraRig m_cameraRig;
        public Camera m_Camera;

        public UserInput[] m_Users;
        public UserInput local_User;

        public ZH_Civillian[] m_Civs;

        public bool arrestAll;
        public bool disableTraps;
        public bool KillAll;
        public bool completeObjs;

        public void Start()
        {
            con.WriteLine("start");
            arrestAll = false;
            disableTraps = false;
            KillAll = false;
            completeObjs = false;
        }
        public void Update()
        {
            m_Civs = FindObjectsOfType<ZH_Civillian>();
            aiMan = FindObjectOfType<ZH_AIManager>();

            con.WriteLine("Users: " + m_Users.Length.ToString() + " - Civs: " + m_Civs.Length.ToString() + " - Objs: " + aiMan.Objectives.Length.ToString() );

            if (m_Camera == null)
            {
                m_Camera = Camera.main;
            }
            if(m_cameraRig = null)
            {
                m_cameraRig = FindObjectOfType<CameraRig>();
            }
            if(trapMan == null)
            {
                trapMan = FindObjectOfType<DoorTrapManager>();
            }



        }
        public void FixedUpdate()
        {
            m_Users = FindObjectsOfType<UserInput>();
            //100hz physics update
        }

        public void OnGUI()
        {

            GUI.color = Color.white;
            GUI.Label(new Rect(10, 10, 100, 100), "ZH Hax - proton");

            //buttons
            if (GUI.Button(new Rect(10, 100, 120, 20),"Force Arrest"))
            {
                arrestAll = true;
            }
            if (GUI.Button(new Rect(10, 120, 120, 20), "Disable Traps"))
            {
                disableTraps = true;
            }
            if (GUI.Button(new Rect(10, 140, 120, 20), "Kill All"))
            {
                KillAll = true;
            }
            if (GUI.Button(new Rect(10, 160, 120, 20), "Complete Goals")) //does not includef arresting/killing/weapons pickup
            {
                completeObjs = true;
            }

            //Player ESP (Multiplayer + Co-Op)
            foreach (UserInput a_player in m_Users)
                {
                    con.WriteLine("Looping players");

                    Vector3 pos = m_Camera.WorldToScreenPoint(a_player.Player.position);

                    con.WriteLine("POS: x." + a_player.Player.position.x.ToString() + " y." + a_player.Player.position.y.ToString() + " z." + a_player.Player.position.z.ToString());

                    if (a_player.MyPhoton.IsMine)
                    {
                        local_User = a_player;
                    }

                    if (a_player.myLogger.TeamID == local_User.myLogger.TeamID)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.red;
                    }

                    float distance = Vector3.Distance(m_Camera.transform.position, a_player.Player.position);
                    
                    if (pos.z > 0)
                    {
                    con.WriteLine("is on screen");
                    //works
                    GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 100, Screen.height - pos.y + 50), a_player.myLogger.name + "\n" + distance.ToString("F1"));
                    GUI.Label(new Rect(pos.x - 50, Screen.height - pos.y, pos.x + 100, Screen.height - pos.y + 50), "HP:" + a_player.myLogger.LocalHealth.ToString("F0") );

                        if (!a_player.myHealth.alive)
                        {
                            GUI.Label(new Rect(pos.x , Screen.height - pos.y - 50, pos.x + 100, Screen.height - pos.y + 50), "*DEAD*");
                        }

                    }
                }//end player loop
            
            //Enemy AI ESP + Arrest/kill all
            if (aiMan.AliveEnemies.Count > 0)
            {
                GUI.color = Color.white;
                GUI.Label(new Rect(10, 30, 100, 100), "Alive AI: " + aiMan.AliveEnemies.Count.ToString());

                foreach (ZH_AINav enemy in aiMan.AliveEnemies)
                {
                    
                    GUI.color = new Color(1.0f,0.2f,0.2f,0.8f);

                    Vector3 pos = m_Camera.WorldToScreenPoint(enemy.transform.position);


                    float distance = Vector3.Distance(m_Camera.transform.position, enemy.transform.position);
                    if (pos.z > 0)
                    {
                        if(enemy.GotArrested)
                        {
                            GUI.Label(new Rect(pos.x, Screen.height - pos.y-50, pos.x + 20, Screen.height - pos.y + 50), "*Surrendered*");
                        }
                        
                        if (enemy.takenHostage)
                        {
                            GUI.Label(new Rect(pos.x, Screen.height - pos.y - 30, pos.x + 20, Screen.height - pos.y + 50), "*Holding Hostage*");
                        }

                        GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "AI_Enemy\n" + distance.ToString("F1"));
                    }


                    if (arrestAll) // do is holding hostage check
                    {
                        if (enemy.takenHostage) // release Hostage
                        {
                            enemy.OnFlashed();
                        }
                        if (enemy.Proned)
                        {
                            enemy.Proned = false;
                        }
                        enemy.OnArrested();
                        enemy.GotArrested = true; //makes no difference?
                        enemy.TriggerArrestAnim();
                        aiMan.ArrestedEnemies.Add(enemy);
                    }

                    if(KillAll)
                    {
                        enemy.ActiveShooter = true;
                        enemy.OnDeath();
                        enemy.enabled = false; //test
                    }
                }
                if (arrestAll) { arrestAll = false; } //reset surrender all after the full AI_Enemy loop
            }

            //Civillian ESP
            if (m_Civs.Length > 0)
            {
                GUI.color = Color.blue;
                GUI.Label(new Rect(100, 30, 100, 100), "Alive Civs: " + m_Civs.Length.ToString());

                foreach (ZH_Civillian civ in m_Civs)
                {
                    Vector3 pos = m_Camera.WorldToScreenPoint(civ.transform.position);

                    float distance = Vector3.Distance(m_Camera.transform.position, civ.transform.position);
                    if (pos.z > 0)
                    {
                        GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "AI_CIV\n" + distance.ToString("F1"));
                    }
                }
            }

            //Door Trap ESP + disable
            if (trapMan.Traps.Count > 0 )
            {
                GUI.color = Color.red;

                foreach (DoorTrapSystem trap in trapMan.Traps)
                {
                    if (trap.ACTIVE)
                    {
                        Vector3 pos = m_Camera.WorldToScreenPoint(trap.transform.position);

                        float distance = Vector3.Distance(m_Camera.transform.position, trap.transform.position);
                        if (pos.z > 0)
                        {
                            GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "TRAP\n" + distance.ToString("F1"));
                        }

                        if (disableTraps)
                        {
                            trap.ACTIVE = false;
                        }
                    }
                }
            }

            //Objective ESP + auto aomplete
            if (aiMan.Objectives.Length > 0)
            {
               
                GUI.color = Color.yellow;

                foreach (ZH_AIManager.CoopObjectiveVariables Obj in aiMan.Objectives)
                {
                    int objType = (int)Obj.ObjectiveType; //we cast the type to an int because it pulls from an Obscured Enum type, the enum name and types change with each update, the values do not.

                    if (!Obj.Completed && objType != 0)
                    {
                        Vector3 pos = m_Camera.WorldToScreenPoint(Obj.TransformReference.position);

                        // int objType = (int)Obj.ObjectiveType; //we cast the type to an int because it pulls from an Obscured Enum type, the enum name and types change with each update, the values do not.
                        /*objective types:
                         *  0 - ?
                         *  1 - Hostage / Ambassador
                         *  2 - ?
                         *  3 - ?
                         *  4 - Secure Intel
                         *  5 - Secure Document
                         */
                        float distance = Vector3.Distance(m_Camera.transform.position, Obj.TransformReference.position);
                        if (pos.z > 0)
                        {
                            if (objType == 1)
                            {
                                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "\n\n" + Obj.ObjectiveName);

                            }
                            else
                            {
                                GUI.Label(new Rect(pos.x, Screen.height - pos.y, pos.x + 20, Screen.height - pos.y + 50), "Objective\n" + distance.ToString("F1") + "\n" + Obj.ObjectiveName);
                            }
                        }

                        if(completeObjs)
                        {
                            Obj.Completed = true;
                        }
                    }
                }
            }

        }
    }
}


/*TODO
 * 
 * Door unlocker
 * secure all weapons
 * complete the enemy elimination with complete all goals (objective type == 1)
 * figure out how to "truely" arrest suspects, ziptie() does not work?
 * add anti-damage for AI enemies (ZH_AINav.damagemultiplier)
 * unlock badges
 * unlock steam achievments
 * bonus points for objectives
 * fix enemies staying spawned in
 * unlock watches?
 * stamina?
 * 
 * */