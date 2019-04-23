using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 20/10/2018
 */

public class ButtonPushed : MonoBehaviour {

    // Variables - checks if button has been pushed, and the audio that plays when pushed. 
    public AudioSource healthDamageSound;
    public bool pushed;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        pushed = false;
    }

    // --------------------------------------------------- METHOD TO PUSH BUTTON --------------------------------------------------- //
    private void OnCollisionEnter2D(Collision2D coll2D)
    {
        // If the player collides with the button, that means the button has been pushed.
        if (coll2D.gameObject.tag == "Player")
        {
            // The button is pushed and the sound will play
            pushed = true;
            healthDamageSound.Play();

            // The button becomes red.
            SpriteRenderer sp = GetComponent<SpriteRenderer>();
            Color color = new Color(255,0,0);
            sp.color = color;
        }
    }
}
