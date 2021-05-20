using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{

    private float distance = 0; 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
        distance = Time.deltaTime * velocity; 
    }

    // Update is called once per frame
    void Update()
    {
        float step = velocity * Time.deltaTime; // calculate distance to move

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, -10, 0.0f), step);
        Debug.Log("Position Sphere : "+transform.position +" with step: "+step); 

        //transform.Translate(Vector3.down * distance, Space.World);
        //Debug.Log(velocity); 
    }
}
