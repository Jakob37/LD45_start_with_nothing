using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnPattern {
    AllSides,
    Horizontal
}

[System.Serializable]
public class SpawnerEvent {
    public int spawn_time;
    public int spawn_number;
    public float start_time;
}

public class Spawner : MonoBehaviour
{
    public SpawnerEvent[] spawner_events;

    public int spawn_time;
    public int spawn_number;

    public GameObject spawn_prefab;

    private float current_time;
    public bool is_active;

    private SpawnPattern spawn_pattern;
    public bool spawn_sides_only;

    void Start() {
        
    }

    void Update() {
        current_time += Time.deltaTime;
        if (current_time > spawn_time && is_active) {
            SpawnUnits(spawn_prefab, spawn_number, spawn_sides_only);
            current_time -= spawn_time;
        }
    }

    private void SpawnUnits(GameObject spawn_prefab, int nbr_units, bool horizontal_sides_only) {

        for (int i = 0; i < nbr_units; i++) {
            InitializeUnit(spawn_prefab, horizontal_sides_only);
        }
    }

    private void InitializeUnit(GameObject spawn_prefab, bool horizontal_sides_only) {
        Instantiate(spawn_prefab);
        spawn_prefab.transform.position = RandomEdgePosition(horizontal_sides_only);
    }

    public static Vector2 RandomEdgePosition(bool horizontal_sides_only) {

        Vector2 left_positions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 right_positions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float padding = 0.5f;

        int side;
        List<int> choices = new List<int>();
        if (!horizontal_sides_only) {
            choices.Add(0);
            choices.Add(1);
            choices.Add(2);
            choices.Add(3);
        }
        else {
            choices.Add(1);
            choices.Add(3);
        }

        side = choices[Random.Range(0, choices.Count)];
        print("Side selected: " + side);

        float xpos = 0;
        float ypos = 0;

        if (side == 0) {
            ypos = left_positions.y - padding;
        }
        else if (side == 1) {
            xpos = right_positions.x + padding;
        }
        else if (side == 2) {
            ypos = right_positions.y + padding;
        }
        else {
            xpos = left_positions.x - padding;
        }

        if (side == 0 || side == 2) {
            xpos = UnityEngine.Random.Range(left_positions.x, right_positions.x);
        }
        else {
            ypos = UnityEngine.Random.Range(left_positions.y, right_positions.y);
        }

        return new Vector2(xpos, ypos);
    }
}
