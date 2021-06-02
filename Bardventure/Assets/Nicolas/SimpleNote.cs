using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : INote
{
    private int type; // 0 is a normal note | 1 is a dodge note

    private string gesture;
    private int points;
    private float time, velocity;
    private Material image;
    public int Type { get => type; set => type = value; }
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    public string Gesture { get => gesture; set => gesture = value; }

    ///<summary>
    /// Simple Note constructore
    ///</summary>
    public SimpleNote(int _type, int _points, float _time, float _velocity,int _gestureType)
    {
        type = _type;
        points = _points;
        time = _time;
        velocity = _velocity;
        if (_gestureType == 0) gesture = "Thumbs_up";
        if (_gestureType == 1) gesture = "Victory_sign";
        if (_gestureType == 2) gesture = "Closed";
        if (_gestureType == 3) gesture = "Open";
    }

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
