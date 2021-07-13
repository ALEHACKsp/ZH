using CustomTypes;
using UnityEngine;
using _GUI;

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

    }
}
