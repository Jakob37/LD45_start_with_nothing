using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thief : MonoBehaviour
{
    private ThievingBehaviour thieving_behaviour;

    void Awake() {
        thieving_behaviour = GetComponent<ThievingBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
            TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
            if (tomato.IsRipe()) {
                Destroy(tomato.gameObject);
                thieving_behaviour.LeaveArea();
            }
            else {
                thieving_behaviour.LookForNewTarget(ThievingTarget.TomatoFruit);
            }
        }
    }
}
