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
        public void menu()
        {
            Rect window_Solo = new Rect(10, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
            Rect window_Coop = new Rect(10, 300, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 9 + m_GUI.windowHorizontalBuffer);
            Rect window_Multi = new Rect(10, 500, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 12 + m_GUI.windowHorizontalBuffer);
            Rect window_General = new Rect(10, 750, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 14 + m_GUI.windowHorizontalBuffer);

            m_GUI.setDefaultskin();
            showMenu = GUI.Toggle(new Rect(200, 10, 100, 30), showMenu, "Show Menu");

            GUI.Label(new Rect(10, 10, 200, 100), baseInfo);
            if (showMenu)
            {
                window_Solo = GUI.Window(0, window_Solo, window_SoloFunct, "Solo Only");
                window_Coop = GUI.Window(1, window_Coop, window_CoopFunct, "Coop");
                window_Multi = GUI.Window(2, window_Multi, window_MultiFunct, "Multiplayer");
                window_General = GUI.Window(3, window_General, window_GeneralFunct, "General");
#if TESTING
                window_test = GUI.Window(4, window_test, window_TestFunct, "Test - Aim");
#endif
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

#if PVT
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

    }
}
