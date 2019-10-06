using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject tomato_plant_prefab;
    public GameObject hole_prefab;

    private BasicAnimator basic_animator;
    private Movement movement;
    private AudioController audio_controller;

    public float move_speed = 0.01f;
    public Sprite dead_sprite;
    private bool alive = true;
    public float dig_time;
    public Sprite[] shovel_frames;

    private float freeze_time;

    // private SpriteRenderer sprite_renderer;
    private Rigidbody2D rigi;
    private Inventory inventory;

    void Awake() {
        rigi = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        basic_animator = GetComponent<BasicAnimator>();
        movement = GetComponent<Movement>();
        audio_controller = FindObjectOfType<AudioController>();
    }

    void Start() {
        movement.IsMoving = false;
        movement.IsFlipped = false;
        if (inventory.HasShovel) {
            basic_animator.UpdateFrames(shovel_frames);
        }
    }

    void Update () {

        freeze_time -= Time.deltaTime;

        if (alive && freeze_time <= 0) {
            UpdateMovement();
        }

        if (Input.GetKeyDown(KeyCode.Z) && inventory.Seeds > 0 && freeze_time <= 0) {
            PlantTomatoPlant();
            inventory.PlantSeed();
        }

        if (Input.GetKeyDown(KeyCode.C) && inventory.HasShovel && freeze_time <= 0) {
            DigHole(dig_time);
            audio_controller.MakeDigSound();
        }
    }

    private void PlantTomatoPlant() {
        GameObject plant = Instantiate(tomato_plant_prefab);
        plant.transform.position = gameObject.transform.position;
    }

    private void DigHole(float dig_time) {
        GameObject hole = Instantiate(hole_prefab);
        hole.transform.position = gameObject.transform.position;
        Freeze(dig_time);
    }

    private void Freeze(float seconds) {
        freeze_time = seconds;
    }

    private void UpdateMovement() {

        float horiz_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        movement.IsMoving = false;

        if (horiz_input != 0) {
            transform.position = new Vector2(transform.position.x + move_speed * horiz_input, transform.position.y);
            transform.position = movement.LimitPositionToScreen(transform.position);
            movement.IsMoving = true;

            if (horiz_input < 0) {
                movement.IsFlipped = false;
            }
            else if (horiz_input > 0) {
                movement.IsFlipped = true;
            }
        }

        if (vertical_input != 0) {
            transform.position = new Vector2(transform.position.x, transform.position.y + move_speed * vertical_input);
            transform.position = movement.LimitPositionToScreen(transform.position);
            movement.IsMoving = true;
        }
    }

    public void Died() {
        // sprite_renderer.sprite = dead_sprite;
        alive = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<TomatoSeed>() != null) {
            inventory.AddSeed();
            Destroy(coll.gameObject);
        }
        else if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                inventory.AddTomato();
                Destroy(coll.gameObject);
            }
        }
        else if (coll.gameObject.GetComponent<ShopSpot>() != null) {
            ShopSpotCollide(coll.gameObject.GetComponent<ShopSpot>());
        }
    }

    private void ShopSpotCollide(ShopSpot shop_spot) {

        bool was_bought = false;
        if (shop_spot.HasShopSeed()) {
            was_bought = BuySeed(shop_spot.GetShopSeed());
        }
        else if (shop_spot.HasShopShovel()) {
            was_bought = BuyShovel(shop_spot.GetShopShovel());
            if (was_bought) {
                basic_animator.UpdateFrames(shovel_frames);
            }
        }

        if (was_bought) {
            shop_spot.BuyObject();
        }
    }

    private bool BuySeed(ShopSeed shop_seed) {
        if (inventory.Tomatoes >= shop_seed.price) {
            inventory.BuySeed(shop_seed.price);
            return true;
        }
        return false;
    }

    private bool BuyShovel(ShopShovel shop_shovel) {
        if (inventory.Tomatoes >= shop_shovel.price) {
            inventory.BuyShovel(shop_shovel.price);
            return true;
        }
        return false;
    }
}
