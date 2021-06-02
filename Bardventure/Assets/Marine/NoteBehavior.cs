using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{
    //Time in second to indicate after how much time should the object be destroyed when the Destroy function is called
    public float destroyObjectIn;

    private Vector3 finalPosition;
    
    private float startSpawning;
    private float rotationOf;

    private float m1, m2; //Slopes 
    public Quaternion target; 

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        startSpawning = -4 * Mathf.Log(spawningLocation); //to get starting z 

        m1 = 0.0f; //-Mathf.Exp(-startSpawning / 4) / 4;

        if (this.gameObject.tag == "SliderNote")
        {
            // Smoothly tilts a transform towards a target rotation.
            // Rotate the cube by converting the angles into a quaternion.
            //target = Quaternion.Euler(-90, 0, 0);
           

            m2 = -Mathf.Exp(-10 / 4) / 4;
            rotationOf = Mathf.Abs((m2 - m1) / (1 + m1 * m2));
            target = Quaternion.Euler(-90-Mathf.Atan(rotationOf) * (180 / Mathf.PI), 0, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, velocity* Time.deltaTime);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Note behavior " + velocity);
        #region Move the sphere
        float step = velocity * Time.deltaTime; // calculate distance to move
 
        //Debug.Log("Note behavior step " + step);
        startSpawning += step;

        #region vertical trajectory
        //finalPosition.x = this.transform.position.x;
        //finalPosition.y = -10.0f; 
        //finalPosition.z = this.transform.position.x;
        #endregion

        #region exponential trajectory
        finalPosition.x = this.transform.position.x; // 2D trajectory, x doesn't matter
        finalPosition.z = startSpawning;
        finalPosition.y = Mathf.Exp(-finalPosition.z / 4);
        transform.position = finalPosition;
        #endregion


        /*
        if (this.gameObject.tag == "SliderNote")
        {
            //rotation 

            m2 = -Mathf.Exp(-finalPosition.z / 4)/4; // Slope of the tangent line at current position on the curve
            rotationOf = Mathf.Abs((m2 - m1) / (1 + m1 * m2));

            Debug.Log("Slider "+m1 + " " + m2 +" "+rotationOf);

            //Debug.Log("Slider rotation " + transform.rotation);

            //transform.Rotate(m2, 0.0f, 0.0f, Space.World);

            //transform.Rotate(-Mathf.Atan(rotationOf) * (180 / Mathf.PI), 0.0f, 0.0f, Space.World);
            m1 = m2;

        }
        */
        
        if (this.gameObject.tag == "SliderNote")
            transform.rotation = Quaternion.Slerp(transform.rotation, target, step);
        


        //transform.position = Vector3.MoveTowards(transform.position, finalPosition, step);
        //Debug.Log("Position Sphere : "+transform.position +" with step: "+step); 
        #endregion

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision "+gameObject.name+ " "+ gameObject.tag + " "+destroyObjectIn);
        
        if (other.gameObject.tag == "Bar")
            if(this.gameObject.tag == "SimpleNote")
                Destroy(this.gameObject, destroyObjectIn+5/velocity); 
            else if(this.gameObject.tag == "SliderNote")
                Destroy(this.gameObject, destroyObjectIn+ 5/velocity);
        
    }



}



