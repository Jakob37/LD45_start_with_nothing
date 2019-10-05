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

    void Awake() {
        movement = GetComponent<Movement>();
    }

    void Start() {
        
        if (transform.position.x < 1) {
            direction = Direction.right;
        }
        else {
            direction = Direction.left;
        }
    }

    void Update() {
        // PerformMove();
    }

    // private void PerformMove() {
    //     float step = movement.speed * Time.deltaTime; // calculate distance to move
    //     var prior_position_x = transform.position.x;
    //     transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
    //     movement.IsFlipped = prior_position_x < transform.position.x;
    // }
}
