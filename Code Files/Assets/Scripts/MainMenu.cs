using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* INFT3960 - Games Production
 * Assignment 2 Player Movement Prototype
 * Authors: Sharlene Von Drehnen and Sora Khan
 * Date Modified: 07/09/2018
 */

public class MainMenu : MonoBehaviour
{
    // --------------------------------------------------------- UPDATE ------------------------------------------------------------- //
    private void Update()
    {
        // If on the main screen, and the player presses 'X', the game will start.
        if(Input.GetKeyDown(KeyCode.X)) { PlayGame(); }

        // To exit the game
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
  }

    // ---------------------------------------------------------- PLAY -------------------------------------------------------------- //
    public void PlayGame()
    { 
        // The first scene (Level 1) will load.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // -------------------------------------------------------- QUIT GAME ------------------------------------------------------------ //
    public void QuitGame()
    {
        // Quits the game.
        Application.Quit();
    }




}