using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{
    //Time in second to indicate after how much time should the object be destroyed when the Destroy function is called
    
    private Vector3 finalPosition;
    public float destroyObjectIn; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
         
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Note behavior " + velocity);
        #region Move the sphere
        float step = velocity * Time.deltaTime; // calculate distance to move
        //Debug.Log("Note behavior step " + step);

        finalPosition.x = this.transform.position.x;
        finalPosition.y = -10.0f; 
        finalPosition.z = this.transform.position.x;

        transform.position = Vector3.MoveTowards(transform.position, finalPosition, step);
        //Debug.Log("Position Sphere : "+transform.position +" with step: "+step); 
        #endregion



    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision "+gameObject.name+ " "+ gameObject.tag + " "+destroyObjectIn);

        if (other.gameObject.tag == "Bar")
            if(this.gameObject.tag == "SimpleNote")
                Destroy(this.gameObject, destroyObjectIn+0.5f); 
            else if(this.gameObject.tag == "SliderNote")
                Destroy(this.gameObject, destroyObjectIn+0.5f);

    }
}



