using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager
{
    private int _score;
    private float _multiplier;

    public PointsManager()
    {
        _score = 0;
        _multiplier = 1f;
    }

    public int GetScore()
    {
        return _score;
    }

    public void MultiplierUp()
    {
        _multiplier = _multiplier + 0.2f;
    }

    public void ScoreUp(int points)
    {
        _score = (int)((_score + points) * _multiplier);
    }

}
