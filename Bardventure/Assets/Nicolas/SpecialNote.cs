using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialNote : INote
{
    private float duration;
    private bool type;

    private int points;
    private float time, velocity;
    private Material image;
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    public float Duration { get => duration; set => duration = value; }
    public bool Type { get => type; set => type = value; }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
