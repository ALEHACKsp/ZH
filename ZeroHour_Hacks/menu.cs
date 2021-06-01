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
        String baseInfo = "Трискит\n";
        public float menutimer = 1f;
#if !PVT
        Rect window_Solo = new Rect( 205, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
        Rect window_Coop = new Rect( 385, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 9 + m_GUI.windowHorizontalBuffer);
        Rect window_Multi = new Rect( 565, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 14 + m_GUI.windowHorizontalBuffer);
        Rect window_General = new Rect(745, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
#endif

#if PVT
        Rect window_Solo = new Rect( 210, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
        Rect window_Coop = new Rect( 400, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 9 + m_GUI.windowHorizontalBuffer);
        Rect window_Multi = new Rect( 590, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 14 + m_GUI.windowHorizontalBuffer);
        Rect window_General = new Rect( 780, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 15 + m_GUI.windowHorizontalBuffer);
        Rect window_Aimbot = new Rect(970, 35, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer);
        public static int numberOfWindows = 5;
        Rect toolbarLocation = new Rect(200, 1, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 4)) * numberOfWindows, 30);
#endif
#if !PVT
        public static int numberOfWindows = 4;
        Rect toolbarLocation = new Rect(200, 1, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 3))*numberOfWindows, 30);
        public string[]  toolBarContent = new string[] {"Solo Features","Coop / AI ESP", "Multiplayer ESP", "General Features"};
#endif
        public int toolBarCurrent = -1;

#if PVT
        public string[]  toolBarContent = new string[] {"Solo Features","Coop / AI ESP", "Multiplayer ESP", "General Features", "Aimbot"};
#endif
        public bool dockWindows = true;
        public void menu()
        {
            GUI.Label(new Rect(10, 10, 200, 100), baseInfo);
            if (showMenu)
            {
                m_GUI.setDefaultskin();

                dockWindows = GUI.Toggle(new Rect(75, 10, 100, 30), dockWindows, "Dock Windows");


                if (dockWindows)
                {
#if PVT
                    window_Solo.x = 210;
                    window_Coop.x = 400;
                    window_Multi.x = 590;
                    window_General.x = 780;
                    window_Solo.y = 35;
                    window_Coop.y = 35;
                    window_Multi.y = 35;
                    window_General.y = 35;
                    window_Aimbot.x = 970;
                    window_Aimbot.y = 35;
#elif !PVT
                    window_Solo.x = 205;
                    window_Coop.x = 385;
                    window_Multi.x = 565;
                    window_General.x = 745;
                    window_Solo.y = 35;
                    window_Coop.y = 35;
                    window_Multi.y = 35;
                    window_General.y = 35;
#endif
                }

                toolBarCurrent = GUI.Toolbar(toolbarLocation, toolBarCurrent, toolBarContent);

                switch(toolBarCurrent)
                {
                    case 0:
                        window_Solo = GUI.Window(0, window_Solo, window_SoloFunct, "Solo Only");
                        break;
                    case 1:
                        window_Coop = GUI.Window(1, window_Coop, window_CoopFunct, "Coop");
                        break;
                    case 2:
                        window_Multi = GUI.Window(2, window_Multi, window_MultiFunct, "Multiplayer");
                        break;
                    case 3:
                        window_General = GUI.Window(3, window_General, window_GeneralFunct, "General");
                        break;
                    case 4:
#if PVT
                        window_Aimbot = GUI.Window(4, window_Aimbot, window_AimbotFunct, "Aimbot - Players Only");
#endif
                        break;
                    default:
                        break;
                }
#if TESTING
                window_test = GUI.Window(5, window_test, window_TestFunct, "Test");
#endif
            }
            else
            {
                GUI.Label(new Rect(75, 10, 100, 30), "Insert for Menu");
            }
        }
        private void window_SoloFunct(int id)
        {
            m_GUI.makeButton(unlockDoors, "Unlock Doors", 1);
            m_GUI.makeButton(disarmTraps, "Disarm Traps", 2);
            m_GUI.makeButton(arrestEnemies, "Arrest Enemies", 3);
            m_GUI.makeButton(killAi, "Kill Enemies", 4);
            m_GUI.makeButton(completeObjs, "Complete Objectives", 5);
            m_GUI.makeButton(collectWeapons, "Collect Weapons", 6);
            m_GUI.makeButton(extraPoints, "Extra Points", 7);
            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }
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
            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }
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
            esp_DeadBodies = m_GUI.makeCheckbox(esp_DeadBodies, "Dead Players", 11,true,esp_Master);
            esp_Throwables = m_GUI.makeCheckbox(esp_Throwables, "Throwables", 12);
            esp_Breakers = m_GUI.makeCheckbox(esp_Breakers, "Breaker Box", 13);

            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }
        }
        private void window_GeneralFunct(int id)
        {
            general_Crosshair = m_GUI.makeCheckbox(general_Crosshair, "Dynamic Aimpoint", 1);

            general_Ammo = m_GUI.makeCheckbox(general_Ammo, "Ammo Counter", 2);

            m_GUI.makeLabel("Force NVGs", 3);
            m_GUI.makeButton(doNVG, "Toggle NVG", 4);


            noRecoil= m_GUI.makeCheckbox(noRecoil, "No Recoil", 5);
            automaticWeapons= m_GUI.makeCheckbox(automaticWeapons, "Force Full Auto", 6);
            infStamina= m_GUI.makeCheckbox(infStamina, "Infinite Stamina", 7);
#if PVT
            fireRate= m_GUI.makeCheckbox(fireRate, "Fire Rate x" + fireRate_Multiplier.currentValue.ToString("F0"), 8);
            fireRate_Multiplier.currentValue = m_GUI.makeSlider(fireRate_Multiplier.currentValue, 1, 20, 9, fireRate, true);

            bulletsPerShot= m_GUI.makeCheckbox(bulletsPerShot, "Bullets Per Shot " + bulletsPerShot_Amount.currentValue.ToString("F0"), 10);
            bulletsPerShot_Amount.currentValue = m_GUI.makeSlider(bulletsPerShot_Amount.currentValue, 1, 20, 11, bulletsPerShot, true);

            instantHit = m_GUI.makeCheckbox(instantHit, "Instant Hit", 12);

            damageHack = m_GUI.makeCheckbox(damageHack, "Damage Hack x" + damageHack_Amount_Multiplier.currentValue.ToString("F0"), 13);
            damageHack_Amount_Multiplier.currentValue = m_GUI.makeSlider(damageHack_Amount_Multiplier.currentValue, 1, 100, 14, damageHack, true);

#endif
            if (!dockWindows)
            {
                GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
            }

        }
        public void menuTimerOperation()
        {
            menutimer -= Time.deltaTime;

            if (menutimer <= 0f)
            {
                if (Input.GetKey(KeyCode.Insert))
                {
                    showMenu = !showMenu;
                    menutimer = 0.15f;
#if TESTING
                    con.WriteLine("MenuFlip");
#endif
                }
            }
        }


#if TESTING
        Rect window_test = new Rect(200, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)) + 500, m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer + 200f);
        private void window_TestFunct(int id)
        {

            test_item_1 = m_GUI.makeCheckbox(test_item_1, "test_item_", 1);
            test_item_2 = m_GUI.makeCheckbox(test_item_2, "test_item_2", 2);
            test_item_3 = m_GUI.makeCheckbox(test_item_3, "test_item_3", 3);
            test_item_4 = m_GUI.makeCheckbox(test_item_4, "test_item_4", 4);
            test_item_5 = m_GUI.makeCheckbox(test_item_5, "test_item_5", 5);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        public bool test_item_1 = false;
        public bool test_item_2 = false;
        public bool test_item_3 = false;
        public bool test_item_4 = false;
        public bool test_item_5 = false;
        public void testStuff()
        {

            if(test_item_1)
            {
            }
        }
#endif

    }
}
