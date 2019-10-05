using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    public float speed;
    public float morale;
    public Transform target;
    private Vector3 currentTarget;
    private Vector3 startpos;
    private Movement movement;

    void Awake() {
        movement = GetComponent<Movement>();
    }

    void Start() {

        TomatoFruit[] tomato_fruits = FindObjectsOfType<TomatoFruit>();

        print(tomato_fruits.Length);

        if (tomato_fruits.Length > 0) {
            TomatoFruit target_fruit = tomato_fruits[UnityEngine.Random.Range(0, tomato_fruits.Length - 1)];
            target = target_fruit.gameObject.transform;
            currentTarget = target.position;
        }
        else {
            LeaveArea();
        }

        movement.IsMoving = true;
        startpos = transform.position;
    }

    void Update() {
        if (currentTarget != null) {
            PerformMove();
        }
    }

    private void PerformMove() {
        float step = speed * Time.deltaTime; // calculate distance to move
        var prior_position_x = transform.position.x;
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, step);
        movement.IsFlipped = prior_position_x < transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                Destroy(tomato.gameObject);
                LeaveArea();
            }
        }
    }

    public void Scare(float scariness) {
        print("Enemy HIT");
        morale -= scariness;
        if (morale <= 0) {
          LeaveArea();
        }
    }

    private void LeaveArea() {
        currentTarget = startpos;
    }
}
