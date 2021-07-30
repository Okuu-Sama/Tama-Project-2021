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
        float barLocation = GameObject.Find("Bar").transform.position.z;
        float velocity = rhythmCore.getBallVelocity();
        float delay = Mathf.Abs(4 * Mathf.Log(rhythmCore.getDisplay().SpawningLocation) - barLocation) / rhythmCore.getDisplay().Velocity;
        audioSource.PlayDelayed(delay);
        //audioSource.PlayDelayed(9f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
