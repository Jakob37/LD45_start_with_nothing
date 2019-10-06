using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum SpawnPattern {
    AllSides,
    Horizontal
}

public class Spawner : MonoBehaviour
{
    public int spawn_time;
    public int spawn_number;

    public GameObject spawn_prefab;
    public SpawnerTimeRate[] spawn_times;

    private float current_time;
    public bool is_active;

    private SpawnPattern spawn_pattern;
    public bool spawn_sides_only;
    private float elapsed_time;

    private SpawnerTimeRate current_spawn_time_rate;

    void Start() {
        current_spawn_time_rate = spawn_times[0];
        SetRate(current_spawn_time_rate.spawn_rate, current_spawn_time_rate.spawn_count);
        elapsed_time = 0;
    }

    public void SetActive() {
        is_active = true;
    }

    public void SetRate(int spawn_time, int spawn_number) {
        this.spawn_time = spawn_time;
        this.spawn_number = spawn_number;
    }

    void Update() {
        elapsed_time += Time.deltaTime;
        if (is_active) {
            current_time += Time.deltaTime;
            UpdateSpawnRate(elapsed_time, spawn_times);
        }
        if (current_time > spawn_time && is_active) {
            SpawnUnits(spawn_prefab, spawn_number, spawn_sides_only);
            current_time -= spawn_time;
        }
    }

    private void UpdateSpawnRate(float gameTime, SpawnerTimeRate[] spawner_list) {

        bool new_event = false;
        for (var i = 1; i < spawner_list.Length; i++) {
            var spawn_event = spawner_list[i];
            if (spawn_event.startTime < gameTime && spawn_event.startTime > current_spawn_time_rate.startTime) {
                current_spawn_time_rate = spawn_event;
                new_event = true;
            }
        }

        if (new_event) {
            SetRate(current_spawn_time_rate.spawn_rate, current_spawn_time_rate.spawn_count);
            print("New rate assigned: " + current_spawn_time_rate.spawn_rate + " " + current_spawn_time_rate.spawn_count);
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
