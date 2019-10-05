using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTomatoGuy : MonoBehaviour
{
    // public float speed;
    private Transform target;
    private Vector3 currentTarget;
    private Movement movement;
    private bool is_done;
    private Vector2 start_position;
    private float dist_to_end;

    void Awake() {
        dist_to_end = 0.1f;
        movement = GetComponent<Movement>();
    }

    void Start() {

        is_done = false;
        LookForNewTarget();
        movement.IsMoving = true;
        start_position = gameObject.transform.position;
    }

    void Update() {
        if (target == null && !is_done) {
            LookForNewTarget();
        }
        PerformMove();

        if (is_done && HasReachedEndPosition(dist_to_end)) {
            Destroy(gameObject);
        }
    }

    private bool HasReachedEndPosition(float dist_to_end) {
        return Vector2.Distance(transform.position, start_position) < dist_to_end;
    }

    private void PerformMove() {
        float step = movement.speed * Time.deltaTime; // calculate distance to move
        var prior_position_x = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
        movement.IsFlipped = prior_position_x < transform.position.x;
    }

    private void LookForNewTarget() {
        TomatoPlant[] tomato_fruits = FindObjectsOfType<TomatoPlant>();
        if (tomato_fruits.Length > 0) {
            TomatoPlant target_fruit = tomato_fruits[UnityEngine.Random.Range(0, tomato_fruits.Length - 1)];
            target = target_fruit.gameObject.transform;
            currentTarget = target.position;
        }
        else {
            is_done = true;
            LeaveArea();
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TomatoPlant>() != null) {
            TomatoPlant tomato_plant = coll.gameObject.GetComponent<TomatoPlant>();
            Destroy(tomato_plant.gameObject);
            LeaveArea();
            is_done = true;
        }
    }

    private void LeaveArea() {
        currentTarget = start_position;
    }
}
