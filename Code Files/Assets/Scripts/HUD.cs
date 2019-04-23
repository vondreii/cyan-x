using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class HUD : MonoBehaviour {

    // Array to store the different states of the health (full health, empty health) etc. 
    public Sprite[] heartSprites;

    // The UI HUD where the information will display
    public static HUD hud;

    // Sprite sheet of hearts has its own image for each state 
    public Image heartUI;
    public Text PointsDisplay;
    
    // Stores inventory and skills information about the player
    private InventoryAndSkills playerXP;

    // Player variable to store info about the player (eg, where it is currently located).
    private Player player;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        // Finds the current position of the player and initialises the player object.
        playerXP = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update ()
    {
        // The heart sprites will change if there is any changes in the health 
        heartUI.sprite = heartSprites[player.currentHealth];

        // The points will change if points of the player changes.
        PointsDisplay.text = "Press [M] to toggle the menu\nXP: " + playerXP.XP.ToString() + "\n Level: " + playerXP.currentLevel.ToString();
    }
}
