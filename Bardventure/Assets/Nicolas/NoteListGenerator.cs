using System.Collections.Generic;
using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;

//Swap to using UnityEngine.Random because 20% 40% faster ?
enum Gestures { Thumbs_up, Victory_sign, Closed, Open };
public static class NoteListGenerator
{
    public static void GenerateList(RhythmCore rhythmCore)
    {
        Debug.Log("Static list generator properly called");
   
        Gestures gesture = new Gestures();
        var values = System.Enum.GetValues(typeof(Gestures));
        System.Random myrand = new System.Random();
        int track = 0;
        MidiFile mymidi = MidiFile.Read("Assets/Nicolas/Audio/" + rhythmCore.getNameOfSong() + ".mid");
        IEnumerable<Note> listNote = mymidi.GetNotes();
        TempoMap tempomap = mymidi.GetTempoMap();

        foreach (Note note in listNote)
        {
            gesture = (Gestures)values.GetValue(myrand.Next(values.Length));
            track = Random.Range(0, 2);
            Debug.Log("Note channel :" + note.Channel);

            if (note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f > 1f)
                rhythmCore.addToNoteList(new SpecialNote(note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 0, (int)gesture, 100, note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1f, track));
            else
                rhythmCore.addToNoteList(new SimpleNote(0, 100, note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1, track));
        }
    }
}
