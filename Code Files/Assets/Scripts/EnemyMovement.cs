using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 04/08/2018
 */

public class EnemyMovement : MonoBehaviour
{
    // The position of the main character
    private Transform playerPos;
    private Attack attack;
    public AudioSource enemyDiesSound;

    // Variables for the speed or the distance between the player/enemy
    int MoveSpeed = 3, maxHealth = 25;
    public int MaxDist, numberOfHitsBeforeDeath;

    // The enemy will be blue when friendly and red when hostile.
    private SpriteRenderer mySpriteRenderer;
    public Sprite hostile, friendly, dead;

    // The enemy
    public GameObject enemy;

    // Game manager and inventory to help with game management
    public GameManager gameManager;
    private InventoryAndSkills iAS;

    // If the enemy becomes hostile once, they become suspicious and the enemy will be more likely
    // to follow the main character
    bool suspicious = false, isDead = false;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start()
    {
        // Gets components from the Player
        iAS = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryAndSkills>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        attack = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack>();

        // Gets a reference to the SpriteRenderer component on this gameObject
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update()
    {
        // If none of the menus/screens are open
        if (!gameManager.getMainMenuActive() && !gameManager.getInventoryMenuActive() && !gameManager.getCraftingMenuActive() && !iAS.currentLevelMenuActive)
        {
            if (!isDead)
            {
                // Watch the player's location
                transform.LookAt(playerPos);

                // Rotates the enemy correctly 
                transform.Rotate(new Vector3(0, -90, 0), Space.Self);

                // If the enemy is about 10 units away from the player's location, then it will start following the player
                if (Vector3.Distance(transform.position, playerPos.position) <= MaxDist)
                {
                    // The enemy becomes hostile.
                    suspicious = true;
                    mySpriteRenderer.sprite = hostile;

                    // Originally, the enemy may be guarding animals or resources. If the player gets too close, they become suspicious and
                    // they are more likely to follow the player.
                    if (MaxDist < 8) MaxDist = 8;

                    // The enemy will start following the player
                    transform.Translate(new Vector3(MoveSpeed * Time.deltaTime, 0, 0));
                }
                else
                {
                    // If the player is far from them (or has ran away successfully, they will not be hostile anymore.
                    mySpriteRenderer.sprite = friendly;
                }

                if (attack.isPunching && Vector3.Distance(transform.position, playerPos.position) <= 2) maxHealth = maxHealth - 5;
                if (attack.isBaseballing && Vector3.Distance(transform.position, playerPos.position) <= 2) maxHealth = maxHealth - 10;
                if (attack.isKnifing && Vector3.Distance(transform.position, playerPos.position) <= 2) maxHealth = maxHealth - 15;
                if (attack.isSwording && Vector3.Distance(transform.position, playerPos.position) <= 2) maxHealth = maxHealth - 20;
                if (attack.isKebabing && Vector3.Distance(transform.position, playerPos.position) <= 2) maxHealth = maxHealth - 25;

                if (maxHealth <= 0) EnemyDies();
            }
        }

    }

    public void EnemyDies()
    {
        isDead = true;
        mySpriteRenderer.sprite = dead;
        Collider2D coll2D = GetComponent<Collider2D>();
        Destroy(coll2D);
        enemyDiesSound.Play();
    }


}
