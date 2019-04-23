using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class Player : MonoBehaviour {

    // Variables for the player/movement/RigibBody etc
    public GameObject player;
    public static Player p;
    private float speed = 5f;

    // To store components of the player (such as skills etc).
    private Rigidbody2D characterRB;
    private InventoryAndSkills iAS;
   
    // Variables for jumping
    private Vector2 jump;
    private int numOfJumpsMade = 0;

    // Variables for health
    public int maxJumps = 1, currentHealth, numberOfHitsBeforeDamage = 1;
    private int maxHealth = 4, countNumHits = 0;
    public bool isDying = false;

    // Variables for sound
    public AudioSource jumpSound, healthDamageSound;

    // For managing the game/scenes or UI.
    public GameManager gameManager;


    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start ()
    {
        // When the game starts, the health is automatically full.
        currentHealth = maxHealth;

        // Finds the current position of the player and initialises the player object.
        iAS = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
        characterRB = GetComponent<Rigidbody2D>();
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update ()
    {
        // If none of the menus/screens are open
        if (!gameManager.getMainMenuActive() && !gameManager.getInventoryMenuActive() && !gameManager.getCraftingMenuActive() && !iAS.currentLevelMenuActive)
        {
            // Allows the player to move either left/right on the horizontal axis.
            transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, 0f);

            // Allows the player to jump. The player cannot double jump, and this determines whether or not the player is currently attached to the ground.
            if (Input.GetKeyDown("space") && numOfJumpsMade == 0)
            {
                // If they are on the ground, and the space button is pressed, then the player jumps. 
                jump = new Vector2(0.0f, 450.0f);
                characterRB.AddForce(jump);

                // The audio of the player jumping will play
                jumpSound.Play();

                // While the numOfJumpsMade > 0, the player cannot press the space bar and jump again while in mid-air. 
                numOfJumpsMade++;
            }
        
            // Does not allow the health of the player to exceed the maximum health. 
            if (currentHealth > maxHealth) currentHealth = maxHealth;

            // If the player has levelled up to level 4, they can take twice as much damage.
            if (iAS.currentLevel > 3) numberOfHitsBeforeDamage = 2;           

            // If the player's health reaches 0, or if they fall off the ledge/platform or (if the y axis is less than where they should be) then the player dies
            if (currentHealth <= 0) die();
            if (player.transform.position.y <= -4) die();
        }
    }

    // ---------------------------------------------- WHEN COLLIDING WITH AN OBJECT -------------------------------------------------- //
    private void OnCollisionEnter2D(Collision2D coll2D)
    {
        // ---------- COLLIDING WITH GROUND ---------- //

        // This stops the player from jumping more than once. When they are on the ground, they have not made any jumps.
        // Whereas, if they are not in contact with the ground, then they are currently jumping. 
        if (coll2D.gameObject.tag == "ground")
        {
            numOfJumpsMade = 0;
            isDying = false;
        }

        // ---------- COLLIDING WITH SPIKES ---------- //

        // If the player touches the cactus, their health goes down by 1. 
        if (coll2D.gameObject.tag == "spike")
        {
            // If the player has reached level 7, they have unlocked the ability to be invincible to spikes. 
            if (iAS.currentLevel < 7)
            {
                // Before that, their health will go down by 1. 
                countNumHits++;
                healthDamageSound.Play();  

                // If they have unclocked health x2, they need to be hit twice for the health to go down.
                if (countNumHits >= numberOfHitsBeforeDamage)
                {
                    currentHealth -= 1;  countNumHits = 0;
                }   
            }
        }

        // ---------- COLLIDING WITH APPLES ---------- //

        // If the player picks up an apple, their health go up by 1.
        if (coll2D.gameObject.tag == "apple")
        {
            // If they don't have full health, then they can pick up an apple.
            if (currentHealth < 4)
            {
                currentHealth++;
                Destroy(coll2D.gameObject);
            }
            //pickupSound.Play(); // The audio sound of the player getting hit will play
        }

        // ---------- COLLIDING WITH ENEMY ---------- //

        // If the player touches the cactus, their health goes down by 1. 
        if (coll2D.gameObject.tag == "enemy")
        {
            countNumHits++;
            healthDamageSound.Play();

            // If they have unclocked health x2, they need to be hit twice for the health to go down.
            if (countNumHits >= numberOfHitsBeforeDamage)
            {
                currentHealth -= 1;    
                countNumHits = 0;
            } 
        }

        // ------- COLLIDING WITH FINISH DOOR ------- //

        // If the player touches the cactus, their health goes down by 1. 
        if (coll2D.gameObject.tag == "Finish")
        {
            gameManager.nextLevel();
        }

    }

    // ---------------------------------------------------------- DIE -------------------------------------------------------------- //
    void die()
    {
        // Variables are set
        Boolean reloadCheckpoint = false; isDying = true; countNumHits = 0;

        // Variable for the current x and y of the player. 
        float currentXPos = player.transform.position.x;
        float currentYPos = player.transform.position.y;

        // If the x position is past 40, then the player will be loaded at the checkpoint
        if (currentXPos > 40) reloadCheckpoint = true;        

        // If they need to reload at the checkpoint:
        if (reloadCheckpoint)
        {
            // *** https://answers.unity.com/questions/188998/transformposition.html *** //

            // The new position of the player will be changed to the checkpoint
            Vector3 temp = transform.position; 

            // Change x coordinate
            temp.x = 52.0f; transform.position = temp;

            // Change y coordinate
            temp.y = -2f; transform.position = temp;
        }
        else
        {
            // Otherwise, just start from the beginning again: 
            Vector3 temp = transform.position;

            // Change x coordinate
            temp.x = -97.0f; transform.position = temp;

            // Change y coordinate
            temp.y = -2f; transform.position = temp;
        }
        currentHealth = maxHealth; // health is reset 
    }
}
