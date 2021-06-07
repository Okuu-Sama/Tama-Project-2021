using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNote : INote
{
    private int type; // 0 is a normal note | 1 is a dodge note

    private int trackside; // 0 for right and 1 for left
    private int points;
    private float time; // Time at which the note should appear
    private float velocity;  // Velocity at which the note is travelling to the bar
    private Material image;
    public int Type { get => type; set => type = value; }
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    public int TrackSide { get => trackside; set => trackside = value; }

    ///<summary>
    /// Simple Note constructore
    ///</summary>
    public SimpleNote(int _type, int _points, float _time, float _velocity,int _track)
    {
        type = _type;
        points = _points;
        time = _time;
        velocity = _velocity;
        trackside = _track;
    }

    public override string ToString()
    {
        return "note type: " + GetType() + " points is: " + points + " time is: " + time + " velocity: " + velocity + " track: " + trackside;
    }
}
