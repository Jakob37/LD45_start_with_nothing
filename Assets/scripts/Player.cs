using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject tomato_plant_prefab;
    public GameObject hole_prefab;

    private BasicAnimator basic_animator;
    private Movement movement;

    public float move_speed = 0.01f;
    public Sprite dead_sprite;
    private bool alive = true;

    // private SpriteRenderer sprite_renderer;
    private Rigidbody2D rigi;
    private Inventory inventory;

    void Awake() {
        rigi = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
        basic_animator = GetComponent<BasicAnimator>();
        movement = GetComponent<Movement>();
    }

    void Start() {
        movement.IsMoving = false;
        movement.IsFlipped = false;
    }

    void Update () {
        if (alive) {
            UpdateMovement();
        }

        if (Input.GetKeyDown(KeyCode.Space) && inventory.Seeds > 0) {
            PlantTomatoPlant();
            inventory.PlantSeed();
        }

        if (Input.GetKeyDown(KeyCode.Return) && inventory.HasShovel) {
            print("Digging hole");
            DigHole();
        }
    }

    private void PlantTomatoPlant() {
        GameObject plant = Instantiate(tomato_plant_prefab);
        plant.transform.position = gameObject.transform.position;
    }

    private void DigHole() {
        GameObject hole = Instantiate(hole_prefab);
        hole.transform.position = gameObject.transform.position;
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
    }
}
