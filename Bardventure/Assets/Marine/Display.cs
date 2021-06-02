using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    private float timer = 0.0f;
    private int counter = 0;

    GameObject note;
    public GameObject myPrefab; 
    public GameObject SliderPrefab;

    public enum NoteType
    {
        simple,   
        special
    }

    #region info note location/displacement
    //protected float velocity = 2.0f; // bpm --> nombre de pulsation par mesure 
    protected float velocity = 2.0f;
    protected float spawningLocation =  10.0f;  
    protected float barLocation = 1;
    #endregion

    #region test sample
    private readonly float[] time = new float[] { 0, 0.5f, 1, 1.5f, 2, 3, 4, 4.5f, 5, 5.5f};
    protected float[] duration = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f };
    private readonly NoteType[] typeNote = new NoteType[] { NoteType.simple, NoteType.simple , NoteType.simple , NoteType.simple , NoteType.special , NoteType.special , NoteType.simple , NoteType.simple , NoteType.simple , NoteType.simple }; 
    #endregion

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
            //Debug.Log("Display "+typeNote[counter]+" " + velocity);
            //Debug.Log("Note created");
            if (typeNote[counter] == NoteType.simple)
                note = Instantiate(myPrefab, new Vector3(counter%2, spawningLocation, -4 * Mathf.Log(spawningLocation)), Quaternion.identity) as GameObject;
            else
            {
                note = Instantiate(SliderPrefab, new Vector3(counter % 2, spawningLocation, -4*Mathf.Log(spawningLocation)), Quaternion.identity) as GameObject;
                note.transform.localScale = new Vector3(0.5f, velocity*duration[counter]-0.2f, 0.5f);
                note.GetComponent<NoteBehavior>().destroyObjectIn = duration[counter]; 


            }

            counter += 1; 

        }

    }



}


