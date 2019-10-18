using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Instances
    public static GameManager manager;

    // GameObjects
    public GameObject player;

    // Bools
    public bool isRunning = false;
    bool isInEndGame = false;

    // Floats
    public float amountToWaitForRestart = 2.0f;

    // Integers
    int score;
    public int scoreAmountPerLanding = 100;

    // Constant strings
    const string TITLE_TEXT = "Lunar Lander \n Press 'Space' To start";
    const string GAME_OVER_TEXT = "Game Over. Final Score: ";
    const string RESTART_TEXT = "Press 'R' To Restart";


    private void Start()
    {
        // Initialize singleton
        manager = this;

        // display the title
        ShowTitle();
    }





    // This function waits a certain amount of time then restarts the level
    IEnumerator RestartLevel(bool status)
    {
        // End running, wait x amount of seconds, then resume
        isRunning = false;
        yield return new WaitForSeconds(amountToWaitForRestart);
        isRunning = true;

        // Did the lander not crash?
        if (status)
        {
            // If yes, then disable the good job text
            UIManager.ui.ToggleTextObject(UIManager.GOOD_NAME, false);
        }
        else
        {
            // If no, then disable the good job text
            UIManager.ui.ToggleTextObject(UIManager.BAD_NAME, false);
        }

        // Add player back into game and reset it's transformation
        PlayerController.player.gameObject.SetActive(true);
        PlayerController.player.ResetPosition();
    }





    // This function gets rid of all text before starting the game
    void CommenceGame()
    {
        UIManager.ui.ChangeText(UIManager.STARTEND_NAME, "");
    }





    // This function shows the title for the game when called
    void ShowTitle()
    {
        UIManager.ui.ChangeText(UIManager.STARTEND_NAME, TITLE_TEXT);
    }





    // This function resets all necessary functions/values and then displays game over screen
    void EndGame()
    {
        UIManager.ui.ChangeText(UIManager.STARTEND_NAME, GAME_OVER_TEXT + score.ToString() + "\n" + RESTART_TEXT);
        score = 0;
    }





    private void Update()
    {

        // Is the game currently in play?
        if (isRunning)
        {
            // If yes, then display the proper score
            UIManager.ui.ChangeText(UIManager.SCORE_NAME, score.ToString());
        }

        // Has the 'R' key been pressed and we are in game over?
        if (Input.GetKey(KeyCode.R) && isInEndGame)
        {
            // If yes, then display title
            isInEndGame = false;
            ShowTitle();
        }

        // Has the 'Space' key been pressed and the game is in its title phase?
        if (Input.GetKey(KeyCode.Space) && !isInEndGame && !isRunning)
        {
            // If yes, then initialize and start up the game
            isRunning = true;
            player.SetActive(true);
            PlayerController.player.ResetPosition();
            CommenceGame();
        }

    }





    // This function processes the landing status for the lander and then determine what to do next
    public void ProcessLanderLandingStatus(bool status)
    {
        // Set the run state to false
        isRunning = false;

        // Has the lander landed correctly?
        if (status)
        {
            // If yes, has the game reached its endstate?
            if (CheckForEndState())
            {
                // If yes, then exit the function
                return;
            }

            // If no, reset the level
            UIManager.ui.ToggleTextObject(UIManager.GOOD_NAME, true);
            StartCoroutine(RestartLevel(status));
            score += scoreAmountPerLanding;
        }
        else
        {
            // If no, subtract from the overal fuel total 
            PlayerController.player.fuelLeft -= 20;

            // If yes, has the game reached its endstate?
            if (CheckForEndState())
            {
                // If yes, then exit the function
                return;
            }

            // If no, reset the level
            UIManager.ui.ToggleTextObject(UIManager.BAD_NAME, true);
            StartCoroutine(RestartLevel(status));
        }
    }





    // This function checks if the game has reached its end state()
    bool CheckForEndState()
    {
        // Has the player run out of fuel?
        if (PlayerController.player.fuelLeft < 0)
        {
            // If yes, then end the game
            EndGame();
            isInEndGame = true;
            isRunning = false;

            // Clear player from screen
            PlayerController.player.gameObject.SetActive(false);

            // Report to rest of class
            return true;
        }

        // If no, return with nothing
        return false;
    }
}
