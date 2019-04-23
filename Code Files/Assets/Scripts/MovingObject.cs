using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 28/08/2018
 */

public class MovingObject : MonoBehaviour {

    // The actual platform object that will move across the screen
    public GameObject platform;

    // The speed the platform will move
    public float moveSpeed;

    // These will help determine (from the array of 2 points) where the platform needs to move. 
    public Transform currentPoint;
    public Transform[] points;
    public int pointSelection;

    // --------------------------------------------------------- START ------------------------------------------------------------- //
    void Start () {
        // Gets the current point based on the array of the possible points the platform can move to
        // This is configured in the Unity Inspector window. There are 2 points listed in the array, the start point and the end point. 
        // The moving platform will move forwards and backwards between these two points. 
        currentPoint = points[pointSelection];
	}

    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    void Update ()
    {
        // Gets the current position of the moving platform and moves it to the new position (platform only moves to the end once). 
        platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        // If the position of the platform is the same as the current point it has to move towards 
        if(platform.transform.position == currentPoint.position)
        {         
            // Goes to the next point of the array (This is where the platform will move to next).
            pointSelection++;

            // There are 2 points in the point selection. 
            // Once it gets to the end of the arry, it will start again from the beginning of the array.
            if (pointSelection == points.Length) pointSelection = 0;

            // Re-assigned the next point we want to go to
            currentPoint = points[pointSelection];
        }
    }
}
