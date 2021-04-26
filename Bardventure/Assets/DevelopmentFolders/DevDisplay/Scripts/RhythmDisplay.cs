using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RhythmDisplay : MonoBehaviour
{
    public Text stateText;
    // Start is called before the first frame update
    void Start()
    {
        ShowStatus( RhythmGameLoop.GameState.WAIT_START );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowStatus( RhythmGameLoop.GameState gs )
    {
        stateText.text = gs.ToString();
    }
}
