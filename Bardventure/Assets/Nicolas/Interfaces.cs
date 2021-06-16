using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INote
{
    /// <summary>
    /// Which track the note will appear on: 0 is right, 1 is left
    /// </summary>
    int TrackSide
    {
        get;
        set;
    }

    int Points
    {
        get;
        set;
    }

    float Time
    {
        get;
        set;
    }

    float Velocity
    {
        get;
        set;
    }

    Material Image
    {
        get;
        set;
    }

}
