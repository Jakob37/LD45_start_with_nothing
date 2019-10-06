using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    private Text ui_text;

    void Awake() {
        ui_text = GetComponent<Text>();
        int score = Score.Tomatoes;
        SetText("Big Tomato win again! You collected " + score + " tomatoes");
    }

    public void SetText(string new_text) {
        ui_text.text = new_text;
    }
}
