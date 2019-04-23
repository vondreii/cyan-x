using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 20/08/2018
 */

public class CameraMovement : MonoBehaviour {

    // Variable for the player
    private GameObject player;

    // Variable that will store the min/max x/y of where the camera can move until.
    public float xMin, xMax, yMin, yMax;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        // Finds the player object
        player = GameObject.FindGameObjectWithTag("Player");
	}

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update () {

        // Gets the x position of the player.
        float playerX = player.transform.position.x;

        // Clamp means that the camera will be 'clamped' onto the x position of the player object (so, it will follow the x pos
        // of the player, where the player's x position is). unless it reaches the min/max of the x position that is allowed for the camera.
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);

        // This camera will keep changing the position to match the x position of the player object
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
