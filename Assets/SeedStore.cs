using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedStore : MonoBehaviour
{
    private Inventory inventory;
    private Player player;
    public float buy_range;
    public int seed_price;
    private SpriteRenderer sprite_renderer;
    private Color initial_color;

    void Awake() {
        inventory = FindObjectOfType<Inventory>();
        player = FindObjectOfType<Player>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        initial_color = sprite_renderer.color;
    }

    void Start() {
        
    }

    private bool PlayerIsClose() {
        return Vector2.Distance(gameObject.transform.position, player.gameObject.transform.position) < buy_range;
    }

    private bool HasTomatoes() {
        return inventory.Tomatoes >= seed_price;
    }

    void Update() {

        if (PlayerIsClose()) {
            sprite_renderer.color = new Color(1, 0, 0);
        }
        else {
            sprite_renderer.color = initial_color;
        }
        
        if (Input.GetKeyDown(KeyCode.Return) && PlayerIsClose() && HasTomatoes()) {
            inventory.BuySeed(seed_price);
        }

    }
}
