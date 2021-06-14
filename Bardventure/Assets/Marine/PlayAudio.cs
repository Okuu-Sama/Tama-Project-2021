using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    private AudioSource audiosource;
    private float musicStart = 0;
    private float timing = 0;

    //to remove 
    protected float velocity = 2.0f;
    protected float spawningLocation = 10.0f;
    protected float barLocation; 

    private int counter = 0;
    private Display display = new Display();

    #region test sample
    private readonly float[] time = new float[] { 0, 0.5f, 1, 1.5f, 2, 3, 4, 4.5f, 5, 5.5f };
    protected float[] duration = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f };
    private readonly string[] typeNote = new string[] { "SimpleNote", "SimpleNote", "SimpleNote", "SimpleNote", "SpecialNote", "SliderNote", "SimpleNote", "SimpleNote", "SimpleNote", "SimpleNote" };

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region get information of game objects present in the scene 
        barLocation = GameObject.Find("Bar").transform.position.y; 
        #endregion

        #region Initialize variables to play music
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = (AudioClip)Resources.Load("auclair");
        musicStart = (spawningLocation - barLocation) / velocity;
        #endregion

        #region launch Display

        display.Velocity = velocity;
        display.SpawningLocation = spawningLocation; 
        display.SimpleNotePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SimpleNote.prefab", typeof(GameObject)); ;
        display.SliderNotePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SliderNote.prefab", typeof(GameObject)); ;
        display.SpecialNotePrefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SpecialNote.prefab", typeof(GameObject)); ;

        #endregion


    }

    // Update is called once per frame
    void Update()
    {
        #region Play Music 
        timing += Time.deltaTime;

        if (timing >= musicStart && !audiosource.isPlaying)
            audiosource.Play();



        #endregion

        #region call Display 
        
        while (counter < time.Length && timing >= time[counter])
        {
            Debug.Log("Display " + typeNote[counter]);

            if (typeNote[counter].ToString() == "SimpleNote")
                display.DisplayNote(typeNote[counter].ToString(), counter);
            else
                display.DisplayNote(typeNote[counter].ToString(), duration[counter], counter) ;

            counter += 1;

        }

        #endregion
    }

}