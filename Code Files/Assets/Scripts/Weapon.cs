using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{ 
    private InventoryAndSkills iAS;
    // For when the player attacks.
    Attack attack;

    // Variables to check whether or not a specific weapon has been crafted or equipped.
    public bool baseballBatCrafted, knifeCrafted, diamondSwordCrafted, shishkebabCrafted;
    private bool baseballBatEquipped, knifeEquipped, diamondSwordEquipped, shishkebabEquipped;
    private string itemEquipped;

    // Textboxes related to the Inventory or buttons for weapons on the Crafting UI.
    public Text guiText, textWood, textMetal, textStone, textDiamond, textRuby, textNumAnimals;
    public Text craftBaseballText, craftKnifeText, craftBlueDiamondText, craftShishkebabText, resourcesWarningText;

    // Use this for initialization
    void Start()
    {
        iAS = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
        attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();

        // If the player is greater than the 1st mission
        if (PlayerPrefs.GetInt("CurrentGameLevel") > 1)
        {
            // Restores which weapons were crafted
            if (PlayerPrefs.GetInt("BaseballBatCrafted") == 1) { baseballBatCrafted = true; craftBaseballText.text = "Equip"; }
                if (PlayerPrefs.GetInt("KnifeCrafted") == 1) { knifeCrafted = true; craftKnifeText.text = "Equip"; }
            if (PlayerPrefs.GetInt("SwordCrafted") == 1) { diamondSwordCrafted = true; craftBlueDiamondText.text = "Equip"; }
            if (PlayerPrefs.GetInt("KebabCrafted") == 1) { shishkebabCrafted = true; craftShishkebabText.text = "Equip"; }
        }
    }

    // Update is called once per frame
    void Update() {    }

    // ------------------------------------------------- CRAFTING AND EQUIPPING ITEMS ------------------------------------------------- //

    // Craft the Baseball Bat using only wood, or equip.
    public void craftBaseballBat()
    {
        if (!baseballBatCrafted) craft("Baseball Bat", 2, iAS.numMetal, 0, iAS.numStone, 0, iAS.numWood, 5, craftBaseballText);
        else equip("Baseball Bat");
    }

    // Craft the knife using metal, stone and wood, or equip.
    public void craftKnife()
    {
        if (!knifeCrafted) craft("Knife", 5, iAS.numMetal, 1, iAS.numStone, 5, iAS.numWood, 1, craftKnifeText);
        else equip("Knife");
    }

    // Craft the Diamond Sword using metal, stone and Blue Diamond, or equip.
    public void craftDiamondSword()
    {
        if (!diamondSwordCrafted) craft("Diamond Sword", 6, iAS.numMetal, 3, iAS.numStone, 5, iAS.numBlueDiamond, 3, craftBlueDiamondText);
        else equip("Diamond Sword");
    }

    // Craft the Shishkebab using metal, stone and Ruby.
    public void craftShishkebab()
    {
        if (!shishkebabCrafted) craft("Shishkebab", 10, iAS.numMetal, 3, iAS.numStone, 5, iAS.numRuby, 1, craftShishkebabText);
        else equip("Shishkebab");
    }

    // ------------------------------------------------- ATTACK USING WEAPON ------------------------------------------------- //
    public void checkEquippedWeaponAndAttack()
    {
        // If the player has no weapons equipped, attack using fists
        if (!baseballBatEquipped && !knifeEquipped && !diamondSwordEquipped && !shishkebabEquipped) attack.hit("Fist");

        // If the baseball bat is equipped, then attack using the baseball bat
        if (baseballBatEquipped) attack.hit("Baseball Bat");

        // If the knife is equipped, then attack using the knife
        if (knifeEquipped) attack.hit("Knife");

        // If the diamond sword is equipped, then attack using the diamond sword
        if (diamondSwordEquipped) attack.hit("Diamond Sword");

        // If the shishkebab is equipped, then attack using the shishkebab
        if (shishkebabEquipped) attack.hit("Shishkebab");
    }

    // ------------------------------------------------------ CRAFT ------------------------------------------------------ //
    public void craft(string itemName, int lvlNeeded, int currentRes1, int res1, int currentRes2, int res2, int currentRes3, int res3, Text button)
    {
        // At level 5/6/10 the player has the ability to craft a particular item
        if (iAS.currentLevel >= lvlNeeded)
        {
            if (currentRes1 >= res1 && currentRes2 >= res2 && currentRes3 >= res3)
            {
                // Their resources are reduced to craft the item if the item has not been crafter yet
                if (itemName.Equals("Baseball Bat"))
                {
                    // Their resources go down and the inventory is updated
                    iAS.numWood = iAS.numWood - 5;
                    textWood.text = iAS.numWood.ToString();

                    baseballBatCrafted = true; equip("Baseball Bat"); // Equip item
                }
                if (itemName.Equals("Knife"))
                {
                    // Their resources go down and the inventory is updated
                    iAS.numMetal = iAS.numMetal - 1; iAS.numStone = iAS.numStone - 5; iAS.numWood = iAS.numWood - 1;
                    textWood.text = iAS.numWood.ToString();

                    knifeCrafted = true; equip("Knife"); // Equip item
                }
                if (itemName.Equals("Diamond Sword"))
                {
                    // Their resources go down and the inventory is updated
                    iAS.numMetal = iAS.numMetal - 3; iAS.numStone = iAS.numStone - 5; iAS.numBlueDiamond = iAS.numBlueDiamond - 3;
                    textDiamond.text = iAS.numBlueDiamond.ToString();

                    diamondSwordCrafted = true; equip("Diamond Sword"); // Equip item
                }
                if (itemName.Equals("Shishkebab"))
                {
                    // Their resources go down and the inventory is updated
                    iAS.numMetal = iAS.numMetal - 3; iAS.numStone = iAS.numStone - 5; iAS.numRuby = iAS.numRuby - 1;
                    textRuby.text = iAS.numRuby.ToString();

                    shishkebabCrafted = true; equip("Shishkebab"); // Equip item
                }

                // Re-set values in inventory
                textMetal.text = iAS.numMetal.ToString(); textStone.text = iAS.numStone.ToString();

                // Instead of 'craft' the word 'equip' is displayed.
            }
            else { resourcesWarningText.text = "You do not have enough resources to craft the " + itemName + "!"; }
        }
        else { resourcesWarningText.text = "You must reach level " + lvlNeeded + " to unlock the ability to craft the " + itemName + "!"; }
    }

    // ------------------------------------------------------ EQUIP ITEMS------------------------------------------------------ //
    public void equip(string weapon)
    {
        // A list of weapons to unequip when equipping a new weapon.
        string[] weapons = new string[3];

        // If the player wants to equip a baseball bat
        if (weapon.Equals("Baseball Bat"))
        {
            // Unequip these weapons
            weapons[0] = "Knife"; weapons[1] = "DiamondSword"; weapons[2] = "Shishkebab";

            baseballBatEquipped = true; craftBaseballText.text = "Equipped"; // Equip this one
        }
        // If the player wants to equip a knife
        if (weapon.Equals("Knife"))
        {
            // Unequip these weapons
            weapons[0] = "BaseballBat"; weapons[1] = "DiamondSword"; weapons[2] = "Shishkebab";

            knifeEquipped = true;  craftKnifeText.text = "Equipped"; // Equip this one
        }
        // If the player wants to equip a Diamond Sword
        if (weapon.Equals("Diamond Sword"))
        {
            // Unequip these weapons
            weapons[0] = "BaseballBat"; weapons[1] = "Knife"; weapons[2] = "Shishkebab";

            diamondSwordEquipped = true;  craftBlueDiamondText.text = "Equipped"; // Equip this one
        }
        // If the player wants to equip a Shishkebab
        if (weapon.Equals("Shishkebab"))
        {
            // Unequip these weapons
            weapons[0] = "BaseballBat"; weapons[1] = "Knife"; weapons[2] = "DiamondSword";

            shishkebabEquipped = true;  craftShishkebabText.text = "Equipped"; // Equip this one
        }
        // Calls a method that equips the item and unquips the other items
        swapEquippedWeapons(weapons);
    }

    // ------------------------------------------------------ SWAP ITEMS------------------------------------------------------ //
    public void swapEquippedWeapons(string[] weapons)
    {
        // For each weapon
        for (int i = 0; i < weapons.Length; i++)
        {
            // If the baseball bat has been crafted, change the text on the UI to 'Equip'
            if (weapons[i].Equals("BaseballBat") && baseballBatCrafted)
            { craftBaseballText.text = "Equip"; baseballBatEquipped = false; }

            // If the knife has been crafted, change the text on the UI to 'Equip'
            if (weapons[i].Equals("Knife") && knifeCrafted)
            { craftKnifeText.text = "Equip"; knifeEquipped = false; }

            // If the diamond sword has been crafted, change the text on the UI to 'Equip'
            if (weapons[i].Equals("DiamondSword") && diamondSwordCrafted)
            { craftBlueDiamondText.text = "Equip"; diamondSwordEquipped = false; }

            // If the shishkebab has been crafted, change the text on the UI to 'Equip'
            if (weapons[i].Equals("Shishkebab") && shishkebabCrafted)
            { craftShishkebabText.text = "Equip"; shishkebabEquipped = false; }
        }
    }
}