using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{

    // Start is called before the first frame update
    void Start()
    {
        velocity *= Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * velocity, Space.World);
        Debug.Log(velocity); 
    }
}
