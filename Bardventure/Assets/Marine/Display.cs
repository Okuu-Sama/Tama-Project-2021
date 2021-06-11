using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display : MonoBehaviour
{

    public float timer = 0.0f;
    private int counter = 0;

    GameObject note;
    public GameObject SimpleNotePrefab; 
    public GameObject SpecialNotePrefab;
    public GameObject SliderNotePrefab;

    public enum NoteType
    {
        SimpleNote,   
        SpecialNote, 
        SliderNote
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
    private readonly NoteType[] typeNote = new NoteType[] { NoteType.SimpleNote, NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SpecialNote , NoteType.SliderNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote }; 
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

            if (typeNote[counter].ToString() == NoteType.SimpleNote.ToString())
                DisplayNote(typeNote[counter].ToString()); 
            else
                DisplayNote(typeNote[counter].ToString(), duration[counter]);
            counter += 1; 

        }

    }
    

    public void DisplayNote(string noteType, float duration)
    {
        
        if (noteType == NoteType.SpecialNote.ToString())
        {
            note = Instantiate(SpecialNotePrefab, new Vector3(counter % 2, spawningLocation, -4 * Mathf.Log(spawningLocation)), Quaternion.identity) as GameObject;
            note.transform.localScale = new Vector3(transform.localScale.x, velocity * duration - 0.2f, transform.localScale.z);
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;


        }
        else
        {
            note = Instantiate(SliderNotePrefab, new Vector3(counter % 2, spawningLocation, -4 * Mathf.Log(spawningLocation)), Quaternion.identity) as GameObject;
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;
        }
    }
    public void DisplayNote(string noteType)
    {

        if (noteType == NoteType.SimpleNote.ToString())
            note = Instantiate(SimpleNotePrefab, new Vector3(counter % 2, spawningLocation, -4 * Mathf.Log(spawningLocation)), Quaternion.identity) as GameObject;
    }

}


