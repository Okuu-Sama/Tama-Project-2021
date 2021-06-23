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
    protected float velocity = 1.0f;
    protected float spawningLocation = 5.0f;
    protected float barLocation; 

    private int counter = 0;
    private Display display; 

    #region test sample
    private readonly float[] time = new float[] { 0, 0.5f, 1, 1.5f, 2, 3, 4, 4.5f, 5, 5.5f };
    protected float[] duration = new float[] { 0.5f, 0.5f, 0.5f, 0.5f, 1, 1, 0.5f, 0.5f, 0.5f, 0.5f };
    private readonly string[] typeNote = new string[] { "SimpleNote", "SimpleNote", "SimpleNote", "SimpleNote", "SliderNote", "SpecialNote", "SimpleNote", "SimpleNote", "SimpleNote", "SimpleNote" };

    #endregion

    // Start is called before the first frame update
    void Start()
    {

        #region Initialize variables to play music
        barLocation = GameObject.Find("Bar").transform.position.z;
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = (AudioClip)Resources.Load("auclair");
        musicStart = Mathf.Abs(-4 * Mathf.Log(spawningLocation) - barLocation) / velocity;
        #endregion

        #region launch Display
        display = new Display((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SimpleNote.prefab", typeof(GameObject)),
        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SpecialNote.prefab", typeof(GameObject)),
        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SliderNote.prefab", typeof(GameObject)), 
        velocity, spawningLocation, GameObject.Find("OVRCameraRig").transform.position);


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
           
            display.GetSliderNoteFinalPosition(typeNote[counter].ToString(), counter % 2); 

            if (typeNote[counter].ToString() == "SimpleNote")
                display.DisplayNote(typeNote[counter].ToString(), counter%2);
            else
                display.DisplayNote(typeNote[counter].ToString(), duration[counter], counter%2) ;

            counter += 1;

        }

        #endregion
    }

}