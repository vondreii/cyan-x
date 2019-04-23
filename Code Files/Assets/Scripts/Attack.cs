using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 27/10/2018
 */

public class Attack : MonoBehaviour {

    public Text guiText;
    private int numberOfHitsTillEnemyDies;
    public Animator animator;
    public bool isPunching, isBaseballing, isKnifing, isSwording, isKebabing;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // --------------------------------------------------- METHOD TO HIT ENEMY --------------------------------------------------- //
    public void hit(string weapon)
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            // The player will hit the enemy with whichever weapon is currently equipped.

            if (weapon.Equals("Fist"))
            {
                
                numberOfHitsTillEnemyDies = 5;
                animator.SetBool("IsPunching", true);
                // Code for when no weapon has been crafted or equipped yet
                isPunching = true;
            }

            if (weapon.Equals("Baseball Bat"))
            {
                numberOfHitsTillEnemyDies = 4;
                animator.SetBool("IsBaseballing", true);
                // Code for when the baseball bat has been equipped 
                isBaseballing = true;
            }

            if (weapon.Equals("Knife"))
            {
                numberOfHitsTillEnemyDies = 3;
                animator.SetBool("IsKniving", true);
                // Code for when the knife has been equipped 
                isKnifing = true;
            }

            if (weapon.Equals("Diamond Sword"))
            {
                numberOfHitsTillEnemyDies = 2;
                animator.SetBool("IsSwording", true);
                // Code for when the diamond sword has been equipped 
                isSwording = true;
            }

            if (weapon.Equals("Shishkebab"))
            {
                numberOfHitsTillEnemyDies = 1;
                animator.SetBool("IsKebabing", true);
                // Code for when the shishkebab has been equipped 
                isKebabing = true;
            }

        }

        if (Input.GetKeyUp(KeyCode.Z)) 
        {
            isPunching = false; isBaseballing = false; isKnifing = false; isSwording = false; isKebabing = false;
            animator.SetBool("IsPunching", false);
            animator.SetBool("IsBaseballing", false);
            animator.SetBool("IsKniving", false);
            animator.SetBool("IsSwording", false);
            animator.SetBool("IsKebabing", false);
        }

    }

}
