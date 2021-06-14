using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display 
{

    #region prefabs/game object
    GameObject note;
    public GameObject SimpleNotePrefab; 
    public GameObject SpecialNotePrefab;
    public GameObject SliderNotePrefab;
    #endregion

    public enum NoteType
    {
        SimpleNote,   
        SpecialNote, 
        SliderNote
    }

    #region info note location/displacement
    //protected float velocity = 2.0f; // bpm --> nombre de pulsation par mesure 

    //Needed to know the speed at which the game objects moves and their (y-axis) spawning location

    private float velocity, spawningLocation; 

    public float Velocity { get => velocity; set => velocity = value; }
    public float SpawningLocation { get => spawningLocation; set => spawningLocation = value; }
    #endregion

    /*
    #region test sample
    private readonly float[] time = new float[] { 0, 0.5f, 1, 1.5f, 2, 3, 4, 4.5f, 5, 5.5f};
    protected float[] duration = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f };
    private readonly NoteType[] typeNote = new NoteType[] { NoteType.SimpleNote, NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SpecialNote , NoteType.SliderNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote , NoteType.SimpleNote }; 
    #endregion
    */
    /*
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
*/


    public void DisplayNote(string noteType, float duration, int counter)
    {
        
        if (noteType == NoteType.SpecialNote.ToString())
        {

            note = GameObject.Instantiate(SpecialNotePrefab, new Vector3(counter % 2, SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;

            note.transform.localScale = new Vector3(note.transform.localScale.x, Velocity * duration - 0.2f, note.transform.localScale.z);
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;



        }
        else
        {
            note = GameObject.Instantiate(SliderNotePrefab, new Vector3(counter % 2, SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;
        }
    }



    public void DisplayNote(string noteType, int counter)
    {

        if (noteType == NoteType.SimpleNote.ToString())
        {
            note = GameObject.Instantiate(SimpleNotePrefab, new Vector3(counter % 2, SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;
        }
    }

}


