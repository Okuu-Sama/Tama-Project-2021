using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    public void GetSliderNoteFinalPosition(string noteType, int track) //track = 0 or 1 
    {
        if (noteType == NoteType.SliderNote.ToString())
        {
            GameObject cerclePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/CercleSliderNote.prefab", typeof(GameObject)); 


            int numberOfChild = cerclePrefab.transform.childCount;
            //float[][] position = new float[numberOfChild][]; 
            Vector3[] position = new Vector3[numberOfChild] ; 

            float barLocation = -0.05366753f; //GameObject.Find("Bar").transform.position.z;
            float trackLocation = Track(track);

            float finalPositionY = Mathf.Exp(-barLocation / 4) - 0.3f / 2; 

            int iterator = 1;

            while (iterator < numberOfChild)
            {


                position[iterator] = new Vector3(trackLocation+ cerclePrefab.transform.GetChild(iterator).position.x * 0.3f,finalPositionY + cerclePrefab.transform.GetChild(iterator).position.y * 0.3f, barLocation);
                Debug.Log("POSITION " +position[iterator].x + " "+ position[iterator].y + " " + position[iterator].z); 
                iterator++; 
            }

            //return position; 


        }

        //return null; 
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
  





