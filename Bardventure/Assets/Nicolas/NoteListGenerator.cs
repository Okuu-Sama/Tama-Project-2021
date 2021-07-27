using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using UnityEngine.UI;

//Swap to using UnityEngine.Random because 20% 40% faster ?
//enum Gestures { Thumbs_up, Victory_sign, Closed, Open };
public static class NoteListGenerator
{
    public static void GenerateList(RhythmCore rhythmCore , ref Text debugTEXT)
    {
        Debug.Log("Static list generator properly called");
        //Gestures gesture = new Gestures();
        //var values = System.Enum.GetValues(typeof(Gestures));
        System.Random myrand = new System.Random();
        int track = 0;
        MidiFile mymidi = null;
        mymidi = MidiFile.Read("Assets/Nicolas/Audio/" + rhythmCore.getNameOfSong() + ".mid");
        if(mymidi == null)
        {
            debugTEXT.text = "midi failed to load";
        }
        debugTEXT.text = "midi loaded";
        NotesManager notesManager = mymidi.GetTrackChunks().Skip(1).First().ManageNotes();
        NotesCollection notesviolin = notesManager.Notes;
        IEnumerable<Note> listNoteviolin = notesviolin.ToList();
        IEnumerable<Note> listNote = mymidi.GetNotes();
        TempoMap tempomap = mymidi.GetTempoMap();
        var timesignature = tempomap.GetTimeSignatureChanges();
         //Add function call to get coordinate from display


        Debug.Log("timesig START");
        foreach (var timesig in timesignature)
        {
            Debug.Log(timesig.ToString());
            Debug.Log(timesig.Value.Numerator.ToString());
            Debug.Log(timesig.Value.Denominator.ToString());
        }
        Debug.Log("timesig END");


        foreach (Note note in listNote)
        {
            //gesture = (Gestures)values.GetValue(myrand.Next(values.Length));
            track = Random.Range(0, 2);
            Debug.Log("Note channel :" + note.Channel);

            if(note.Channel == 1)
            {
                if (note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f > 1.5f)
                {
                    Vector3[] ballscoordinates = rhythmCore.getDisplay().GetSliderNoteFinalPosition("SliderNote", track);
                    float timeOfBall = note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / (float)ballscoordinates.Length;
                    rhythmCore.addToNoteList(new SliderNote(ballscoordinates, timeOfBall, note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 100, note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1f, track));

                    /*for(int i = 1;i<6;i++)
                    {
                        var rad = 2 * Mathf.PI / 6 * i;
                        var vertical = Mathf.Sin(rad);
                        var horizontal = Mathf.Cos(rad);

                        Vector3 spherePosition = new Vector3(horizontal, 0, vertical);
                        sphereSpaceData[i - 1] = spherePosition;
                        // Add now slider note to list of note with the data
                    }*/
                }
                else if (note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f > 1f)
                    rhythmCore.addToNoteList(new SpecialNote(note.LengthAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 100, note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1f, track));
                else
                    rhythmCore.addToNoteList(new SimpleNote(0, 100, note.TimeAs<MetricTimeSpan>(tempomap).TotalMicroseconds / 1000000f, 1, track));
            }
        }
    }
}
