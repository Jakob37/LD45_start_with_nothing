using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float speed;
    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
      float step =  speed * Time.deltaTime; // calculate distance to move
      transform.position = Vector3.MoveTowards(transform.position, target.position, step);
      //transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
    }
}
