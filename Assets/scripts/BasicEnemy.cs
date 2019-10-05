using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Vector3 currentTarget;

    void Start()
    {
        currentTarget = target.position;
    }

    void Update()
    {
      float step =  speed * Time.deltaTime; // calculate distance to move
      transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);
    }

    void OnTriggerEnter2D(Collider2D coll) {
      if (coll.gameObject.GetComponent<TomatoFruit>() != null) {
        print(coll.gameObject);
        TomatoFruit tomato = coll.gameObject.GetComponent<TomatoFruit>();
        if (tomato.IsRipe()) {
          Destroy(tomato.gameObject);
          currentTarget = new Vector3(0,0,0);
        }
      }
    }
}
