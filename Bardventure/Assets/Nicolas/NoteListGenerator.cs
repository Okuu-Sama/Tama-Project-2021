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


public class NoteListGenerator : MonoBehaviour
{

    public string nameOfSong;
    private MidiFile myMidiFile;
    private NotesManager notesManager;
    private NotesCollection notes;
    private IEnumerable<Note> listNote;
    private TempoMap tempomap;
    private int notecounter = 0;
    private GameObject rhythmCoreObj;
    private RhythmCore rhythmCoreComp;

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

        rhythmCoreObj = GameObject.FindWithTag("RhythmCore");
        if (rhythmCoreObj != null) Debug.Log("Core not found");
        rhythmCoreComp = rhythmCoreObj.GetComponent<RhythmCore>();
        if (rhythmCoreComp == null) Debug.Log("Error getting attached script");
        

        //myMidiFile = MidiFile.Read("Assets/Nicolas/Audio/"+nameOfSong+".mid");
        myMidiFile = MidiFile.Read("Assets/Nicolas/Audio/auclair.mid");
        tempomap = myMidiFile.GetTempoMap();
        notesManager = myMidiFile.GetTrackChunks().First().ManageNotes();
        notes = notesManager.Notes;
        listNote = myMidiFile.GetNotes();
        Debug.Log("testlistnote");
        Debug.Log(listNote.Count().ToString());
        foreach (Note note in listNote)
        {
            Debug.Log(">> Note name:" + note.NoteName + note.Octave);
            Debug.Log(">>> Note length(?):" + note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f);
            Debug.Log(">>>> Note time(?):" + note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f);
            Debug.Log("note counter:" + notecounter);
            notecounter++;

            rhythmCoreComp.notes.Add(new SimpleNote(0,100, note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f,1));
        }
        Debug.Log(rhythmCoreComp.notes.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
