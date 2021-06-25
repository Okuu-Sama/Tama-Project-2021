using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        GameObject Rcore = GameObject.FindWithTag("RhythmCore");
        Debug.Log(Rcore);
        RhythmCore rhythmCore = Rcore.GetComponent<RhythmCore>();
        float velocity = rhythmCore.getBallVelocity();
        audioSource.PlayDelayed(9*velocity);
        //audioSource.PlayDelayed(9f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
