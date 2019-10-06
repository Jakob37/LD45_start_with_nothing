using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThievingTarget {
    TomatoFruit,
    TomatoPlant
}

public class ThievingBehaviour : MonoBehaviour
{
    public float morale;
    private Transform target;
    private Vector3 currentTarget;
    private Vector3 startpos;
    private Movement movement;
    private bool is_done;
    private Vector2 start_position;
    private float dist_to_end;

    public ThievingTarget thieving_target_type;

    void Awake() {
        dist_to_end = 0.1f;
        movement = GetComponent<Movement>();
    }

    void Start() {

        is_done = false;
        LookForNewTarget(thieving_target_type);
        movement.IsMoving = true;

        start_position = gameObject.transform.position;
    }


    void Update() {
        if (target == null && !is_done) {
            LookForNewTarget(thieving_target_type);
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

    private void LookForNewTarget(ThievingTarget thieving_target_type) {

        bool no_thief_target = false;
        if (thieving_target_type == ThievingTarget.TomatoFruit) {
            TomatoFruit[] thieving_target = FindObjectsOfType<TomatoFruit>();
            if (thieving_target.Length > 0) {
                TomatoFruit target_fruit = thieving_target[Random.Range(0, thieving_target.Length - 1)];
                target = target_fruit.gameObject.transform;
                currentTarget = target.position;
            }
            else {
                no_thief_target = true;
            }
        }
        else if (thieving_target_type == ThievingTarget.TomatoPlant) {
            TomatoPlant[] thieving_target = FindObjectsOfType<TomatoPlant>();
            if (thieving_target.Length > 0) {
                TomatoPlant target_fruit = thieving_target[Random.Range(0, thieving_target.Length - 1)];
                target = target_fruit.gameObject.transform;
                currentTarget = target.position;
            }
            else {
                no_thief_target = true;
            }
        }
        else {
            throw new System.Exception("Unimplemented thieving type: " + thieving_target_type);
        }

        if (no_thief_target) {
            is_done = true;
            LeaveArea();
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                Destroy(tomato.gameObject);
                LeaveArea();
                is_done = true;
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
        currentTarget = start_position;
    }
}
