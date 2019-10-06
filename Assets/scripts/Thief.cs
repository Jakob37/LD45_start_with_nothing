using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    private ThievingBehaviour thieving_behaviour;

    private bool has_tomato;

    void Awake() {
        thieving_behaviour = GetComponent<ThievingBehaviour>();
    }

    void Start() {
        has_tomato = false;
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (!has_tomato && coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                Destroy(tomato.gameObject);
                thieving_behaviour.LeaveArea();
                has_tomato = true;
            }
            else {
                thieving_behaviour.LookForNewTarget(ThievingTarget.TomatoFruit);
            }
        }
    }
}
