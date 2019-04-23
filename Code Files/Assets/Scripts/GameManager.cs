using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 28/10/2018
 */

public class GameManager : MonoBehaviour {

    // A list of the different UIs (or pages in navigation or screens) that will display to the player at different points in the game.
    public GameObject missionCompleteUI, statsUI, levelUpUI, mainMenuUI, craftingUI;
    public bool openNextLvl = false, mainMenuActive = false, inventoryMenuActive = false, craftingMenuActive = false;

    // Text for the GUI.
    public Text ScoreWhenCompleted; 

    // Variables for the Player and their stats and resources collected. 
    private Player player;
    private InventoryAndSkills iAS;
    private Weapon weapon;
    public int currentGameLevel;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    private void Start()
    {
        // Gets the components of the player and their stats etc.
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        iAS = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();

        // At the beginning, they are at level 1. 
        currentGameLevel = 1;
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update()
    {
        // String that will store the next level that will load.
        string nextLevelToLoad = "";

        if (!player.isDying)
        {     
            // Depending on what level the player is currently on, the next level will load. 
            if (SceneManager.GetActiveScene().buildIndex == 1) nextLevelToLoad = "Level2";
            if (SceneManager.GetActiveScene().buildIndex == 2) nextLevelToLoad = "Level3";

            // If we are already in level 3, the main menu will automatically load.
            if (SceneManager.GetActiveScene().buildIndex == 3)
            {
                nextLevelToLoad = "MainMenu";

                // All data about the player will be deleted. 
                PlayerPrefs.DeleteAll();       
            }

            // If the user presses space (but only on the menu screen)
            if (Input.GetKeyDown("space") && openNextLvl)
            {
                // The next level loads
                SceneManager.LoadScene(nextLevelToLoad);

                // The screen is no longer displaying and the current level increases
                openNextLvl = false;
            }

            // Otherwise, if they are on the screen and press 'X,' then they will be taken to the main menu
            if (Input.GetKeyDown(KeyCode.X) && openNextLvl)
            {
                PlayerPrefs.DeleteAll();
                SceneManager.LoadScene("MainMenu");
            }
        }

    }

    // ------------------------------------------------------- NEXT LEVEL ---------------------------------------------------------- //
    public void nextLevel()
    {
        currentGameLevel++;

        // All data such as the XP or resources collected are stored in the PlayerPrefs so that they can be used
        // during the next level. 
        PlayerPrefs.SetInt("CurrentGameLevel", currentGameLevel);
        PlayerPrefs.SetInt("XP", iAS.XP);
        PlayerPrefs.SetInt("CurrentLevel", iAS.currentLevel);
        PlayerPrefs.SetInt("NumWood", iAS.numWood);
        PlayerPrefs.SetInt("NumStone", iAS.numStone);
        PlayerPrefs.SetInt("NumMetal", iAS.numMetal);
        PlayerPrefs.SetInt("NumDiamond", iAS.numBlueDiamond);
        PlayerPrefs.SetInt("NumAnimalsRescued", iAS.numAnimalsRescued);

        if(weapon.baseballBatCrafted) PlayerPrefs.SetInt("BaseballBatCrafted", 1);
        if(weapon.knifeCrafted) PlayerPrefs.SetInt("KnifeCrafted", 1);
        if(weapon.diamondSwordCrafted) PlayerPrefs.SetInt("SwordCrafted", 1);
        if(weapon.shishkebabCrafted) PlayerPrefs.SetInt("KebabCrafted", 1);

        PlayerPrefs.SetInt("NumAnimalsRescued", iAS.numAnimalsRescued);

        // The player's score is displayed at the end of the level.
        ScoreWhenCompleted.text = "XP Collected: "+ iAS.XP +"        Number of animals rescued: " + iAS.numAnimalsRescued;

        // The GUI Screen that has the 'mission complete' message is enabled and displays on the screen
        missionCompleteUI.SetActive(true);
        openNextLvl = true;
    }

    // ------------------------------------------------------- STATS MENU -------------------------------------------------------- //
    public void openStatsMenu()
    {
        // Close the main menu and open the inventory.
        mainMenuUI.SetActive(false);
        statsUI.SetActive(true);
        inventoryMenuActive = true;
    }

    public void closeStatsMenu()
    {
        // Close the inventory and open the main menu again.
        statsUI.SetActive(false);
        mainMenuUI.SetActive(true);
        inventoryMenuActive = false;
    }

    // ----------------------------------------------------- CRAFTING MENU ------------------------------------------------------ //
    public void openCraftingMenu()
    {
        // Close the main menu and open the crafting screen.
        mainMenuUI.SetActive(false);
        craftingUI.SetActive(true);
        craftingMenuActive = true;
    }

    public void closeCraftingMenu()
    {
        // Close the crafting menu and open the main menu again.
        craftingUI.SetActive(false);
        mainMenuUI.SetActive(true);
        craftingMenuActive = false;
    }

    // ------------------------------------------------------- LEVEL UP UI -------------------------------------------------------- //
    public void TriggerLevelUpUI()
    {
        // If the level up screen is already open, close it.
        if (levelUpUI.activeSelf) levelUpUI.SetActive(false);

        // otherwise, open.
        else levelUpUI.SetActive(true);
    }

    // -------------------------------------------------- MAIN MENU (PAUSED MENU) -------------------------------------------------- //
    public void mainMenu()
    {
        // If the main menu is already open, close it.
        if (mainMenuUI.activeSelf)
        {
            mainMenuUI.SetActive(false);
            mainMenuActive = false;
        }

        // otherwise, open.
        else
        {
            mainMenuUI.SetActive(true);
            mainMenuActive = true;
        }
    }

    // ------------------------------------------- CHECKS FOR IF MENUS ARE CURRENTLY OPEN ------------------------------------------- //

    // Getters for the boolean values to check whether or not a particupar screen is open
    // This is used in other classes to prevent the player from re-opening the main menu when other menus are open
    // Or freezing the screen. 
    public bool getMainMenuActive()  { return mainMenuActive; }
    public bool getInventoryMenuActive() { return inventoryMenuActive; }
    public bool getCraftingMenuActive() { return craftingMenuActive; }


    // ------------------------------------------- CALLS METHOD TO CRAFT THE WEAPONS CHOSEN ------------------------------------------- //
    public void craftBaseballBat() { iAS.craftWeapon("BaseballBat"); }
    public void craftKnife() { iAS.craftWeapon("Knife"); }
    public void craftDiamondSword() { iAS.craftWeapon("DiamondSword"); }
    public void craftShishkebab() { iAS.craftWeapon("Shishkebab"); }


    // ---------------------------------------------------- BACK TO MAIN MENU -------------------------------------------------------- //
    public void BackToMain()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainMenu");
    }

   
}
