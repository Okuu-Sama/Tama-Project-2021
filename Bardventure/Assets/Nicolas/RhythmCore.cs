using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class RhythmCore : MonoBehaviour
{

    public AudioSource audioSource;
    public Text songInfo;
    public Text noteInfo;
    public Text scoreInfo;
    public Text gameplayInfo;
    public Text debugInfo;
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
    int combo = 0;
    INote previousNote;
    bool successHit = false;

    private List<INote> notes = new List<INote>();
    private List<INote> notesForDetection = new List<INote>();
    private PointsManager playerScore;
    private int iteratorDisplay = 0;
    private int iteratorDetection = 0;

    private Display display;
    private GestureDetected gestureDetection;

    OVRPose vRPose;
    GameObject currentHDMObj;

    public string getNameOfSong()
    {
        return nameOfSong;
    }

    /// <summary>
    /// Get the absolute time of the song. Start when the scene start.
    /// </summary>
    /// <returns></returns>
    public float getSongPosition()
    {
        return songposition;
    }

    /// <summary>
    /// Get the relative time of the song. Start when the song audio start.
    /// </summary>
    /// <returns></returns>
    public float getTimeOfSong()
    {
        return audioSource.time;
    }

    public void addToNoteList(INote note)
    {
        notes.Add(note);
    }

    public float getBallVelocity()
    {
        return display.Velocity;
    }

    public Display getDisplay()
    {
        return display;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioSource.PlayDelayed(10f);
        nameOfSong = audioSource.clip.name;
        playerScore = new PointsManager();
        scoreInfo.text = playerScore.GetScore().ToString();
        lastbeat = 0;
        crochet = 60f / bpm;
        dsptimesong = (float)AudioSettings.dspTime;
        path = "Assets/Nicolas/Audio/The_First_Layer-Notes_Data.txt";
        writer = new StreamWriter(path, true);
        writer.WriteLine("Start: " + (AudioSettings.dspTime - dsptimesong).ToString());
        writer.WriteLine("Start: " + (AudioSettings.dspTime - dsptimesong).ToString());
        display = new Display( Resources.Load("Notes/SimpleNote") as GameObject ,
         Resources.Load("Notes/SpecialNote") as GameObject,
         Resources.Load("Notes/SliderNote") as GameObject,
        1.0f, 5.0f, GameObject.Find("OVRCameraRig").transform.position);

        GameObject gestureObj = GameObject.Find("GestureDetected");
        gestureDetection = gestureObj.GetComponent<GestureDetected>();

        NoteListGenerator.GenerateList(this, ref debugInfo);
        notesForDetection = notes.ToList();
        previousNote = notesForDetection[0];

        //vRPose = OVRManager.tracker.GetPose();
        //currentHDMObj = GameObject.Find("OVRCameraRig");
        //Vector3 vectortest = new Vector3(-8f, 1.3f, 0.48f);
        //currentHDMObj.transform.position = vectortest;
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

        if(notes != null && notes[iteratorDisplay].Time <=  songposition)
        {
            //display.DisplayNote(notes[iterator].GetType(),notes[iterator].);
            //displayObj.GetComponent<Display>().DisplayNote();
            noteInfo.text = notes[iteratorDisplay].ToString();
            if(notes[iteratorDisplay] is SimpleNote)
            {
                display.DisplayNote(notes[iteratorDisplay].GetType().ToString(), notes[iteratorDisplay].TrackSide);
            }
            if(notes[iteratorDisplay] is SpecialNote)
            {
                display.DisplayNote(notes[iteratorDisplay].GetType().ToString(), (notes[iteratorDisplay] as SpecialNote).Duration, notes[iteratorDisplay].TrackSide);
            }
            if(notes[iteratorDisplay] is SliderNote)
            {
                display.DisplayNote(notes[iteratorDisplay].GetType().ToString(), (notes[iteratorDisplay] as SliderNote).Duration, notes[iteratorDisplay].TrackSide);
            }
            iteratorDisplay++;
            if (iteratorDisplay == notes.Count) notes = null;
        }

        if (notesForDetection != null && notesForDetection[iteratorDetection].Time <= audioSource.time)
        {
            Debug.Log("note found for detection");
            Debug.Log("current index:"+iteratorDetection);
            if (gestureDetection.rightGesture(notesForDetection[iteratorDetection]))
            {
                //Add success animation
                gameplayInfo.text = "NOTE HIT";
                successHit = true;
                playerScore.ScoreUp(notesForDetection[iteratorDetection].Points);
                combo++;
                if (combo % 10 == 0) playerScore.MultiplierUp();
                //previousNote = notesForDetection[iteratorDetection];
            }
            if(notesForDetection[iteratorDetection].Time + 0.24f < audioSource.time && !successHit)
            {
                gameplayInfo.text = "NOTE MISSED";
                combo = 0;
                playerScore.ResetMultiplier();
            }
            if (iteratorDetection == notesForDetection.Count) notesForDetection = null;
            if (notesForDetection != null )
            {
                if(iteratorDetection == notesForDetection.Count - 1)
                {
                    notesForDetection = null;
                }
                else if(notesForDetection[iteratorDetection + 1] != null && notesForDetection[iteratorDetection + 1].Time <= audioSource.time - 0.25f)
                {
                    previousNote = notesForDetection[iteratorDetection];
                    iteratorDetection++;
                    successHit = false;
                }
            }
                
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
