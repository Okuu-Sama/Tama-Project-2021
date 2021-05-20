using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : Display
{
    public AudioSource audiosource;
    public float musicStart = 0;
    private float timing = 0; 
    


    // Start is called before the first frame update
    void Start()
    {
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = (AudioClip)Resources.Load("auclair"); 
 

        musicStart = (spawningLocation - barLocation) / velocity;
        
                                                                 



    }

    // Update is called once per frame
    void Update()
    {
        timing += Time.deltaTime; 

        if (timing >= musicStart && !audiosource.isPlaying)
                audiosource.Play(); 

        Debug.Log("Playing? "+audiosource.isPlaying + " " + timing); 




    }
}
