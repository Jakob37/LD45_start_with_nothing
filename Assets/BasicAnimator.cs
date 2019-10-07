using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;
    private Movement movement;
    public Sprite[] frames;
    public float walk_anim_time = 1f;

    private float current_move_time;
    private int current_walk_frame;

    void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<Movement>();
    }

    void Update() {
        if (movement.IsMoving) {
            current_move_time += Time.deltaTime;
            if (current_move_time > walk_anim_time) {
                current_walk_frame = ShiftWalkFrame(current_walk_frame, frames);
                sprite_renderer.sprite = frames[current_walk_frame];
                current_move_time = 0;
            }
        }

        int scale_modifier = 1;
        if (movement.IsFlipped && sprite_renderer.transform.localScale.x > 0) {
            scale_modifier = -1;
        }
        else if (!movement.IsFlipped && sprite_renderer.transform.localScale.x < 0)
        {
            scale_modifier = -1;
        }
        sprite_renderer.transform.localScale = new Vector3(
            sprite_renderer.transform.localScale.x * scale_modifier,
            sprite_renderer.transform.localScale.y,
            sprite_renderer.transform.localScale.z);
    }

    private int ShiftWalkFrame(int curr_walk_frame, Sprite[] frames) {

        curr_walk_frame += 1;
        if (curr_walk_frame >= frames.Length) {
            curr_walk_frame = 0;
        }
        return curr_walk_frame;
    }

    public void UpdateFrames(Sprite[] new_frames) {
        frames = new_frames;
    }
}
