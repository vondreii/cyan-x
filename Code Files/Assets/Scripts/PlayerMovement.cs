using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class PlayerMovement : MonoBehaviour {

    // Variables declared for player animation and movement
    public CharacterController2D controller;
    public Animator animator;
    float horizontalMove = 0f;

    // For helping the moving functionalities.
    bool jump = false;
    public float runSpeed = 900f;

    // Managing game/levels etc.
    public GameManager gm;

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update () {
        // If none of the menus are open
        if (!gm.getMainMenuActive() && !gm.getInventoryMenuActive() && !gm.getCraftingMenuActive())
        {
            // Allows the player to move either left/right on the horizontal axis - the animator will change as this is happening.
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

            // if jump button pressed, will be true
            if (Input.GetButtonDown("Jump")) {
                jump = true;
                animator.SetBool("IsJumping",true);
            }

          

        }
	}

    public void OnLanding ()
    {
        animator.SetBool("IsJumping", false);
    }

    // ------------------------------------------------------- FIXED UPDATE ----------------------------------------------------------- //
    private void FixedUpdate()
	{   
        // allows character to move && variables for jumping functionality set
        controller.Move(horizontalMove * Time.fixedDeltaTime,false, jump);
        jump = false;
	}

}
