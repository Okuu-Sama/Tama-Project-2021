using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    public float timer = 0.0f;
    private int counter = 0;
    GameObject note;
    public GameObject myPrefab;
    public float velocity = 2.0f; // bpm --> nombre de pulsation par mesure 
    public float spawningLocation = 20;
    
    public float barLocation = 0; 



    private readonly float[] time = new float[] { 0, 0.5f, 1, 1.5f, 2, 3, 4, 4.5f, 5, 5.5f};
    private readonly float[] duration = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f};


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f; //reference music was 100 bpm --> 600 beats per sec 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Debug.Log("Display.timer " + timer); 
        while(counter < time.Length && timer >= time[counter])
        {
            //Debug.Log("Note created");
            note = Instantiate(myPrefab, new Vector3(counter%2, spawningLocation, 0), Quaternion.identity) as GameObject;
           
            counter += 1; 

        }

    }

    GameObject CreateNote()
    {

        GameObject note = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        note.transform.position = new Vector3(counter+1, 1, 1);
         
        return note; 
    }


}


