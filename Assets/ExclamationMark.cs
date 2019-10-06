using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExclamationMark : MonoBehaviour
{
    public Sprite[] frames;
    private SpriteRenderer sprite_renderer;

    public float display_time;
    private float curr_display_time;
    public float anim_loop_time;
    private float curr_loop_time;
    private int anim_index;

    void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void Activate() {
        curr_display_time = display_time;
    }

    void Update() {
        if (curr_display_time > 0) {
            sprite_renderer.color = new Color(1, 1, 1, 1);
            curr_display_time -= Time.deltaTime;
            curr_loop_time -= Time.deltaTime;

            if (curr_loop_time <= 0) {
                anim_index += 1;
                if (anim_index >= frames.Length) {
                    anim_index -= frames.Length;
                }
                curr_loop_time += anim_loop_time;
            }

            sprite_renderer.sprite = frames[anim_index];
        }
        else {
            sprite_renderer.color = new Color(0, 0, 0, 0);
        }
    }
}
