using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryText : MonoBehaviour
{
    private Text ui_text;

    void Awake() {
        ui_text = GetComponent<Text>();
    }

    public void SetText(string new_text) {
        ui_text.text = new_text;
    }
}
