using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;

public class RhythmCore : MonoBehaviour
{

    public AudioSource audioSource;
    public Text songInfo;
    public Text noteInfo;
    public GameObject testSphere;
    private string nameOfSong;
    readonly float bpm = 120;
    float lastbeat;
    float crochet;
    float diffculty;    //Represent the velocity at which the note come to the player, defined when selecting diffulty of the song at song screen select
    Color red = new Color(1f,0f,0f,0f);
    Color blue = new Color(0f, 0f, 1f, 0f);
    int counter=0;
    float songposition = 0;
    float dsptimesong;
    string path;
    StreamWriter writer;
    int token = 0;

    private List<INote> notes = new List<INote>();
    private PointsManager playerScore;
    private int iterator = 0;

    private Display display;

    public string getNameOfSong()
    {
        return nameOfSong;
    }

    public float getSongPosition()
    {
        return songposition;
    }

    public void addToNoteList(INote note)
    {
        notes.Add(note);
    }

    public float getBallVelocity()
    {
        return display.Velocity;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.PlayDelayed(10f);
        nameOfSong = audioSource.clip.name;
        playerScore = new PointsManager();
        lastbeat = 0;
        crochet = 60f / bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        path = "Assets/Nicolas/Audio/The_First_Layer-Notes_Data.txt";
        writer = new StreamWriter(path, true);
        writer.WriteLine("Start: " + (AudioSettings.dspTime - dsptimesong).ToString());
        writer.WriteLine("Start: " + (AudioSettings.dspTime - dsptimesong).ToString());
        display = new Display((GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SimpleNote.prefab", typeof(GameObject)),
        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SpecialNote.prefab", typeof(GameObject)),
        (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Marine/SliderNote.prefab", typeof(GameObject)),
        1.0f, 10.0f, GameObject.Find("OVRCameraRig").transform.position);

        NoteListGenerator.GenerateList(this);
    }

    // Update is called once per frame
    void Update()
    {
        writer.Write("-");
        songposition = (float)(AudioSettings.dspTime - dsptimesong) * audioSource.pitch;
        songInfo.text = "Pitch of the song: " + audioSource.pitch.ToString() +
                        " Audio time (source):" + audioSource.time.ToString() +
                        "Audiotime(setting): " + AudioSettings.dspTime.ToString() +
                        " songpostion: " + songposition +
                        "Score test: " + playerScore.GetScore().ToString() +
                        "name of song: " + nameOfSong;
        if (songposition > lastbeat + crochet)
        {
            if (counter == 0)
            {

                testSphere.GetComponent<Renderer>().material.color = red;
                counter++;
            }
            else
            {
                testSphere.GetComponent<Renderer>().material.color = blue;
                counter--;
            }
            lastbeat += crochet;
        }

        if(notes != null && notes[iterator].Time <=  songposition)
        {
            //display.DisplayNote(notes[iterator].GetType(),notes[iterator].);
            //displayObj.GetComponent<Display>().DisplayNote();
            noteInfo.text = notes[iterator].ToString();
            if(notes[iterator] is SimpleNote)
            {
                display.DisplayNote(notes[iterator].GetType().ToString(), notes[iterator].TrackSide);
            }
            if(notes[iterator] is SpecialNote)
            {
                display.DisplayNote(notes[iterator].GetType().ToString(), (notes[iterator] as SpecialNote).Duration, notes[iterator].TrackSide);
            }
            iterator++;
            if (iterator == notes.Count) notes = null;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (token == 0)
            {
                writer.WriteLine("Beat: " + songposition.ToString());
                token++;
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            token = 0;
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerScore.ScoreUp(100);
            playerScore.MultiplierUp();
        }
    }
}
