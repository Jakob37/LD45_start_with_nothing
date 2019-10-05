using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoFruit : MonoBehaviour
{
    public float maturation_time;
    private float curr_growth_time;
    private SpriteRenderer renderer;
    private Transform transform;

    private float final_height_scale;

    public bool IsMature {
        get {
            return curr_growth_time >= maturation_time;
        }
    }

    void Awake() {
        renderer = GetComponent<SpriteRenderer>();
        transform = gameObject.transform;
        final_height_scale = transform.localScale.y;
        transform.localScale = new Vector2(0, 0);
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
        transform.localScale = new Vector2(tomato_scale, tomato_scale);

        // if (!IsRipe()) {
        //     renderer.color = new 
        // }
    }
}
