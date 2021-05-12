using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : INote
{
    private bool type; // 0 is a normal note | 1 is a dodge note

    private int points;
    private float time, velocity;
    private Material image;
    public bool Type { get => type; set => type = value; }
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
