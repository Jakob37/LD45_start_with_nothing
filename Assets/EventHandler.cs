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

[System.Serializable]
public class SpawnerTimeRate {
    public float startTime;
    public int spawn_count;
    public int spawn_rate;
}

public class EventHandler : MonoBehaviour
{
    public MessageEvent[] messageEvents;
    // public SpawnerEvent[] spawnerEvents;

    public Spawner thief_spawner;
    public Spawner tomato_guy_spawner;
    public Spawner tomato_lady_spawner;

    public SpawnerTimeRate[] spawnerEventsThief;
    public SpawnerTimeRate[] spawnerEventsTomatoGuy;
    public SpawnerTimeRate[] spawnerEventsTomatoLady;

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

        UpdateSpawnerList(gameTime, thief_spawner, spawnerEventsThief);
        UpdateSpawnerList(gameTime, tomato_guy_spawner, spawnerEventsTomatoGuy);
        UpdateSpawnerList(gameTime, tomato_lady_spawner, spawnerEventsTomatoLady);

        foreach (MessageEvent m in messageEvents) {
            if (!m.shown && m.timeUntilMessage <= gameTime) {
                m.shown = true;
                textController.ShowText(m.message, m.displayTime);
            }
        }

        // foreach (SpawnerEvent s in spawnerEvents) {
        //     if (!s.spawner.is_active && s.startTime <= gameTime) {
        //         s.spawner.SetActive();
        //     }
        // }

        if(!hasZoomed && timeBeforeZoom <= gameTime) {
            hasZoomed = true;
            camControl.SetTargetZoom();
        }

        CheckEndCondition();
    }

    private void UpdateSpawnerList(float gameTime, Spawner spawner, SpawnerTimeRate[] spawner_list) {

        SpawnerTimeRate latest_event = spawner_list[0];
        for (var i = 1; i < spawner_list.Length; i++) {
            var spawn_event = spawner_list[i];
            if (spawn_event.startTime > latest_event.startTime) {
                latest_event = spawn_event;
            }
            else {
                break;
            }
        }

        spawner.SetActive();
        spawner.SetRate(latest_event.spawn_rate, latest_event.spawn_count);
    }

    private void CheckEndCondition() {
        TomatoPlant[] plants = FindObjectsOfType<TomatoPlant>();
        if ((plants == null || plants.Length == 0) && gameTime >= 60) {
            SceneManager.LoadScene("2_end", LoadSceneMode.Single);
        }
    }
}
