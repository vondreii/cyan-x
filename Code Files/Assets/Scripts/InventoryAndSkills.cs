using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class InventoryAndSkills : MonoBehaviour {

    // variables for this inventory, 
    public static InventoryAndSkills iAS;
    private Weapon weapon;

    // ------ Inventory and Crafting ------ //
    
    // Variables related to resources
    public int numWood, numStone, numMetal, numBlueDiamond, numAnimalsRescued, numRuby;

    // Textboxes related to the Inventory or buttons for weapons on the Crafting UI.
    public Text guiText, textWood, textMetal, textStone, textDiamond, textRuby, textNumAnimals;
    public Text craftBaseballText, craftKnifeText, craftBlueDiamondText, craftShishkebabText, resourcesWarningText;

    // Variables to check whether or not a specific weapon has been crafted or equipped.
    private bool baseballBatCrafted, knifeCrafted, diamondSwordCrafted, shishkebabCrafted;

    // Image displayed of the new skill unlocked when levelling up 
    public Sprite healthUpgrade, XPUpgrade, KnifeUnlocked;
    public Image newSkillImageUI;
    private RectTransform rt;

    // ------ Stats ------ //

    // textboxes related to the UI for the stats menu
    public Text lvl2Checked, lvl3Checked, lvl4Checked, lvl5Checked, lvl6Checked, lvl7Checked, lvl8Checked, lvl9Checked, lvl10Checked;
    public Text XPText, lvlText, newCurrentLevelText, newSkillUnlockedText;

    // Variables related to Level
    public int XP, currentLevel, XPUntilNextLvl;

    // ------ Variables for managing game ------ //

    // Variables for managing the game
    public GameManager gm;
    public AudioSource pickupSound;
    public bool currentLevelMenuActive = false;
    private bool mainMenuActive = false;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start ()
    {
        // This is for the level upgrade page. This is the RectTranform Component of the image which allows us to change
        // The height, width, x/y coordinates of the image on the level up page.
        rt = newSkillImageUI.GetComponent<RectTransform>();

        // Weapon component of the player which allows them to attack or craft/equip
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();

        // Initially the player starts at level 1
        currentLevel = 1;

        // If the player is greater than the 1st mission
        if (PlayerPrefs.GetInt("CurrentGameLevel") > 1)
        {
            // Restores all variables to the previous level's values
            XP = PlayerPrefs.GetInt("XP");
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
            numWood = PlayerPrefs.GetInt("NumWood");
            numStone = PlayerPrefs.GetInt("NumStone");
            numMetal = PlayerPrefs.GetInt("NumMetal");
            numBlueDiamond = PlayerPrefs.GetInt("NumDiamond");
            numAnimalsRescued = PlayerPrefs.GetInt("NumAnimalsRescued");

            // Restores which weapons were crafted
            if(PlayerPrefs.GetInt("BaseballBatCrafted") == 1)  baseballBatCrafted = true;
            if (PlayerPrefs.GetInt("KnifeCrafted") == 1) knifeCrafted = true;
            if (PlayerPrefs.GetInt("SwordCrafted") == 1) diamondSwordCrafted = true;
            if (PlayerPrefs.GetInt("KebabCrafted") == 1) shishkebabCrafted = true;

            // Changes the inventory text in the menu.
            textWood.text = numWood.ToString();
            textStone.text = numStone.ToString();
            textMetal.text = numMetal.ToString();
            textDiamond.text = numBlueDiamond.ToString();
            textNumAnimals.text = numAnimalsRescued.ToString();
        }
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update () {
        // The player's level and text displays on the screen
        XPText.text = XP.ToString(); lvlText.text = currentLevel.ToString();

        // If the player presses 'M' the Main Menu will display (If the menu is already displaying, pressing 'M' will close it again).
        if (Input.GetKeyDown(KeyCode.M))
        {
            // The player cannot open the main menu again if a sub-menu page is currently open
            if(!gm.getInventoryMenuActive() && !gm.getCraftingMenuActive()) gm.mainMenu();
        }

        // The levels the player can have. The method levelUp() is called to increase the player's level:
        // first arg: the level they are leveling up to (eg, 2). second arg: The number of XP they need in order to get to that level (eg, 20xp to get to level 2)
        // third arg: The message displayed on the screen. fourth arg: the sprite of an image that will appear on the screen
        // fifth/sixth args: width and height of the picture that we want to display.

        levelUp(2, 20, "You have now unlocked the ability to craft a baseball bat", KnifeUnlocked, 180, 70);
        levelUp(3, 50, "Rescuing animals now gives you 15 XP", XPUpgrade, 60, 60);
        levelUp(4, 100, "Health is 2x stronger. You can now receive 2 hits of damage before a heart goes down ", healthUpgrade, 60, 60);
        levelUp(5, 150, "You have now unlocked the ability to craft a knife", KnifeUnlocked, 180, 70); // baseball image here 
        levelUp(6, 200, "You have now unlocked the ability to craft a Diamond Sword", KnifeUnlocked, 180, 70); // Diamond Sword image here  
        levelUp(7, 250, "Rescuing animals now gives you 20 XP", healthUpgrade, 60, 60);
        levelUp(8, 300, "You are now invincible to spikes or sharp objects", KnifeUnlocked, 180, 70); // spikes image here 
        levelUp(9, 350, "XP will float towards you when you get close", KnifeUnlocked, 180, 70); // XP image here 
        levelUp(10, 400, "You have now unlocked the ability to craft a Shishkebab", KnifeUnlocked, 180, 70); // katana image here 

        weapon.checkEquippedWeaponAndAttack();
    }

    // ------------------------------------------------------ LEVEL UP ---------------------------------------------------------- //
    private void levelUp(int newLvl, int goalXP, string newSkillUnlocked, Sprite img, int imgWidth, int imgHeight)
    {
        // If they have reached the XP needed for the next level
        if (XP >= goalXP && currentLevel < newLvl)
        {
            // Then their current level = the new level they levelled up to
            currentLevel = newLvl;
            newCurrentLevelText.text = "You have reached Level " + newLvl;

            // Opens the level up screen
            gm.TriggerLevelUpUI();
            currentLevelMenuActive = true;

            // On the UI screen, print the new skill they have unlocked, along with an image.
            newSkillUnlockedText.text = newSkillUnlocked;
            newSkillImageUI.sprite = img;
            rt.sizeDelta = new Vector2(imgWidth, imgHeight);
        }

        // If the level up screen is open and they press space
        if (currentLevelMenuActive && Input.GetKeyDown("space"))
        {
            // The level up screen is closed
            gm.TriggerLevelUpUI();
            currentLevelMenuActive = false;
        }

        // lvl2Checked, lvl3Checked.. etc is the textboxes next to the list of levels in the Stats UI.
        // If they have reached a certain level, it will be ticked in the UI. 
        if (currentLevel >= 2) { lvl2Checked.text = "X"; }
        if (currentLevel >= 3) { lvl3Checked.text = "X"; }
        if (currentLevel >= 4) { lvl4Checked.text = "X"; }
        if (currentLevel >= 5) { lvl5Checked.text = "X"; }
        if (currentLevel >= 6) { lvl6Checked.text = "X"; }
        if (currentLevel >= 7) { lvl7Checked.text = "X"; }
        if (currentLevel >= 8) { lvl8Checked.text = "X"; }
        if (currentLevel >= 9) { lvl9Checked.text = "X"; }
        if (currentLevel >= 10) { lvl10Checked.text = "X"; }

        XPUntilNextLvl = goalXP - XP;

    }

    IEnumerator ShowMessage(string message, float delay)
    {
        guiText.text = message;
        guiText.enabled = true;
        yield return new WaitForSeconds(delay);
        guiText.enabled = false;
    }

    // --------------------------------------------------- PICK UP RESOURCE ------------------------------------------------------- //
    private void pickUpItem(Collision2D coll2D, string pickUpName)
    {
        // If the player collects a wood block, it goes in their inventory.
        if (coll2D.gameObject.tag == pickUpName)
        {
            // Depending on the pickup, their number goes up by 1 and their inventory is updated (The text for the pickup)
            if (pickUpName.Equals("wood"))  { numWood++; textWood.text = numWood.ToString(); }
            if (pickUpName.Equals("stone"))  { numStone++; textStone.text = numStone.ToString(); }
            if (pickUpName.Equals("metal"))  { numMetal++; textMetal.text = numMetal.ToString(); }
            if (pickUpName.Equals("BlueDiamond"))  { numBlueDiamond++; textDiamond.text = numBlueDiamond.ToString(); }
            if (pickUpName.Equals("ruby"))  { numRuby++; textRuby.text = numRuby.ToString(); }

            // If they collected an animal, their numberOfAnimals increases too.
            if (pickUpName.Equals("AnimalToRescue"))
            {
                numAnimalsRescued++; textNumAnimals.text = numAnimalsRescued.ToString();
                // Depending on what level they are on (and which skill they unlocked) their XP will increase by 10, 15 or 20.
                if (currentLevel > 6) XP += 20;
                else if (currentLevel > 2) XP += 15;
                else XP += 10;
            }
            else XP += 1; // If it is not an animal the player collected (but another collectable) their XP goes up by 1.

            Destroy(coll2D.gameObject);
            pickupSound.Play(); // The audio sound of the player getting hit will play

            // Messages for feedback that they have collected their items.
            if(pickUpName.Equals("AnimalToRescue")) StartCoroutine(ShowMessage("You have rescued an animal", 2));
            else if (!pickUpName.Equals("pickup_XP")) StartCoroutine(ShowMessage("+1 " + pickUpName, 2));
        }
    }

    // ------------------------------------------------- IF COLLIDES WITH AN OBJECT  ---------------------------------------------------- //
    private void OnCollisionEnter2D(Collision2D coll2D)
    {
        // Checks if the player has collided with the objects with the following 'tags' 
        pickUpItem(coll2D, "pickup_XP");
        pickUpItem(coll2D, "AnimalToRescue");
        pickUpItem(coll2D, "wood");
        pickUpItem(coll2D, "stone");
        pickUpItem(coll2D, "metal");
        pickUpItem(coll2D, "BlueDiamond");
        pickUpItem(coll2D, "ruby");
    }

    public void craftWeapon(string weaponToCraft)
    {
        if (weaponToCraft.Equals("BaseballBat")) weapon.craftBaseballBat();
        if (weaponToCraft.Equals("Knife")) weapon.craftKnife();
        if (weaponToCraft.Equals("DiamondSword")) weapon.craftDiamondSword();
        if (weaponToCraft.Equals("Shishkebab")) weapon.craftShishkebab();
    }
}
