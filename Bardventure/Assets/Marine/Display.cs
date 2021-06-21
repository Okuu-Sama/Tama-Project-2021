using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display 
{

    #region prefabs/game object
    GameObject note;
    private GameObject SimpleNotePrefab; 
    private GameObject SpecialNotePrefab;
    private GameObject SliderNotePrefab;
    #endregion

    private enum NoteType
    {
        SimpleNote,   
        SpecialNote, 
        SliderNote
    }

    private float RightTrack, LeftTrack; 

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

            Debug.Log("Size slider: " + velocity * duration); 
            note.transform.localScale = new Vector3(note.transform.localScale.x, (velocity * duration)/(SpecialNotePrefab.transform.localScale.y) , note.transform.localScale.z);
            note.GetComponent<NoteBehavior>().destroyObjectIn = duration;



        }
        else if (noteType == NoteType.SliderNote.ToString())
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


    public Vector3[] GetSliderNoteFinalPosition(string noteType)
    {
        if (noteType == NoteType.SliderNote.ToString())
        {
            
            
            int numberOfChild = SliderNotePrefab.GetComponent<NoteBehavior>().transform.childCount;
            Vector3[] position = new Vector3[numberOfChild];

            float barLocation = GameObject.Find("Bar").transform.position.z;

            int iterator = 0; 

            while(iterator < numberOfChild)
            {
                position[iterator] = new Vector3();
                iterator++; 
            }

            return position; 


        }

        return null; 
    }

    public void SetScore(float score)
    {
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = score.ToString(); 
    }

    public void AddToScore(float score)
    {
        score += float.Parse(GameObject.Find("Canvas").transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text);
        GameObject.Find("Canvas").transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = score.ToString();
    }

}
  





