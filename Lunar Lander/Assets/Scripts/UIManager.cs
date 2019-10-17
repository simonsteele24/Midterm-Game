using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    public Text horizontalText;
    public Text verticalText;
    public Text fuelText;
    public Text goodJobText;
    public Text badJobText;
    public Text scoreText;
    public Text startEndText;

    // Start is called before the first frame update
    void Awake()
    {
        ui = this;
    }

    public void ChangeVerticalSpeedText(string newText)
    {
        verticalText.text = newText;
    }

    public void ChangeHorizontalSpeedText(string newText)
    {
        horizontalText.text = newText;
    }

    public void ChangeFuelText(string newText)
    {
        fuelText.text = newText;
    }

    public void ChangeScoreText(string newText)
    {
        scoreText.text = newText;
    }

    public void ToggleGoodJobText(bool toggleValue)
    {
        goodJobText.gameObject.SetActive(toggleValue);
    }

    public void ToggleBadJobText(bool toggleValue)
    {
        badJobText.gameObject.SetActive(toggleValue);
    }

    public void DisplayEnd(string score)
    {
        startEndText.text = "Game Over. Final Score: " + score + "\n" + "Press 'R' To Restart";
    }

    public void DisplayStart()
    {
        startEndText.text = "Lunar Lander \n Press 'Space' To start";
    }

    public void ClearStartEndText()
    {
        startEndText.text = "";
    }
}
