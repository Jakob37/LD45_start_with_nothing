using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefSpawner : MonoBehaviour
{
    public int spawn_time;
    public int spawn_number;

    public int spawn_time_tomato_guy;
    public int spawn_number_tomato_guy;

    public GameObject thief_prefab;
    public GameObject big_tomato_guy_prefab;

    private float current_time;
    private float current_time_tomato_guy;

    void Update() {
        current_time += Time.deltaTime;
        if (current_time > spawn_time) {
            SpawnUnits(thief_prefab, spawn_number);
            current_time -= spawn_time;
        }

        current_time_tomato_guy += Time.deltaTime;
        if (current_time_tomato_guy > spawn_time_tomato_guy) {
            SpawnUnits(big_tomato_guy_prefab, spawn_number_tomato_guy);
            current_time_tomato_guy -= spawn_time_tomato_guy;
        }
    }

    private void SpawnUnits(GameObject spawn_prefab, int nbr_units) {

        for (int i = 0; i < nbr_units; i++) {
            InitializeUnit(spawn_prefab);
        }
    }

    private void InitializeUnit(GameObject spawn_prefab) {
        Instantiate(spawn_prefab);
        spawn_prefab.transform.position = RandomEdgePosition();
    }

    public static Vector2 RandomEdgePosition() {

        Vector2 left_positions = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector2 right_positions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        float padding = 0.5f;

        int side = UnityEngine.Random.Range(0, 3);
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
            xpos = right_positions.x - padding;
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
