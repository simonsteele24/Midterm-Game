using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Singleton Instances
    public static UIManager ui;

    // Constant strings
    public const string HORIZONTAL_NAME = "Horizontal";
    public const string VERTICAL_NAME = "Vertical";
    public const string FUEL_NAME = "Fuel";
    public const string GOOD_NAME = "Good";
    public const string BAD_NAME = "Bad";
    public const string SCORE_NAME = "Score";
    public const string STARTEND_NAME = "StartEnd";

    // Texts
    public Text horizontalText;
    public Text verticalText;
    public Text fuelText;
    public Text goodJobText;
    public Text badJobText;
    public Text scoreText;
    public Text startEndText;

    // Dictionaries
    Dictionary<string, Text> texts;





    // Start is called before the first frame update
    void Awake()
    {
        // Initialize singleton instance
        ui = this;

        // Initialize text dictionary
        texts = new Dictionary<string, Text>();
        texts.Add(HORIZONTAL_NAME, horizontalText);
        texts.Add(VERTICAL_NAME, verticalText);
        texts.Add(FUEL_NAME, fuelText);
        texts.Add(GOOD_NAME, goodJobText);
        texts.Add(BAD_NAME, badJobText);
        texts.Add(SCORE_NAME, scoreText);
        texts.Add(STARTEND_NAME, startEndText);

    }





    // This function changes a text (given by a key) to a given value
    public void ChangeText(string key, string newText)
    {
        texts[key].text = newText;
    }





    // This function toggles the good job text
    public void ToggleTextObject(string key, bool toggleValue)
    {
        texts[key].gameObject.SetActive(toggleValue);
    }
}
