using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class TrapdoorFunctions : MonoBehaviour {

    // The actual trapdoor object that will move across the screen
    public GameObject platform;

    // These will help determine (from the array of 2 points) where the trapdoor needs to move. 
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;

    // The button that has to be pushed in order for the trapdoor to move
    public ButtonPushed bp = new ButtonPushed();

    // The speed the platform will move
    public float moveSpeed;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        // Initialises currentPoint to first points that the trapdoor currently is in.
        currentPoint = points[pointSelection];
    }

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update () {
        // If the button is pushed
        if(bp.pushed)
            // The trapdoor will open (the object will move to the 'endpoint' or another location on the X,Y pane)
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);
    }
}
