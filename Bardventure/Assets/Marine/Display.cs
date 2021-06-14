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

    private float RightTrack, LeftTrack; 

    //protected float velocity = 2.0f; // bpm --> nombre de pulsation par mesure 
    //Needed to know the speed at which the game objects moves and their (y-axis) spawning location

    private float velocity, spawningLocation; 

    public float Velocity { get => velocity; set => velocity = value; }
    public float SpawningLocation { get => spawningLocation; set => spawningLocation = value; }
    


    public Display(GameObject _SimpleNotePrefab, GameObject _SpecialNotePrefab, GameObject _SliderNotePrefab, float _velocity, float _spawningLocation, Vector3 position)
    {
        SimpleNotePrefab = _SimpleNotePrefab;
        SpecialNotePrefab = _SpecialNotePrefab;
        SliderNotePrefab = _SliderNotePrefab;
        velocity = _velocity;
        spawningLocation = _spawningLocation;

        RightTrack = position.x + 0.4f;
        LeftTrack = position.x - 0.4f; 

    }


    private float Track(int side)
    {
        if (side == 0)//right 
            return RightTrack;
        else if (side == 1)//left 
            return LeftTrack;

        return 0;
    }
    public void DisplayNote(string noteType, float duration, int track)
    {
        
        if (noteType == NoteType.SpecialNote.ToString())
        {

            note = GameObject.Instantiate(SpecialNotePrefab, new Vector3(Track(track), SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;

            note.transform.localScale = new Vector3(note.transform.localScale.x, Velocity * duration - 0.2f, note.transform.localScale.z);
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;



        }
        else
        {
            note = GameObject.Instantiate(SliderNotePrefab, new Vector3(Track(track), SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;

        }
    }



    public void DisplayNote(string noteType, int track)
    {

        if (noteType == NoteType.SimpleNote.ToString())
        {
            note = GameObject.Instantiate(SimpleNotePrefab, new Vector3(Track(track), SpawningLocation, -4 * Mathf.Log(SpawningLocation)), Quaternion.identity) as GameObject;
            note.GetComponent<NoteBehavior>().Velocity = Velocity;
            note.GetComponent<NoteBehavior>().SpawningLocation = SpawningLocation;
        }
    }

  


}


