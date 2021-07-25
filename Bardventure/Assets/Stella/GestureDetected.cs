using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureDetected : MonoBehaviour
{
    public Text m_MyText;
    public int type ;
    public int points;
    public float time;
    public float velocity;
    public int track;
    public string noteName;
    public bool result=false;
    private SpecialNote test = new SpecialNote(10f, 0,0,0,0);
    private SimpleNote simpleNote;
    private GameObject gestureRecognition;
    private GameObject rhythmCoreObject;
    private RhythmCore rhythmCore;
    private GestureTest[] myGestureTestCompTable;
    private GestureTest myGestureTestComp;
    private int sliderIterator = 0;
    bool currentBallValidated = false;

    public bool rightGesture(INote note) {
        
        
        if (note is SimpleNote) {
            //m_MyText.text = "Simple Note detected!";
            if (myGestureTestComp.SimpleGestureDetection(((SimpleNote)note).TrackSide, ((SimpleNote)note).Time)) 
            {
                //m_MyText.text = "Right gesture for simple note";
                return true;
            }
        } else if (note is SliderNote)
        {
            //m_MyText.text = "Slider Note detected!";
            
            /*if (sliderIterator< ((SliderNote)note).spheresData.Length) {
                if ((rhythmCore.getTimeOfSong() > ((SliderNote)note).spheresData[sliderIterator].atTime + 0.1) && currentBallValidated == false)
                {
                    sliderIterator = 0;
                    return false;
                }
                currentBallValidated = false;
                if (myGestureTestComp.SliderGestureDetection(((SliderNote)note).TrackSide, ((SliderNote)note).Time, ((SliderNote)note).spheresData[sliderIterator].position[0], ((SliderNote)note).spheresData[sliderIterator].position[1], ((SliderNote)note).spheresData[sliderIterator].position[2])) {
                    sliderIterator++;
                    currentBallValidated = true;
                }
                if (sliderIterator == ((SliderNote)note).spheresData.Length && currentBallValidated==true)
                {
                    return true;
                }


            }
            /*if (myGestureTestComp.SliderGestureDetection(0,0)) 
            {
                m_MyText.text = "Right gesture for slider note";
                return true;
            }*/
        } else if (note is SpecialNote) {
            //m_MyText.text = "Special Note detected!";
            myGestureTestComp = myGestureTestCompTable[((SpecialNote)note).TrackSide];
            if (myGestureTestComp.SpecialGestureDetection(((SpecialNote)note).Duration, ((SpecialNote)note).Time, ((SpecialNote)note).TrackSide))
            {
                //m_MyText.text = "Right gesture for special note";
                return true;
            }
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            m_MyText.text = "My text has now changed.";
        }*/

        return false;
    }

    private INote initializeNote() {
        simpleNote = new SimpleNote(type, points, time, velocity, track);
        return simpleNote;
    }
    // Start is called before the first frame update
    void Start()
    {
        gestureRecognition = GameObject.Find("GestureTest");
        //rhythmCoreObject = GameObject.Find("RhythmCoreObj");
        //rhythmCore = rhythmCoreObject.GetComponent<RhythmCore>();
        myGestureTestCompTable = gestureRecognition.GetComponents<GestureTest>();
        myGestureTestComp = myGestureTestCompTable[0];
        m_MyText.text = "Nothing happened.";
        simpleNote = (SimpleNote) initializeNote();


    }

    // Update is called once per frame
    void Update()
    {
        if (result==false) {
            if ((myGestureTestComp.getSongPosition() < simpleNote.Time + 0.3) && !(rightGesture(simpleNote))) {
                result = false;
            }
            else {
                result = true;
                m_MyText.text = noteName;

            }
        }
    }
}
