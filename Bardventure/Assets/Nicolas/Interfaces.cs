using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INote
{
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
