using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Standards;
using Melanchall.DryWetMidi.Tools;

//remove monobehaviour and instantiate class in game loop ?
//Swap to using UnityEngine.Random because 20% 40% faster ?

public class NoteListGenerator : MonoBehaviour
{

    public string nameOfSong;
    private MidiFile myMidiFile;
    private IEnumerable<Note> listNote;
    private TempoMap tempomap;
    private GameObject rhythmCoreObj;
    private RhythmCore rhythmCoreComp;
    
    private enum Gestures {Thumbs_up,Victory_sign,Closed,Open};
    //private ArrayList values;   

    // Start is called before the first frame update
    void Start()
    {
        /*
         * TODO:
         *      > Extract midi file name from Core
         *      > Get the list of note to fill from Core
         *      > Randomize type of notes
         *      > Manage points
         *      > Implement points multiplier
         */

        // FINISH GESTURE IN NOTE USING RANDOM
        Gestures gesture = new Gestures();
        var values = System.Enum.GetValues(typeof(Gestures));
        System.Random myrand = new System.Random();
        

        rhythmCoreObj = GameObject.FindWithTag("RhythmCore");
        if (rhythmCoreObj != null) Debug.Log("Core not found");
        rhythmCoreComp = rhythmCoreObj.GetComponent<RhythmCore>();
        if (rhythmCoreComp == null) Debug.Log("Error getting attached script");
        

        //myMidiFile = MidiFile.Read("Assets/Nicolas/Audio/"+nameOfSong+".mid");
        myMidiFile = MidiFile.Read("Assets/Nicolas/Audio/auclair.mid");
        tempomap = myMidiFile.GetTempoMap();
        listNote = myMidiFile.GetNotes();
        foreach (Note note in listNote)
        {
            gesture = (Gestures)values.GetValue(myrand.Next(values.Length));
            Debug.Log("GESTURE IS: " + gesture + " IN INT FORM: " + (int)gesture);
            rhythmCoreComp.addToNoteList(new SimpleNote(0, 100, note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1, (int)gesture));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
