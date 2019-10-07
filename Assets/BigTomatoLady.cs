using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction {
    right,
    left
}

public class BigTomatoLady : MonoBehaviour
{
    private Direction direction;
    private Movement movement;

    private float arbitrary_mid_point;
    private Vector2 edge_points;

    private AudioController audio_controller;

    void Awake() {
        arbitrary_mid_point = 1;
        movement = GetComponent<Movement>();
        float padding = 0.5f;
        edge_points = new Vector2(
            Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x - padding,
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x + padding
        );
        audio_controller = FindObjectOfType<AudioController>();
    }

    void Start() {
        
        if (transform.position.x < arbitrary_mid_point) {
            direction = Direction.right;
        }
        else {
            direction = Direction.left;
        }
        movement.IsMoving = true;
    }

    void Update() {
        PerformMove(direction);
        PerformObjectCleanup(direction, edge_points);
    }

    private void PerformMove(Direction direction) {

        float x_step;
        if (direction == Direction.left) {
            x_step = movement.speed * Time.deltaTime * -1;
        }
        else if (direction == Direction.right) {
            x_step = movement.speed * Time.deltaTime;
        }
        else {
            throw new System.Exception("Unknown direction: " + direction);
        }
        var prior_position_x = transform.position.x;
        transform.position = transform.position + new Vector3(x_step, 0, 0);
        movement.IsFlipped = prior_position_x < transform.position.x;
    }

    private void PerformObjectCleanup(Direction direction, Vector2 edge_points) {
        float curr_x_pos = transform.position.x;
        if (direction == Direction.right && curr_x_pos > edge_points.y) {
            Destroy(gameObject);
        }
        else if (direction == Direction.left && curr_x_pos < edge_points.x) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<TomatoPlant>() != null || 
            coll.gameObject.GetComponent<TomatoFruit>() != null) {
            // TomatoPlant tomato_plant = coll.gameObject.GetComponent<TomatoPlant>();
            Destroy(coll.gameObject);
            // thieving_behaviour.LeaveArea();
            // LeaveArea();
            // is_done = true;
        }
        else if (coll.gameObject.GetComponent<Hole>() != null) {
            Hole hole = coll.gameObject.GetComponent<Hole>();
            if (!hole.IsFilled) {
                hole.FillHole();
                audio_controller.MakeCrashSound();
                Destroy(gameObject);
            }
        }
    }
}
