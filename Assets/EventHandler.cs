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

[System.Serializable]
public class SpawnerEvent {
    public float startTime;
    public Spawner spawner;
}

public class EventHandler : MonoBehaviour
{
    public MessageEvent[] messageEvents;
    public SpawnerEvent[] spawnerEvents;
    public float timeBeforeZoom; 
    private bool hasZoomed;

    private float gameTime;
    private DisplayText textController;
    private CameraControl camControl;

    void Start() {
        gameTime = 0;
        textController = FindObjectOfType<DisplayText>();
        camControl = FindObjectOfType<CameraControl>();
        hasZoomed = false;
    }

    void Update() {
        gameTime += Time.deltaTime;

        foreach (MessageEvent m in messageEvents) {
            if (!m.shown && m.timeUntilMessage <= gameTime) {
                m.shown = true;
                textController.ShowText(m.message, m.displayTime);
            }
        }

        foreach (SpawnerEvent s in spawnerEvents) {
            if (!s.spawner.is_active && s.startTime <= gameTime) {
                s.spawner.SetActive();
            }
        }

        if(!hasZoomed && timeBeforeZoom <= gameTime) {
            hasZoomed = true;
            camControl.SetTargetZoom();
        }
    }
}
