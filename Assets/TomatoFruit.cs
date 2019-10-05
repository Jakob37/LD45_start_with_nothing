using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoFruit : MonoBehaviour
{
    public float maturation_time;
    private float curr_growth_time;
    private SpriteRenderer renderer;
    private Transform transform;
    private int plant_position;

    public Sprite[] frames;
    

    private float final_height_scale;

    public bool IsMature {
        get {
            return curr_growth_time >= maturation_time;
        }
    }

    public int getPosition()
    {
        return plant_position;
    }

    public void setPosition(int position)
    {
        plant_position = position;
    }

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        curr_growth_time = 0;

    }

    void Update() {
        curr_growth_time += Time.deltaTime;
        if (curr_growth_time < maturation_time) {
            UpdateMaturationStage();
        }
    }

    public bool IsRipe() {
        return curr_growth_time >= maturation_time;
    }

    private void UpdateMaturationStage() {
        float tomato_scale = (curr_growth_time / maturation_time) * final_height_scale;

        int index = Math.Min(
            frames.Length - 1,
            (int)Math.Floor(curr_growth_time / maturation_time * frames.Length));

        this.GetComponent<SpriteRenderer>().sprite = frames[index];
    }
}
