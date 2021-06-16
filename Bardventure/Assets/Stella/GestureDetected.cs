using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureDetected : MonoBehaviour
{
    public Text m_MyText;
    private SpecialNote test = new SpecialNote(0f,0,0f,0f,0);
    private GameObject gestureRecognition;
    private GestureTest[] myGestureTestCompTable;
    private GestureTest myGestureTestComp;


    public bool rightGesture(INote note) {

        if (note is SimpleNote) {
            m_MyText.text = "Simple Note detected!";
            if (Input.GetKey(KeyCode.Space)) //myGestureTestComp.SimpleGestureDetection(((SimpleNote)note).TrackSide)
            {
                m_MyText.text = "Right gesture for simple note";
                return true;
            }
        } else if (note is SliderNote)
        {
            m_MyText.text = "Slider Note detected!";
            if (Input.GetKey(KeyCode.A)) 
            {
                m_MyText.text = "Right gesture for slider note";
                return true;
            }
        } else if (note is SpecialNote) {
            m_MyText.text = "Special Note detected!";
            //TODO differentiate left and right notes
            //myGestureTestComp = myGestureTestCompTable[((SpecialNote)note).];
            if (myGestureTestComp.SpecialGestureDetection(((SpecialNote)note).Time))
            {
                m_MyText.text = "Right gesture for special note";
                return true;
            }
        }
        /*if (Input.GetKey(KeyCode.Space))
        {
            m_MyText.text = "My text has now changed.";
        }*/

        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        gestureRecognition = GameObject.Find("GestureTest");
        myGestureTestCompTable = gestureRecognition.GetComponents<GestureTest>();
        myGestureTestComp = myGestureTestCompTable[0];
        m_MyText.text = "No note detected";
        test.Time = 10f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //rightGesture(test);
    }
}
