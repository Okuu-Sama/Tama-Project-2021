using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : Display 
{
    //Time in second to indicate after how much time should the object be destroyed when the Destroy function is called
    public float destroyObjectIn;

    private Vector3 finalPosition;
    private float startSpawning;
    private bool showShape;
    private float chronometer = 0; 
 
    private Quaternion target;

    public GameObject sucessEffectPrefab, failedEffectPrefab, shapePrefab;
    GameObject shape;


    // Start is called before the first frame update
    void Start()
    {
        showShape = false; //For sliderNote 

        Time.timeScale = 1;
        startSpawning = -4 * Mathf.Log(spawningLocation); //to get starting z 

        if (this.gameObject.tag == "SpecialNote")
        {
            float m1, m2; //Slopes 
            float rotationOf = 0.0f;
            m1 = 0.0f; 
            // Smoothly tilts a transform towards a target rotation.
            // Rotate the cube by converting the angles into a quaternion.

            float final_z = 10.0f; 
 
            m2 = -Mathf.Exp(-final_z / 4) / 4;
            rotationOf = Mathf.Abs((m2 - m1) / (1 + m1 * m2));
            target = Quaternion.Euler(-90-Mathf.Atan(rotationOf) * (180 / Mathf.PI), 0, 0);
            
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Note behavior " + velocity);
        if(showShape == false)
        { 
            #region Move the GameObject
            float step = velocity * Time.deltaTime; // calculate distance to move
 
            //Debug.Log("Note behavior step " + step);
            startSpawning += step;

            #region vertical trajectory NOT USED
            //finalPosition.x = this.transform.position.x;
            //finalPosition.y = -10.0f; 
            //finalPosition.z = this.transform.position.x;
            #endregion

            #region exponential trajectory
            finalPosition.x = this.transform.position.x; // 2D trajectory, x doesn't matter
            finalPosition.z = startSpawning;
            finalPosition.y = Mathf.Exp(-finalPosition.z / 4);//Mathf.Exp(-finalPosition.z / 4);
            transform.position = finalPosition;
            #endregion

            #endregion

             

            #region rotate if GameObject is type SpecialNote

            if (this.gameObject.tag == "SpecialNote")
                transform.rotation = Quaternion.Slerp(transform.rotation, target, step);


            #endregion
        }
        else
        {
            #region SliderNote reveals its pattern
            
            if (chronometer == 0)
            {
                Vector3 positionOfPrefab = new Vector3(transform.position.x, transform.position.y - shapePrefab.transform.localScale.y/2, transform.position.z); 
                shape = Instantiate(shapePrefab, positionOfPrefab, Quaternion.identity) as GameObject;
                transform.position = shape.transform.GetChild(0).position; 
                transform.localScale = shape.transform.GetChild(1).localScale;
            }
                

            chronometer += Time.deltaTime;

            if (chronometer >= destroyObjectIn-0.1f)
            {
                showShape = false;
                //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Destroy(shape); 
            }

            #endregion
        }




    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision "+gameObject.name+ " "+ gameObject.tag + " "+destroyObjectIn);

        if (other.gameObject.tag == "Bar")
            if (gameObject.tag == "SimpleNote")
                Destroy(gameObject, destroyObjectIn + 5 / velocity);
            else if (gameObject.tag == "SpecialNote")
                Destroy(gameObject, destroyObjectIn + 5 / velocity);
            else if (gameObject.tag == "SliderNote")
            {
                //Debug.Log("Position " + transform.position.z + " " + transform.position.y);
                showShape = true;
                Destroy(gameObject, destroyObjectIn*2 + 5 / velocity);
                
            }
                



    }

    // Function to display effect 
    private void Suceed()
    {
        GameObject visualEffect = Instantiate(sucessEffectPrefab, new Vector3(transform.position.x+0.2f, transform.position.y + 0.2f, transform.position.z + 0.2f), Quaternion.identity) as GameObject;
        visualEffect.transform.parent = transform;
        visualEffect.transform.SetAsFirstSibling(); 
        
        transform.GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).GetChild(0).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).GetChild(1).localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.GetChild(0).GetChild(2).localScale = new Vector3(0.1f, 0.1f, 0.1f);

       
        Destroy(visualEffect, 0.1f); 
    }




}



