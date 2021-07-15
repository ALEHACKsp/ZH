using UnityEngine;
using _GUI;


namespace ZeroHour_Hacks
{
    public partial class gameObj : MonoBehaviour
    {
        private readonly string buildNo = "v1.342.0";



#if TESTING
        ConsoleWriter con = new ConsoleWriter();
#endif
        public void Start()
        {
            baseInfo += buildNo;
            killAll = false;
            weps.populateWeps();

            try
            {
                LoadSettings();
            }
            catch { }
        }

        public void Update()
        {

            PopulateHumanPlayers();
            PopulateEntityLists();
            MenuHotkeyManager();

            if (temporaryHideAll) { return; }

            AimkeyHandler();
            AntiSoftBan_Funct();

            CDTimer -= Time.deltaTime;
            if (CDTimer <= 0)
            {
                Destroy(this);
            }

            if (Input.GetKey(KeyCode.F9))
            {
                panicKey = true;
            }
            else
            {
                panicKey = false;
            }
            if (Input.GetKey(KeyCode.F10))
            {
                panicKeyConfirm = true;
            }
            else
            {
                panicKeyConfirm = false;
            }

            if (unlimited_Traps)
            {
                try
                {
                    UnlimitedTraps();
                }
                catch { }
            }
            if (unlimited_Nades)
            {
                try
                {
                    UnlimitedGrenades();
                }
                catch { }
            }

            settingsSaveTimer -= Time.deltaTime;
            if (settingsSaveTimer <= 0f)
            {
               // con.WriteLine("saved");
                SaveSettings();
                settingsSaveTimer = 120f;
            }
#if TESTING
            testStuff();
#endif
        }

        public void LateUpdate()
        {
            if (temporaryHideAll) { return; }

            AimbotController();
        }
        public void FixedUpdate()
        {
            if (temporaryHideAll) { return; }

            if (!local_User.myWeaponManager.CurrentWeapon.Properties.GunName.Contains("BALL")) //ballistic shield
            {
                bool weaponChanged = HasPlayerWeaponChanged();
                ExecuteSwitchedHack(noRecoil, ApplyNoRecoil, RemoveNoRecoil);
                ExecuteSwitchedHack(automaticWeapons, ApplyFullAutoWeapons, RemoveFullAutoWeapons);
                ExecuteSwitchedHack(infStamina, ApplyInfiniteStamina, RemoveInfiniteStamina);
                ExecuteSwitchedHack(fireRate, ApplyFireRate, RemoveFireRate);
                ExecuteSwitchedHack(bulletsPerShot, ApplyBulletsPerShot, RemoveBulletsPerShot);
                ExecuteSwitchedHack(damageHack, ApplyDamageHack, RemoveDamageHack);
                ExecuteSwitchedHack(instantHit, ApplyInstantHit, RemoveInstantHit);
            }
        }

        public void OnGUI()
        {

            if (CDTimer < 600)
            {
                //display timer
                string s = "";
                if (CDTimer < 60)
                {
                    s += CDTimer.ToString("F0") + " Seconds";
                }
                else
                {
                    s += (CDTimer / 60).ToString("F0") + " Minutes";
                }
                if (!temporaryHideAll)
                {
                    GUI.Label(new Rect(50, 100, 300, 100), s + " Until ZeroHax Unloads.\nPlease Re-Launch!");
                }
            }

            if (temporaryHideAll) { return; }

            if (panicKey)
            {
                GUI.color = UnityEngine.Color.red;
                GUI.Label(new Rect((Screen.width / 2) - 200, Screen.height / 2, 500, 30), "Panic key (F9) pressed, press F10 also to unload ZeroHax!");
                if (panicKeyConfirm)
                {
                    Destroy(this);
                }
            }

            RenderMenu();
            HumanPlayersLoop(); //you changed distance to 2.0f!
            AIDefendersLoop();
            DoorTrapsLoop();
            CivilliansLoop();
            BreakerBoxLoop();
            HostagesLoop();
            AimbotFOVHandler();
            try
            {
                ObjectivesLoop();
            }
            catch { }

#if TESTING

            //Transform_ESP(local_User.myWeaponManager.CurrentWeapon.BulletSpawner, "*");
#endif
        }

#if TESTING

        Rect Rect_test = new Rect(200, 130, (m_GUI.buttonWidth + (m_GUI.windowHorizontalBuffer * 2)) + 500, m_GUI.buttonHeight * 10 + m_GUI.windowHorizontalBuffer + 200f);
        private void Rect_TestFunct(int id)
        {


            test_item_1 = m_GUI.makeCheckbox(test_item_1, "test item 1", 1);
            test_item_2 = m_GUI.makeCheckbox(test_item_2, "test_item_2", 2);
            test_item_3 = m_GUI.makeCheckbox(test_item_3, "test_item_3", 3);
            test_item_4 = m_GUI.makeCheckbox(test_item_4, "test_item_4", 4);
            test_item_5 = m_GUI.makeCheckbox(test_item_5, "test_item_5", 5);

            GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
        }
        bool test_item_1 = false;
        bool test_item_2 = false;
        bool test_item_3 = false;
        bool test_item_4 = false;
        bool test_item_5 = false;



        Vector3 savedPosition;
        CapsuleCollider myCollider;


        void testStuff()
        {
            if (test_item_1)
            {

                if (Input.GetKey(KeyCode.Space))
                {

                    myCollider.transform.position += new Vector3(0,  0.3f, 0);
                }

            }

            if (test_item_2)
            {
                if (Input.GetKey(KeyCode.Home))
                {

                    test_item_2 = false;
                }
            }

            //PlayerPrefs.SetFloat("GameplayFOV", this.GameplayFOV)

            if(test_item_3)
            {
                if (Input.GetKey(KeyCode.PageUp))
                {
                    try
                    {
                        myCollider = local_User.GetComponent<CapsuleCollider>();  
                    }
                    catch
                    {
                        con.WriteLine("Cannot get m_GameSettings");
                    }

                    myCollider.transform.position = m_BombHostageTriggers[0].transform.position;     // savedPosition;


                }

                if (Input.GetKey(KeyCode.PageDown))
                {
                    try
                    {
                        myCollider = local_User.GetComponent<CapsuleCollider>();
                    }
                    catch
                    {
                        con.WriteLine("Cannot get m_GameSettings");
                    }

                    savedPosition = myCollider.transform.position;
                }

            }


            if(test_item_4)
            {

                SaveSettings();
                test_item_4 = false;
            }

            if (test_item_5)
            {
                LoadSettings();
                test_item_5 = false;
            }

        }
#endif


    }//end class

}//end Namespace