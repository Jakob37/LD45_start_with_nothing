using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private bool is_filled;
    public bool IsFilled {
        get {
            return is_filled;
        }
    }

    public Sprite fill_sprite;
    SpriteRenderer sprite_renderer;

    void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void FillHole() {
        is_filled = true;
        sprite_renderer.sprite = fill_sprite;
        sprite_renderer.color = new Color(1, 0, 0);
    }
}
