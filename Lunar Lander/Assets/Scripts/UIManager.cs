using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager ui;
    public Text horizontalText;
    public Text verticalText;

    // Start is called before the first frame update
    void Awake()
    {
        ui = this;
    }

    public void ChangeVerticalSpeedText(string newText)
    {
        horizontalText.text = newText;
    }

    public void ChangeHorizontalSpeedText(string newText)
    {
        horizontalText.text = newText;
    }
}
