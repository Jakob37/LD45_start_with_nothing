using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class MessageEvent {
    public float timeUntilMessage;
    public float displayTime;
    public bool shown = false;
    public string message;
}

public class EventHandler : MonoBehaviour
{
    public MessageEvent[] messageEvents;

    public float timeUntilThiefSpawnStart;

    private float gameTime;
    private DisplayText textController;

    void Start() {
        gameTime = 0;
        textController = FindObjectOfType<DisplayText>();
    }

    void Update() {
        gameTime += Time.deltaTime;

        foreach (MessageEvent m in messageEvents) {
            if (!m.shown && m.timeUntilMessage <= gameTime) {
                m.shown = true;
                textController.ShowText(m.message, m.displayTime);
            }
        }
    }
}
