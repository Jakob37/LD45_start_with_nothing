using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class TomatoPlant : MonoBehaviour
{
    public GameObject tomato_prefab;

    public Sprite[] frames;
    private float growth_time;
    private float tomato_growth_time;
    private float fruit_time;
    private float fruit_time_offset;

    private List<Sprite> grow_frames;
    private Random rnd;

    public float maturation_time;
    public float tomato_maturation_time;

    private Transform transform;
    private float final_height_scale;

    private bool has_tomato;

    private List<Vector3> possible_positions;

    void Awake() {
        possible_positions = new List<Vector3>();


    }

    void Start() {
        Vector3 position = gameObject.transform.position;
        position.x -= 0.07f;
        position.y += 0.02f;
        possible_positions.Add(position);

        position = gameObject.transform.position;
        position.x += 0.14f;
        position.y -= 0.02f;
        possible_positions.Add(position);

        position = gameObject.transform.position;
        position.x += 0.12f;
        position.y += 0.07f;
        possible_positions.Add(position);

        position = gameObject.transform.position;
        position.x -= 0.05f;
        position.y -= 0.06f;
        possible_positions.Add(position);

        position = gameObject.transform.position;
        position.x += 0.11f;
        position.y -= 0.10f;
        possible_positions.Add(position);

        rnd = new Random();

        growth_time = 0;
        fruit_time_offset = (float)(maturation_time + 0.3);
    }

    void Update() {

        if (growth_time < maturation_time) {
            growth_time = growth_time + Time.deltaTime;
            UpdateGrowth();
        }
        else if (fruit_time < fruit_time_offset)
        {
            fruit_time = fruit_time + Time.deltaTime;
        }
        else if (growth_time > maturation_time &&
            possible_positions.Count > 0 &&
            fruit_time > fruit_time_offset) {

            has_tomato = true;
            tomato_growth_time = 0;
            MakeTomato();
            fruit_time_offset = (float)(rnd.NextDouble() + 0.5);
            fruit_time = 0;
        } 
    }

    private void MakeTomato() {

        GameObject tomato = Instantiate(tomato_prefab);

        int index = rnd.Next(0, possible_positions.Count);
        tomato.transform.position = possible_positions[index];
        possible_positions.RemoveAt(index);

    }

    private void UpdateGrowth() {

        int index = Math.Min(
            frames.Length - 1,
            (int)Math.Floor(growth_time / maturation_time * frames.Length));

        this.GetComponent<SpriteRenderer>().sprite = frames[index];

    }

}
