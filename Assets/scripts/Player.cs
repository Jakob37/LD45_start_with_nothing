using UnityEngine;

public class Player : MonoBehaviour {

    public Sprite[] frames;
    public float walk_anim_time = 1f;

    public GameObject tomato_plant_prefab;

    private float current_walk_time;
    private int current_walk_frame;

    public float move_speed = 0.01f;
    public Sprite dead_sprite;
    private bool alive = true;

    private SpriteRenderer sprite_renderer;
    private Rigidbody2D rigi;

    private Inventory inventory;
    private bool is_moving;
    private bool is_flipped;

    void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
        rigi = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void Start () {
        is_moving = false;
        is_flipped = false;
	}
	
	void Update () {
        if (alive) {
            UpdateMovement();
        }

        if (is_moving) {
            current_walk_time += Time.deltaTime;
            if (current_walk_time > walk_anim_time) {
                ShiftWalkFrame();
                current_walk_time = 0;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && inventory.Seeds > 0) {
            PlantTomatoPlant();
            inventory.PlantSeed();
        }
    }

    private void PlantTomatoPlant() {
        GameObject plant = Instantiate(tomato_plant_prefab);
        plant.transform.position = gameObject.transform.position;
    }

    private void ShiftWalkFrame() {

        current_walk_frame += 1;
        if (current_walk_frame >= frames.Length) {
            current_walk_frame = 0;
        }
        sprite_renderer.sprite = frames[current_walk_frame];
    }

    private void UpdateMovement() {

        float horiz_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");

        is_moving = false;

        if (horiz_input != 0) {
            transform.position = new Vector2(transform.position.x + move_speed * horiz_input, transform.position.y);
            is_moving = true;

            if (horiz_input < 0) {
                is_flipped = false;
            }
            else if (horiz_input > 0) {
                is_flipped = true;
            }
        }

        if (is_flipped) {
            sprite_renderer.transform.localScale = new Vector3(-1, 1, 1);
        }
        else {
            sprite_renderer.transform.localScale = new Vector3(1, 1, 1);
        }

        if (vertical_input != 0) {
            transform.position = new Vector2(transform.position.x, transform.position.y + move_speed * vertical_input);
            is_moving = true;
        }
    }

    public void Died() {
        sprite_renderer.sprite = dead_sprite;
        alive = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<TomatoSeed>() != null) {
            inventory.AddSeed();
            Destroy(coll.gameObject);
        }
    }
}
