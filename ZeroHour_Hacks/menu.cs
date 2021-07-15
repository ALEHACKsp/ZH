using _GUI;
using UnityEngine;

namespace ZeroHour_Hacks
{

    public partial class gameObj : MonoBehaviour
    {

#if TESTING
        string baseInfo = "Zero Hax\nTESTING BUILD\n";
#else
        private string baseInfo = "Zero Hax\n";
#endif
        


        private Rect Rect_Menu = new Rect(baseHorizontalOffset - m_GUI.windowHorizontalBuffer, 0, (menuButtonOffset * 7), verticalWindowOffset - 3);
        private Rect Rect_Solo = new Rect(baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 8 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_Coop = new Rect((menuButtonOffset) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 11 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_Multi = new Rect((menuButtonOffset * 2) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 20 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_General = new Rect((menuButtonOffset * 3) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 9 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_Aimbot = new Rect((menuButtonOffset * 4) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 12 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_Meme = new Rect((menuButtonOffset * 5) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer);
        private Rect Rect_Utility = new Rect((menuButtonOffset * 6) + baseHorizontalOffset, verticalWindowOffset, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)), m_GUI.buttonHeight * 5 + m_GUI.windowHorizontalBuffer);




        private void Win_MenuFunc(int id)
        {
            show_SoloWindow     = m_GUI.MenuWindowSwitch(show_SoloWindow, "Solo", new Vector2(m_GUI.windowHorizontalBuffer, 5));
            show_CoopWindow     = m_GUI.MenuWindowSwitch(show_CoopWindow, "Coop", new Vector2(menuButtonOffset * 1 + m_GUI.windowHorizontalBuffer, 5));
            show_MultiWindow    = m_GUI.MenuWindowSwitch(show_MultiWindow, "Multiplayer", new Vector2(menuButtonOffset * 2 + m_GUI.windowHorizontalBuffer, 5));
            show_GeneralWindow  = m_GUI.MenuWindowSwitch(show_GeneralWindow, "General", new Vector2(menuButtonOffset * 3 + m_GUI.windowHorizontalBuffer, 5));
            show_AimbotWindow   = m_GUI.MenuWindowSwitch(show_AimbotWindow, "Aimbot", new Vector2(menuButtonOffset * 4 + m_GUI.windowHorizontalBuffer, 5));
            show_MemeWindow     = m_GUI.MenuWindowSwitch(show_MemeWindow, "Meme", new Vector2(menuButtonOffset * 5 + m_GUI.windowHorizontalBuffer, 5));
            show_UtilityWindow  = m_GUI.MenuWindowSwitch(show_UtilityWindow, "Utility", new Vector2(menuButtonOffset * 6 + m_GUI.windowHorizontalBuffer, 5));
        }

        private void RenderMenu()
        {

            GUI.Label(new Rect(10, 10, 200, 100), baseInfo + "\nPanic: F9 | Home To Hide All");

            GUI.Label(new Rect(75, 10, 100, 30), (showMenu ? "Insert to Hide" : "Insert for Menu"));

            if (showMenu)
            {
                m_GUI.setDefaultskin();

                Rect_Menu = GUI.Window(0, Rect_Menu, Win_MenuFunc, string.Empty);

                //Janky drop downs for aimbot options
                if (aimTargetDropDown.show)
                {
                    Rect_Aimbot.height = m_GUI.buttonHeight * 13 + m_GUI.windowHorizontalBuffer;
                }
                else if (aimKeyDropDown.show)
                {
                    Rect_Aimbot.height = m_GUI.buttonHeight * 16 + m_GUI.windowHorizontalBuffer;
                }
                else
                {
                    Rect_Aimbot.height = m_GUI.buttonHeight * 12 + m_GUI.windowHorizontalBuffer;
                }

                if (show_SoloWindow)
                {
                    Rect_Solo = GUI.Window(1, Rect_Solo, Win_SoloFunc, "Solo Only");
                }
                if (show_CoopWindow)
                {
                    Rect_Coop = GUI.Window(2, Rect_Coop, Win_CoopFunc, "Coop");
                }
                if (show_MultiWindow)
                {
                    Rect_Multi = GUI.Window(3, Rect_Multi, Win_MPFunc, "Multiplayer");
                }
                if (show_GeneralWindow)
                {
                    Rect_General = GUI.Window(4, Rect_General, Win_GeneralFunc, "General");
                }
                if (show_AimbotWindow)
                {
                    Rect_Aimbot = GUI.Window(5, Rect_Aimbot, Win_AimbotFunc, "Aimbot - Players Only");
                }

                if (show_MemeWindow)
                {
                    Rect_Meme = GUI.Window(6, Rect_Meme, Win_MemeFunct, "Meme");
                }
                if (show_UtilityWindow)
                {
                    Rect_Utility = GUI.Window(7, Rect_Utility, Win_UtilityFunc, "Utility");
                }

#if TESTING
                Rect_test = GUI.Window(8, Rect_test, Rect_TestFunct, "Test");
#endif

            }

        }//end RenderMenu

        private void Win_SoloFunc(int id)
        {
            m_GUI.makeButton(UnlockAllDoors_SP, "Unlock Doors", 1);
            m_GUI.makeButton(DisarmAllDoorTraps_SP, "Disarm Traps", 2);
            m_GUI.makeButton(ArrestAIDefenders, "Arrest Enemies", 3);
            m_GUI.makeButton(KillAIDefender, "Kill Enemies", 4);
            m_GUI.makeButton(ForceCompleteObjectives_SP, "Complete Objectives", 5);
            m_GUI.makeButton(CollectDroppedAIWeapons, "Collect Weapons", 6);
            m_GUI.makeButton(ExtraSPPoints, "Extra Points", 7);
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        private void Win_CoopFunc(int id)
        {
            m_GUI.makeLabel("AI ESP", 1);
            esp_AI_Master = m_GUI.makeCheckbox(esp_AI_Master, "Enable ESP", 2);
            esp_AI_Distance = m_GUI.makeCheckbox(esp_AI_Distance, "AI Distance", 3, true, esp_AI_Master);
            esp_AI_Box = m_GUI.makeCheckbox(esp_AI_Box, "AI Box", 4, true, esp_AI_Master);
            esp_Objective = m_GUI.makeCheckbox(esp_Objective, "Objectives", 5, true, esp_AI_Master);
            esp_Civs = m_GUI.makeCheckbox(esp_Civs, "Civillians", 6, true, esp_AI_Master);
            esp_AI_Headdot = m_GUI.makeCheckbox(esp_AI_Headdot, "Head Dot", 7, true, esp_AI_Master);
            esp_Throwables = m_GUI.makeCheckbox(esp_Throwables, "Throwables", 8);
            esp_Traps = m_GUI.makeCheckbox(esp_Traps, "Door Traps", 9, true);
            esp_Breakers = m_GUI.makeCheckbox(esp_Breakers, "Breaker Box", 10);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));

        }
        private void Win_MPFunc(int id)
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
            esp_DeadBodies = m_GUI.makeCheckbox(esp_DeadBodies, "Dead Players", 11, true, esp_Master);
            esp_Skeleton = m_GUI.makeCheckbox(esp_Skeleton, "Skeletons", 12, true, esp_Master);
            esp_HPSkeleton = m_GUI.makeCheckbox(esp_HPSkeleton, "HP Skeleton", 13, true, (esp_Master && esp_Skeleton));
            m_GUI.makeLabel("Bone Thiccness: " + Mathf.RoundToInt(skeletonThickness).ToString(), 14);
            skeletonThickness = m_GUI.makeSlider(skeletonThickness, 1, 3, 15);
            esp_Throwables = m_GUI.makeCheckbox(esp_Throwables, "Throwables", 16);
            esp_Traps = m_GUI.makeCheckbox(esp_Traps, "Door Traps", 17, true);
            esp_Breakers = m_GUI.makeCheckbox(esp_Breakers, "Breaker Box", 18);
            esp_Hostages = m_GUI.makeCheckbox(esp_Hostages, "Hostages", 19);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));

        }

        private void Win_GeneralFunc(int id)
        {
            general_Crosshair = m_GUI.makeCheckbox(general_Crosshair, "Dynamic Aimpoint", 1);

            general_Ammo = m_GUI.makeCheckbox(general_Ammo, "Ammo Counter", 2);

            m_GUI.makeLabel("Force NVGs", 3);
            m_GUI.makeButton(ForceToggleNVG, "Toggle NVG", 4);


            noRecoil = m_GUI.makeCheckbox(noRecoil, "No Recoil", 5);
            automaticWeapons = m_GUI.makeCheckbox(automaticWeapons, "Force Full Auto", 6);
            infStamina = m_GUI.makeCheckbox(infStamina, "Infinite Stamina", 7);
            noFlash = m_GUI.makeCheckbox(noFlash, "Anti-Flash", 8);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));

        }


        private void Win_AimbotFunc(int id)
        {

            aimbot = m_GUI.makeCheckbox(aimbot, "Silent Aimbot", 1);
            m_GUI.makeLabel("Aimbot FOV: " + aimbotFOV.ToString("F0"), 2);
            aimbotFOV = m_GUI.makeSlider(aimbotFOV, 10, 300, 3);
            showFOV = m_GUI.makeCheckbox(showFOV, "Show FOV", 4, true, aimbot);
            disableAimkey = m_GUI.makeCheckbox(disableAimkey, "Disable Aimkey", 5, true, aimbot);
            aimAtTeam = m_GUI.makeCheckbox(aimAtTeam, "Aim At Team", 6, true, aimbot);
            teleportBullets = m_GUI.makeCheckbox(teleportBullets, "Shoot Through Walls", 7, true, aimbot);
            m_GUI.makeLabel("Aim Target", 8);
            aimTargetDropDown.makeDropper(9);
            if (!aimTargetDropDown.show)
            {
                m_GUI.makeLabel("Aim Key", 10);
                aimKeyDropDown.makeDropper(11);
            }
            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void Win_MemeFunct(int id)
        {
            fireRate = m_GUI.makeCheckbox(fireRate, "Fire Rate Mult. x" + fireRate_Multiplier.currentValue.ToString("F1"), 1);

            fireRate_Multiplier.currentValue = m_GUI.makeSlider(fireRate_Multiplier.currentValue, 1, 5, 2, fireRate, false);

            bulletsPerShot = m_GUI.makeCheckbox(bulletsPerShot, "Bullets Mult. x" + bulletsPerShot_Amount.currentValue.ToString("F0"), 3);

            bulletsPerShot_Amount.currentValue = m_GUI.makeSlider(bulletsPerShot_Amount.currentValue, 1, 5, 4, bulletsPerShot, true);

            instantHit = m_GUI.makeCheckbox(instantHit, "Instant Hit", 5);

            damageHack = m_GUI.makeCheckbox(damageHack, "Damage Mult. x" + damageHack_Amount_Multiplier.currentValue.ToString("F0"), 6);

            damageHack_Amount_Multiplier.currentValue = m_GUI.makeSlider(damageHack_Amount_Multiplier.currentValue, 1, 10, 7, damageHack, true);

            unlimited_Traps = m_GUI.makeCheckbox(unlimited_Traps, "Unlimited Traps", 8);
            unlimited_Nades = m_GUI.makeCheckbox(unlimited_Nades, "Unlimited Nades", 9);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void Win_UtilityFunc(int id)
        {
            antiSoftBan = m_GUI.makeCheckbox(antiSoftBan, "Anti-Ban (BETA)", 1);

            m_GUI.makeLabel("Custom FOV", 2);
            customFOV = m_GUI.makeSlider(customFOV, 60, 160, 3);
            m_GUI.makeButton(ForceSetFOV, $"Set FOV to: {customFOV.ToString("F0")}", 4);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }

        private void MenuHotkeyManager()
        {
            menutimer -= Time.deltaTime;

            if (menutimer <= 0f)
            {
                if (Input.GetKey(KeyCode.Insert))
                {
                    showMenu = !showMenu;
                    menutimer = 0.25f;
                }
                if (Input.GetKey(KeyCode.Home))
                {
                    temporaryHideAll = !temporaryHideAll;
                    menutimer = 0.25f;
                }
            }
        }

    }
}
