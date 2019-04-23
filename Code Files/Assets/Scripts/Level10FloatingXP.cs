using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 26/08/2018
 */

public class Level10FloatingXP : MonoBehaviour {

    // The player's position and variables related to speed and the distance between the player and XP.
    private Transform playerPos;
    int MoveSpeed = 2, MaxDist = 5;

    // The Inventory and Skills of the player
    private InventoryAndSkills iAS;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        // Finds the current position of the player and initialises the player object.
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        iAS = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update () {
        // If the user has unlocked the ability to have XP float towards them:
        if (iAS.currentLevel > 8)
        {
            // Watch the player's location
            transform.LookAt(playerPos);

            // Rotates the enemy correctly 
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            // If the enemy is about 5 units away from the player's location, then it will start following the player
            if (Vector3.Distance(transform.position, playerPos.position) <= MaxDist)
            {
                // The XP will start floating towards the player
                transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));
            }
        }
    }
}
