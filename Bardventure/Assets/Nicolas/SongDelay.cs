using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayDelayed(10f);
        Debug.Log("properly delayed");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}