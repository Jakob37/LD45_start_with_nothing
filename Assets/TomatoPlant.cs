using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoPlant : MonoBehaviour
{
    public GameObject tomato_prefab;

    public Sprite[] frames;
    private float growth_time;
    private float tomato_growth_time;

    private List<Sprite> grow_frames;


    public float maturation_time;
    public float tomato_maturation_time;

    private Transform transform;
    private float final_height_scale;

    private bool has_tomato;

    void Awake() {
        //transform = gameObject.transform;
        //final_height_scale = transform.localScale.y;
        //transform.localScale = new Vector2(0, 0);
    }

    void Start() {

        growth_time = 0;
    }

    void Update() {

        if (growth_time < maturation_time) {
            growth_time = growth_time + Time.deltaTime;
            UpdateGrowth();
        }
        else if (growth_time > maturation_time && !has_tomato) {
            has_tomato = true;
            tomato_growth_time = 0;
            MakeTomato();
        } 
    }

    private void MakeTomato() {
        GameObject tomato = Instantiate(tomato_prefab);
        tomato.transform.position = gameObject.transform.position;
    }

    private void UpdateGrowth() {
        float growth_scale = (growth_time / maturation_time) * final_height_scale;

        int index = Math.Min(
            frames.Length - 1,
            (int)Math.Floor(growth_time / maturation_time * frames.Length));

        this.GetComponent<SpriteRenderer>().sprite = frames[index];
    }
}
