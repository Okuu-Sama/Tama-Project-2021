using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmGameLoop : MonoBehaviour
{
    public enum GameState { WAIT_START, IN_PLAY, SHOW_RESULT, P };
    GameState gamestate, former_gamestate;

    // Start is called before the first frame update
    void Start()
    {
        former_gamestate = GameState.WAIT_START;
        gamestate = GameState.WAIT_START;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gamestate++;
            if(gamestate == GameState.P)
                gamestate = 0;
        }
        if( gamestate != former_gamestate )
        {
            Debug.Log(gamestate);
        }
        former_gamestate = gamestate;
    }
}
