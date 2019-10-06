using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public bool display_text;

    private Panel panel;
    private DisplayText displayText;
    private Text text;

    void Start () {
        displayText = GetComponentInChildren<DisplayText>();
        text = GetComponentInChildren<Text>();
        text.text = "text";

    }

    public void ShowText(string message) {
        text.text = message;
    }
}
