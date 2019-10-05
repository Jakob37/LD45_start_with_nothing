using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Vector3 currentTarget;
    private Movement movement;

    void Awake() {
        movement = GetComponent<Movement>();
    }

    void Start() {
        currentTarget = target.position;
        movement.IsMoving = true;
    }

    void Update() {
        float step =  speed * Time.deltaTime; // calculate distance to move
        var prior_position_x = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
        movement.IsFlipped = prior_position_x < transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                Destroy(tomato.gameObject);
                currentTarget = new Vector3(0,0,0);
            }
        }
    }
}
