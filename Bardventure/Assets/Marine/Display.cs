using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    private float timer = 0.0f;
    private int counter = 0;
    GameObject note;
    public GameObject myPrefab;
    protected float velocity = 6; 


 
    private readonly float[] time = new float[] { 0, 0.6f, 1.2f, 1.7999999999999998f, 2.4f, 3, 3.5999999999999996f, 4.2f, 4.8f, 5.3999999999999995f};
    private readonly float[] duration = new float[] { 0.6f, 0.6f, 0.6f, 0.6f, 1.1999999999999997f, 0.6f, 1.2f, 0.6f, 0.6f, 0.6f};


    // Start is called before the first frame update
    void Start()
    {

        Time.timeScale = 0.6f; //reference music was 100 bpm --> 600 beats per sec 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
        while(counter < time.Length && timer >= time[counter])
        {
            Debug.Log("Note created");
            note = Instantiate(myPrefab, new Vector3(counter%2, 10, 10), Quaternion.identity) as GameObject;
            Destroy(note, duration[counter]); 
            counter += 1; 

        }

    }

    GameObject CreateNote()
    {

        GameObject note = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        note.transform.position = new Vector3(counter+1, 1, 1);
        //Destroy(note, duration[counter]); 
        return note; 
    }

    // Displace component on the y axis from top to bottom
    void DisplaceVerticallyGameObject( GameObject component, float velocity)
    {
    
        if (component != null)
        {
            component.transform.Translate(Vector3.down * velocity, Space.World);
        }
           
    }
}


