using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorWheel : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotation_speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotation_speed);
    }
}
