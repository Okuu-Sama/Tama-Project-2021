using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{
    //Time in second to indicate after how much time should the object be destroyed when the Destroy function is called
    private float destroyObjectIn = 0.2f; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
         
    }

    // Update is called once per frame
    void Update()
    {
        #region Move the sphere
        float step = velocity * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0.0f, -10, 0.0f), step);
        //Debug.Log("Position Sphere : "+transform.position +" with step: "+step); 
        #endregion



    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");

        if (other.gameObject.tag == "Bar")
            Destroy(this.gameObject, destroyObjectIn); 

    }
}



