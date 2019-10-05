using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefSpawner : MonoBehaviour
{
    public int spawn_time;
    public int spawn_number;

    public GameObject thief_prefab;

    private float current_time;

    void Start() {
        
    }

    void Update() {
        current_time += Time.deltaTime;
        if (current_time > spawn_time) {
            SpawnThieves(spawn_number);
            current_time -= spawn_time;
        }
    }

    private void SpawnThieves(int nbr_thieves) {

        for (int i = 0; i < nbr_thieves; i++) {
            InitializeThief();
            print("Initializing thief!");
        }
    }

    private void InitializeThief() {
        Instantiate(thief_prefab);
        thief_prefab.transform.position = RandomEdgePosition();
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
