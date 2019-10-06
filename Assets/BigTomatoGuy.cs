using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTomatoGuy : MonoBehaviour
{
    private ThievingBehaviour thieving_behaviour;

    void Awake() {
        thieving_behaviour = GetComponent<ThievingBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D coll) {

        if (coll.gameObject.GetComponent<TomatoPlant>() != null) {
            TomatoPlant tomato_plant = coll.gameObject.GetComponent<TomatoPlant>();
            Destroy(tomato_plant.gameObject);
            thieving_behaviour.LeaveArea();
                // LeaveArea();
                // is_done = true;
        }
    }
}
