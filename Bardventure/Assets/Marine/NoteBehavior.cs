using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour 
{
    //Time in second to indicate after how much time should the object be destroyed when the Destroy function is called
    public float destroyObjectIn;

    #region variables for displacmeent/rotation 
    private Vector3 finalPosition;  
    private float startSpawning;
    private Quaternion target;
    public float Velocity;
    public float SpawningLocation;
    #endregion

    #region SliderNote prefab needed variable
    private bool showShape;
    private float chronometer = 0;
    private Vector3 previousScalingOfPrefab;
    #endregion

    #region gameObject/prefab
    public GameObject sucessEffectPrefab, failedEffectPrefab, shapePrefab;
    GameObject shape;
    #endregion




    // Start is called before the first frame update
    void Start()
    {
        showShape = false; //For sliderNote 

        Time.timeScale = 1;

        startSpawning = -4 * Mathf.Log(SpawningLocation); //to get starting z 

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
            float step = Velocity * Time.deltaTime; // calculate distance to move
 
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
                Vector3 positionOfPrefab = new Vector3(transform.position.x, transform.position.y - 0.3f / 2, transform.position.z);
                previousScalingOfPrefab = transform.localScale;  
                
                shape = Instantiate(shapePrefab, positionOfPrefab, Quaternion.identity) as GameObject;
                shape.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); 
                
                transform.position = shape.transform.GetChild(0).position;

                transform.localScale = new Vector3(shape.transform.localScale.x * transform.localScale.x, shape.transform.localScale.y * transform.localScale.y, shape.transform.localScale.z * transform.localScale.z);
            }
            chronometer += Time.deltaTime;

            if (chronometer >= destroyObjectIn)
            {
                showShape = false;
                transform.localScale = previousScalingOfPrefab; 
                Destroy(shape); 
            }

            #endregion
        }




    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Bar")
            if (gameObject.tag == "SimpleNote")
                Destroy(gameObject, destroyObjectIn + 2 / Velocity);
            else if (gameObject.tag == "SpecialNote")
                Destroy(gameObject, destroyObjectIn + 2 / Velocity);
            else if (gameObject.tag == "SliderNote")
            {

                //Debug.Log("DISTANCE BTW COLLIDERS " + Mathf.Sqrt(Mathf.Pow(other.transform.position.z - transform.position.z, 2))); 
                //Debug.Log("OFFSET " + transform.GetComponent<SphereCollider>().contactOffset + " " + other.contactOffset); 
                showShape = true;
                Destroy(gameObject, destroyObjectIn + 2 / Velocity); //destroyObjectIn*2 + 5 / Velocity);
                //Debug.Log("OBJECT " + transform.position.z); 
                //Debug.Log("COLLIDER "+other.transform.position);


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



