using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DisplayText : MonoBehaviour
{
    public bool showText;
    private float displayTime;

    private Panel panel;
    private DisplayText textDisplayer;
    private Text text;
    private Message message;

    void Start () {
        message = GetComponentInChildren<Message>();
        text = message.GetComponentInChildren<Text>();
        text.text = "text";
        showText = false;
        message.gameObject.SetActive(showText);
    }

    public void ShowText(string message, float displayTime) {
        text.text = message;
        showText = true;
        this.displayTime = displayTime;
        this.message.gameObject.SetActive(showText);
    }

    public void Update() {
        if (showText) {
            displayTime -= Time.deltaTime;
        }

        if (showText && displayTime <= 0) {
            showText = false;
            message.gameObject.SetActive(showText);
        }
    }

}
