using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialNote : INote
{
    private float duration;  // The duration of the note (how long it appear on screen)

    private int trackside; // 0 for right and 1 for left
    private int points;
    private float time; // Time at which the note should appear
    private float velocity;  // Velocity at which the note is travelling to the bar
    private Material image;
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    public float Duration { get => duration; set => duration = value; }
    public int TrackSide { get => trackside; set => trackside = value; }

    public SpecialNote(float _duration, int _points, float _time, float _velocity, int _track)
    {
        duration = _duration;
        points = _points;
        time = _time;
        velocity = _velocity;
        trackside = _track;
    }

    public override string ToString()
    {
        return "note type: " + GetType() + " points is: " + points + " time is: " + time + " track: " + trackside + " gesture: " + gesture;
    }
}
