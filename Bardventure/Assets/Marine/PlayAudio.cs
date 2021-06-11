using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : Display
{
    private AudioSource audiosource;
    private float musicStart = 0;
    private float timing = 0; 
    


    // Start is called before the first frame update
    void Start()
    {
        #region Initialize variables to play music
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = (AudioClip)Resources.Load("auclair"); 
        musicStart = (spawningLocation - barLocation) / velocity;
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


    }
}
