using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    // GameObjects
    public GameObject player;

    // Bools
    bool isRunning = false;
    bool isInEndGame = false;

    // Floats
    public float amountToWaitForRestart = 2.0f;

    // Integers
    int score;
    public int scoreAmountPerLanding = 100;

    private void Start()
    {
        manager = this;
        ShowTitle();
    }

    IEnumerator RestartLevel(bool status)
    {
        isRunning = false;
        yield return new WaitForSeconds(amountToWaitForRestart);
        isRunning = true;

        if (status)
        {
            UIManager.ui.ToggleGoodJobText(false);
        }
        else
        {
            UIManager.ui.ToggleBadJobText(false);
        }

        PlayerController.player.gameObject.SetActive(true);
        PlayerController.player.ResetPosition();
    }

    void CommenceGame()
    {
        UIManager.ui.ClearStartEndText();
    }

    void ShowTitle()
    {
        UIManager.ui.DisplayStart();
    }

    void EndGame()
    {
        UIManager.ui.DisplayEnd(score.ToString());
        score = 0;
    }

    private void Update()
    {
        if (isRunning)
        {
            UIManager.ui.ChangeScoreText(score.ToString());
        }
        
        if (Input.GetKey(KeyCode.R) && isInEndGame)
        {
            isInEndGame = false;
            ShowTitle();
        }

        if (Input.GetKey(KeyCode.Space) && !isInEndGame && !isRunning)
        {
            isRunning = true;
            player.SetActive(true);
            CommenceGame();
        }

    }

    public void GiveLanderLandingStatus(bool status)
    {
        if (PlayerController.player.fuelLeft <= 0)
        {
            EndGame();
            isInEndGame = true;
            isRunning = false;
            return;
        }

        if (status)
        {
            UIManager.ui.ToggleGoodJobText(true);
            StartCoroutine(RestartLevel(status));
            score += scoreAmountPerLanding;
        }
        else
        {
            UIManager.ui.ToggleBadJobText(true);
            StartCoroutine(RestartLevel(status));
            PlayerController.player.fuelLeft -= 200;
        }
    }
}
