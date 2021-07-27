using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderNote : INote
{
    /// <summary>
    /// Struct representing the position where a sphere composing the slider is
    /// and the time at which the sphere is
    /// </summary>
    public struct SpaceTimeData
    {
        public Vector3 position;
        public float atTime;
    }

    public SpaceTimeData[] spheresData;

    private float duration;

    private int points,trackside;
    private float time, velocity;
    private Material image;
    public int Points { get => points; set => points = value; }
    public float Time { get => time; set => time = value; }
    public float Velocity { get => velocity; set => velocity = value; }
    public Material Image { get => image; set => throw new System.NotImplementedException(); }
    public float Duration { get => duration; set => duration = value; }
    public int TrackSide { get => trackside; set => trackside = value   ; }


    public SliderNote(Vector3[] _ballsCoordinates, float _timeForNote,float _duration ,int _points , float _time, float _velocity, int _track)
    {
        duration = _duration;
        points = _points;
        time = _time;
        velocity = _velocity;
        trackside = _track;
        float temp = _time;

        spheresData = new SpaceTimeData[_ballsCoordinates.Length];
        for (int i = 0; i < _ballsCoordinates.Length; ++i)
        {
            spheresData[i].position = _ballsCoordinates[i];
            spheresData[i].atTime = temp;
            temp += _timeForNote;
        }
    }


    public override string ToString()
    {
        return "note type: "+ GetType() +"duration: " + duration + " points: " + points + " time: " + time;
    }
}
