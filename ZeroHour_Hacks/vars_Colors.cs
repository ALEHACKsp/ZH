using CustomTypes;
using UnityEngine;
using _GUI;
using System;
using Microsoft.Win32;


namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        /* Menu Variables */
        private float menutimer = 1f;
        private static readonly int baseHorizontalOffset = 210;
        private static readonly int menuButtonOffset = 190;
        private static readonly int verticalWindowOffset = 40;
        private bool show_SoloWindow = true;
        private bool show_CoopWindow = true;
        private bool show_MultiWindow = true;
        private bool show_GeneralWindow = true;
        private bool show_AimbotWindow = true;
        private bool show_MemeWindow = true;
        private bool show_UtilityWindow = true;
        private bool showMenu = true;

        /* Panic / Auto-Shutdown */
        private float CDTimer = 21600; //time to unload the GameObject
        private bool panicKey = false;
        private bool panicKeyConfirm = false;
        private bool temporaryHideAll = false;

        /* General Options */
        private readonly WeaponInfo weps = new WeaponInfo();
        private string lastWeapon;
        private bool noRecoil = false;
        private bool automaticWeapons = false;
        private bool infStamina = false;
        private bool noFlash = false;

        /* Meme Options */
        private bool fireRate = false;
        private bool bulletsPerShot = false;
        private bool damageHack = false;
        private bool instantHit = false;
        private bool unlimited_Traps = false;
        private bool unlimited_Nades = false;
        private bool general_Crosshair = false;
        private bool general_Ammo = false;
        private readonly m_Types.trackedFloat bulletsPerShot_Amount = new m_Types.trackedFloat(1);
        private readonly m_Types.trackedFloat fireRate_Multiplier = new m_Types.trackedFloat(1);
        private readonly m_Types.trackedFloat damageHack_Amount_Multiplier = new m_Types.trackedFloat(1);

        /* Utility Options */
        private bool antiSoftBan = false;
        private float customFOV = 90f;

        /* ESP Colors */
        private Color PlayerHUDColor = Color.white;
        private Color esp_AI_Vis = Color.red;
        private Color esp_AI_NoVis = new Color(1, 0.6f, 0f, 0.7f);
        private Color esp_Enemy_Vis = Color.red;
        private Color esp_Enemy_NoVis = new Color(1, 0.6f, 0f, 0.7f);
        private Color esp_Nade_Vis = Color.red;
        private Color esp_Nade_NoVis = new Color(1, 1f, 0f, 0.7f);
        private Color esp_Obj_Vis = Color.green;
        private Color esp_Obj_NoVis = new Color(0, 1f, 0.7f, 0.7f);
        private Color esp_Team_Vis = new Color(0, 1f, 0.3f, 1);
        private Color esp_Team_NoVis = new Color(0, 0.7f, 1, 0.7f);
        
        /* AI ESP Options */
        private bool esp_AI_Master = false;
        private bool esp_AI_Distance = true;
        private bool esp_AI_Box = true;
        private bool esp_AI_Headdot = false;

        /* MP ESP Options */
        private bool esp_Objective = false;
        private bool esp_Traps = false;
        private bool esp_Civs = false;
        private bool esp_Hostages = false;
        private bool esp_Master = false;
        private bool esp_Team = true;
        private bool esp_Distance = true;
        private bool esp_Box = true;
        private bool esp_Headdot = false;
        private bool esp_HPBars = false;    
        private bool esp_Throwables = false;
        private bool esp_Weapon = false;
        private bool esp_Name = false;
        private bool esp_HPNums = false;
        private bool esp_DeadBodies = false;
        private bool esp_Breakers = false;
        private bool esp_Skeleton = false;
        private bool esp_HPSkeleton = false;
        private float skeletonThickness = 1f;

        /* Solo Play Options */
        private bool killAll; //this is a bool instead of a function because i'm already looping through the entity list each update, so no need to do it twice.

        /* Aimbot Options */
        private float aimbotFOV = 100f;
        private readonly float offset_BulletSpawner = 0.25f; //used to determine the offset of the bull spawn location from the target along the raycast from the weapon
        private bool showFOV = true;
        private bool aimAtTeam = false;
        private bool disableAimkey = false;
        private bool aimbot = false;
        private bool teleportBullets = false; //used as 'Shoot Through walls'
        private bool aimkeyPressed = false;
        private UserInput playerAimTarget;
        private static readonly string[] ops_Aimbot = { "Head", "Chest", "Pelvis" };
        private readonly m_GUI.dropDown aimTargetDropDown = new m_GUI.dropDown(ops_Aimbot, 150, 20);
        private static readonly string[] ops_Aimkey = { "Mouse 4", "Mouse 5", "Left Alt", "Right Mouse" };
        private readonly m_GUI.dropDown aimKeyDropDown = new m_GUI.dropDown(ops_Aimkey, 150, 20);

        /* Entities & Entity Lists */
        private ZH_AIManager m_ZH_AIManager;
        private DoorTrapManager m_DoorTrapManager;
        private CameraRig m_cameraRig;
        private Camera m_Camera;
        private UserInput[] m_Users;
        private UserInput local_User;
        private ZH_Civillian[] m_Civs;
        private DoorManager m_DoorManager;
        private BombHostageTrigger[] m_BombHostageTriggers;
        private GameNetworks m_GameNetwork;
        private GameSettings m_GameSettings;
        private BreakerBoxSystem[] m_BreakerBoxSystem;
        private float populateHumanPlayers_Timer = 1f; //entity lists update fast
        private float populateEntityLists_Timer = 10f; //entity lists update slow






        /* load & save settings */

        float settingsSaveTimer = 120f;



        private void SaveSettings()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ZH");

            key.SetValue("noRecoil", noRecoil);
            key.SetValue("automaticWeapons", automaticWeapons);
            key.SetValue("infStamina", infStamina);
            key.SetValue("noFlash", noFlash);

            key.SetValue("fireRate", fireRate);
            key.SetValue("bulletsPerShot", bulletsPerShot);
            key.SetValue("damageHack", damageHack);
            key.SetValue("instantHit", instantHit);
            key.SetValue("unlimited_Traps", unlimited_Traps);
            key.SetValue("unlimited_Nades", unlimited_Nades);
            key.SetValue("general_Crosshair", general_Crosshair);
            key.SetValue("general_Ammo", general_Ammo);

            key.SetValue("antiSoftBan", antiSoftBan);
            key.SetValue("customFOV", customFOV);

            key.SetValue("esp_AI_Master", esp_AI_Master);
            key.SetValue("esp_AI_Distance", esp_AI_Distance);
            key.SetValue("esp_AI_Box", esp_AI_Box);
            key.SetValue("esp_AI_Headdot", esp_AI_Headdot);

            key.SetValue("esp_Objective", esp_Objective);
            key.SetValue("esp_Traps", esp_Traps);
            key.SetValue("esp_Civs", esp_Civs);
            key.SetValue("esp_Hostages", esp_Hostages);
            key.SetValue("esp_Master", esp_Master);
            key.SetValue("esp_Team", esp_Team);
            key.SetValue("esp_Distance", esp_Distance);
            key.SetValue("esp_Box", esp_Box);
            key.SetValue("esp_Headdot", esp_Headdot);
            key.SetValue("esp_HPBars", esp_HPBars);
            key.SetValue("esp_AI_Box", esp_AI_Box);
            key.SetValue("esp_AI_Headdot", esp_AI_Headdot);
            key.SetValue("esp_Throwables", esp_Throwables);
            key.SetValue("esp_Weapon", esp_Weapon);
            key.SetValue("esp_Name", esp_Name);
            key.SetValue("esp_HPNums", esp_HPNums);
            key.SetValue("esp_DeadBodies", esp_DeadBodies);
            key.SetValue("esp_Breakers", esp_Breakers);
            key.SetValue("esp_Skeleton", esp_Skeleton);
            key.SetValue("esp_HPSkeleton", esp_HPSkeleton);
            key.SetValue("skeletonThickness", skeletonThickness);

            key.SetValue("aimbotFOV", aimbotFOV);
            key.SetValue("showFOV", showFOV);
            key.SetValue("aimAtTeam", aimAtTeam);
            key.SetValue("disableAimkey", disableAimkey );
            key.SetValue("aimbot", aimbot);
            key.SetValue("teleportBullets", teleportBullets);

            key.Close();
        }

        private void LoadSettings()
        {
            try
            {
                RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ZH");

                noRecoil = (string)key.GetValue("noRecoil") == "True"; // noRecoil);
                automaticWeapons = (string)key.GetValue("automaticWeapons") == "True"; // automaticWeapons);
                infStamina = (string)key.GetValue("infStamina") == "True"; // infStamina);
                noFlash = (string)key.GetValue("noFlash") == "True"; // noFlash);


                fireRate = (string)key.GetValue("fireRate") == "True"; // fireRate);
                bulletsPerShot = (string)key.GetValue("bulletsPerShot") == "True"; // bulletsPerShot);
                damageHack = (string)key.GetValue("damageHack") == "True"; // damageHack);
                instantHit = (string)key.GetValue("instantHit") == "True"; // instantHit);
                unlimited_Traps = (string)key.GetValue("unlimited_Traps") == "True"; // unlimited_Traps);
                unlimited_Nades = (string)key.GetValue("unlimited_Nades") == "True"; // unlimited_Nades);
                general_Crosshair = (string)key.GetValue("general_Crosshair") == "True"; // general_Crosshair);
                general_Ammo = (string)key.GetValue("general_Ammo") == "True"; // general_Ammo);

                antiSoftBan = (string)key.GetValue("antiSoftBan") == "True"; // antiSoftBan);
                customFOV = float.Parse(key.GetValue("customFOV").ToString()); // customFOV);

                esp_AI_Master = (string)key.GetValue("esp_AI_Master") == "True"; // esp_AI_Master);
                esp_AI_Distance = (string)key.GetValue("esp_AI_Distance") == "True"; // esp_AI_Distance);
                esp_AI_Box = (string)key.GetValue("esp_AI_Box") == "True"; // esp_AI_Box);
                esp_AI_Headdot = (string)key.GetValue("esp_AI_Headdot") == "True"; // esp_AI_Headdot);

                esp_Objective = (string)key.GetValue("esp_Objective") == "True"; // esp_Objective);
                esp_Traps = (string)key.GetValue("esp_Traps") == "True"; // esp_Traps);
                esp_Civs = (string)key.GetValue("esp_Civs") == "True"; // esp_Civs);
                esp_Hostages = (string)key.GetValue("esp_Hostages") == "True"; // esp_Hostages);
                esp_Master = (string)key.GetValue("esp_Master") == "True"; // esp_Master);
                esp_Team = (string)key.GetValue("esp_Team") == "True"; // esp_Team);
                esp_Distance = (string)key.GetValue("esp_Distance") == "True"; // esp_Distance);
                esp_Box = (string)key.GetValue("esp_Box") == "True"; // esp_Box);
                esp_Headdot = (string)key.GetValue("esp_Headdot") == "True"; // esp_Headdot);
                esp_HPBars = (string)key.GetValue("esp_HPBars") == "True"; // esp_HPBars);
                esp_AI_Box = (string)key.GetValue("esp_AI_Box") == "True"; // esp_AI_Box);
                esp_AI_Headdot = (string)key.GetValue("esp_AI_Headdot") == "True"; // esp_AI_Headdot);
                esp_Throwables = (string)key.GetValue("esp_Throwables") == "True"; // esp_Throwables);
                esp_Weapon = (string)key.GetValue("esp_Weapon") == "True"; // esp_Weapon);
                esp_Name = (string)key.GetValue("esp_Name") == "True"; // esp_Name);
                esp_HPNums = (string)key.GetValue("esp_HPNums") == "True"; // esp_HPNums);
                esp_DeadBodies = (string)key.GetValue("esp_DeadBodies") == "True"; // esp_DeadBodies);
                esp_Breakers = (string)key.GetValue("esp_Breakers") == "True"; // esp_Breakers);
                esp_Skeleton = (string)key.GetValue("esp_Skeleton") == "True"; // esp_Skeleton);
                esp_HPSkeleton = (string)key.GetValue("esp_HPSkeleton") == "True"; // esp_HPSkeleton);
                skeletonThickness = Int32.Parse(key.GetValue("skeletonThickness").ToString()); // skeletonThickness);

                aimbotFOV = float.Parse(key.GetValue("aimbotFOV").ToString()); // aimbotFOV);
                showFOV = (string)key.GetValue("showFOV") == "True"; // showFOV);
                aimAtTeam = (string)key.GetValue("aimAtTeam") == "True"; // aimAtTeam);
                disableAimkey = (string)key.GetValue("disableAimkey") == "True"; // disableAimkey);
                aimbot = (string)key.GetValue("aimbot") == "True"; // aimbot);
                teleportBullets = (string)key.GetValue("teleportBullets") == "True"; // teleportBullets);
                
                key.Close();

            }catch(Exception e)
            {
              //  con.WriteLine(e.ToString());
            }
        }

    }
}
