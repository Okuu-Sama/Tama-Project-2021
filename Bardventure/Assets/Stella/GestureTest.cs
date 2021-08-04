using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Following a tutorial for gesture detection to try things out

[System.Serializable]
public struct Gesture 
{
    public string name;
    public List<Vector3> fingerDatas;
    public UnityEvent onRecognized;
}
public class GestureTest: MonoBehaviour
{
    //Constant variables
    private const float specialDurationMargin = 0.15f;
    private const float SimplePositionMargin = 0.1f;
    private const float ballRadius = 0.018f;
    private const float sliderDetectionRadius = 0.04f;
    private const float sliderTimingMargin = 0.1f;
    private const float simpleTimingMargin = 0.3f;
    private const float specialTimingMargin = 0.5f;

    //Recognize and Save variables
    public float threshold = 0.1f;
    public OVRSkeleton skeleton;
    public List<OVRBone> fingerBones;
    public List<Gesture> gestures;
    public bool debugMode = true;
    public bool specialGesture;
    public Gesture previousGesture;

    //Special gesture variables

    private bool wasClosedCondition = false;
    private bool isOpenedAfterCondition = false;
    private bool durationRespectedCondition = false;
    private bool timingRespectedCondition = false;
    private bool hasRecognizedClenched = false;
    private bool hasRecognizedOpen = false;
    private float closingTime = 0;
    private float openingTime = 0;

    //Roration tests for simple note

    public GameObject leftHandPrefab;
    private GameObject rightHandPrefab;
    private GameObject cube;
    private GameObject cube2;
    private bool handForward = false;
    bool firstTimeHandForward = false;
    float firstZPosition = -1000;

    //Slider gesture variables
    //float songposition = 0;
    float dsptimesong;
    public AudioSource audioSource;
    private GameObject sphere;

    //Song position variables
    private GameObject rhythmCoreObject;
    private RhythmCore rhythmCore;

    //Hub
    public int victoryCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousGesture = new Gesture();
        leftHandPrefab = GameObject.Find("OVRHandPrefabLeft");
        rightHandPrefab = GameObject.Find("OVRHandPrefabRight");
        cube = GameObject.Find("Cube");
        cube2 = GameObject.Find("Cube2");
        sphere = GameObject.Find("Sphere");
        //dsptimesong = (float)AudioSettings.dspTime;
        rhythmCoreObject = GameObject.Find("RhythmCoreObj");
        if (rhythmCore!=null) { rhythmCore = rhythmCoreObject.GetComponent<RhythmCore>(); }
        


    }

    // Update is called once per frame
    void Update()
    {
        /*if(debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            fingerBones = new List<OVRBone>(skeleton.Bones);
            Save();
        }*/
        //songposition = (float)(AudioSettings.dspTime - dsptimesong);
        //Debug.Log(songposition);
        /*sphere.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        if (SliderGestureDetection(0, 5f, 0, 1, 0.2f))
        {
            sphere.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        }*/

        /*cube.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        cube2.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        fingerBones = new List<OVRBone>(skeleton.Bones);*/
        /*if (SimpleGestureDetection(1, 10f)) {
            cube.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        if (SimpleGestureDetection(0, 10f))
        {
            cube2.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }*/

        //specialGesture = SpecialGestureDetection(5f, 10f,0);
        /*fingerBones = new List<OVRBone>(leftHandPrefab.GetComponent<OVRSkeleton>().Bones);
        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("Gesture found:" + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }*/
        /*else 
        {
            Debug.Log("No gesture detected.");
        }*/
        if (GameObject.Find("HubGesture")!=null) {

            HubGestureDetection();
            
        }
        
    }


    public int getVictoryCounter() {
        return victoryCounter;
    }
    private void Save()
    {
        Gesture g = new Gesture();
        g.name = "New Gesture";
        List<Vector3> data = new List<Vector3>();
        foreach(var bone in fingerBones)
        {
            //finger position relative to root
            data.Add(skeleton.transform.InverseTransformPoint(bone.Transform.position));
        }
        g.fingerDatas = data;
        gestures.Add(g);
    }

    public Gesture Recognize() 
    {
        Gesture currentGesture = new Gesture();
        float currentMin = Mathf.Infinity;

        foreach (var gesture in gestures) 
        {
            float sumDistance = 0;
            bool isDiscarded = false;
            for(int i = 0; i< fingerBones.Count; i++)
            {
                Vector3 currentData = skeleton.transform.InverseTransformPoint(fingerBones[i].Transform.position);
                float distance = Vector3.Distance(currentData, gesture.fingerDatas[i]);
                
                if(distance>threshold)
                {
                    isDiscarded = true; //pose not recognized
                    break;
                }

                sumDistance += distance;
            }
            if (!isDiscarded && sumDistance < currentMin) 
            {
                currentMin = sumDistance;
                currentGesture = gesture;
            }
        }

        return currentGesture;
    }


    public void HubGestureDetection() {
        fingerBones = new List<OVRBone>(leftHandPrefab.GetComponent<OVRSkeleton>().Bones);
        Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture) )
        {
            if (currentGesture.Equals(gestures[1])) {
                if (victoryCounter==0) {
                    victoryCounter = 1;
                }
                else
                {
                    victoryCounter = 0;
                }
            }
            
            //Debug.Log("Gesture found:" + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }
    }

    public bool SpecialGestureDetection(float duration, float time, int trackSide)
    {
        if (trackSide == 0)
        {
            fingerBones = new List<OVRBone>(rightHandPrefab.GetComponent<OVRSkeleton>().Bones);

        }
        else
        {
            fingerBones = new List<OVRBone>(leftHandPrefab.GetComponent<OVRSkeleton>().Bones);

        }
        bool executed = false;

        //first = fist clenched, second = open just after, third = duration right
        Gesture currentGesture = Recognize();
        
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            hasRecognizedClenched = currentGesture.Equals(gestures[2]);
            hasRecognizedOpen = currentGesture.Equals(gestures[3]);
            Debug.Log("Gesture found:" + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
            if (wasClosedCondition)
            {
                if (hasRecognizedOpen)
                {
                    Debug.Log("Second condition validated");
                    openingTime = Time.time;
                    Debug.Log("Closed for " + (openingTime - closingTime));
                    isOpenedAfterCondition = true;

                    //Debug.Log("Comparing if " + (openingTime - closingTime) + ">" + (duration * (1+specialDurationMargin)) + " and " + (openingTime - closingTime) + "<" + (duration * (1-specialDurationMargin)) );
                    if (((openingTime - closingTime) < (duration * (1+specialDurationMargin))) && ((openingTime - closingTime) > (duration * (1-specialDurationMargin))))
                    { //15% DURATION error

                        Debug.Log("Third condition validated");
                        durationRespectedCondition = true;
                    }

                }
                else
                {
                    wasClosedCondition = false;
                    timingRespectedCondition = false;

                }
            }
            
            if (hasRecognizedClenched && !wasClosedCondition)
            {
                if ((((time + specialTimingMargin) > rhythmCore.getTimeOfSong()) && ((time - specialTimingMargin) < rhythmCore.getTimeOfSong())))
                {
                    timingRespectedCondition = true;
                }
                closingTime = Time.time;
                wasClosedCondition = true;
                Debug.Log("First condition validated");
                


            }


            Debug.Log("First condition is: " + wasClosedCondition);
            

            Debug.Log("Second condition is: " + isOpenedAfterCondition);
            Debug.Log("Third condition is: " + durationRespectedCondition);
        }



        
        
        if (isOpenedAfterCondition && durationRespectedCondition && timingRespectedCondition) 
        {
            Debug.Log("EVERY CONDITIONS ARE RESPECTED");
            executed = true;
        }

        isOpenedAfterCondition = false;
        durationRespectedCondition = false;
        
        return executed;
    }


    public bool SimpleGestureDetection(int trackSide, float time) {
        bool executed = false;
        
       
        float xRotation = 0;
        float yRotation = 0;
        float zRotation = 0;

        float currentZPosition = 0;

        
        if (trackSide == 0)
         {
              currentZPosition = rightHandPrefab.transform.position.z;
         }
        else
         {
              currentZPosition = leftHandPrefab.transform.position.z;
         }
        if (handForward==true)
        {
            //Debug.Log("STILL FORWARD, currentZPosition = " + currentZPosition + "");
            if (currentZPosition>= (firstZPosition + SimplePositionMargin) && (((time + simpleTimingMargin) > rhythmCore.getTimeOfSong()) && ((time - simpleTimingMargin) < rhythmCore.getTimeOfSong()))) {
                Debug.Log("MOVED FORWARD");
                executed = true;
            }
            //TODO detect moving forward??

        }


        //Right hand
        if (trackSide==0) {
            xRotation = rightHandPrefab.transform.rotation.eulerAngles.x;
            yRotation = rightHandPrefab.transform.rotation.eulerAngles.y;
            zRotation = rightHandPrefab.transform.rotation.eulerAngles.z;

            
            if (xRotation < 0)
            {
                xRotation = xRotation + 360;
            }
            if (yRotation < 0)
            {
                yRotation = yRotation + 360;
            }
            if (zRotation < 0)
            {
                zRotation = zRotation + 360;
            }
            //Debug.Log("x :" + rightHandPrefab.transform.rotation.eulerAngles.x + " y :" + rightHandPrefab.transform.rotation.eulerAngles.y + " z :" + rightHandPrefab.transform.rotation.eulerAngles.z);
            if (((xRotation > 0 && xRotation < 20) || (xRotation < 360 && xRotation > 340)) && ((yRotation < 110 && yRotation > 70)) && ((zRotation < 290 && zRotation > 250))) {
                handForward = true;
                //Debug.Log("RIGHT HAND FACING FORWARD");
                if (firstTimeHandForward==false) {
                    firstTimeHandForward = true;
                    firstZPosition = rightHandPrefab.transform.position.z;
                }
                
                
            }
            else {
                //Debug.Log("RIGHT HAND NOT FACING FORWARD");
                firstTimeHandForward = false;
                handForward = false;
                firstZPosition = -1000;
            }
        }
        else //Left hand
        {
            xRotation = leftHandPrefab.transform.rotation.eulerAngles.x;
            yRotation = leftHandPrefab.transform.rotation.eulerAngles.y;
            zRotation = leftHandPrefab.transform.rotation.eulerAngles.z;
            if (xRotation < 0)
            {
                xRotation = xRotation + 360;
            }
            if (yRotation < 0)
            {
                yRotation = yRotation + 360;
            }
            if (zRotation < 0)
            {
                zRotation = zRotation + 360;
            }
            //Debug.Log("x :" + leftHandPrefab.transform.rotation.eulerAngles.x + " y :" + leftHandPrefab.transform.rotation.eulerAngles.y + " z :" + leftHandPrefab.transform.rotation.eulerAngles.z);

            if (((xRotation > 0 && xRotation < 20) || (xRotation < 360 && xRotation > 340)) && ((yRotation < 110 && yRotation > 70)) && ((zRotation < 110 && zRotation > 70)))
            {
                handForward = true;
                //Debug.Log("LEFT HAND FACING FORWARD");
                if (firstTimeHandForward == false)
                {
                    firstTimeHandForward = true;
                    firstZPosition = leftHandPrefab.transform.position.z;
                    //Debug.Log("INITIAL Z POSITION = " + firstZPosition);
                }
            }
            else
            {
                //Debug.Log("LEFT HAND NOT FACING FORWARD");
                handForward = false;
                firstTimeHandForward = false;
                firstZPosition = -1000;
            }
        }
        return executed;

    }

    public bool SliderGestureDetection(int trackSide, float time, float xBallPosition, float yBallPosition, float zBallPosition)
    {
        bool executed = false;
        float xPalmPosition = 0;
        float yPalmPosition = 0;
        float zPalmPosition = 0;

        
        if (((time + sliderTimingMargin) > rhythmCore.getTimeOfSong()) && ((time - sliderTimingMargin) < rhythmCore.getTimeOfSong()))
        {
            Debug.Log("IN THE BIG IF CONDITION");
            if (trackSide == 0)
            {
                fingerBones = new List<OVRBone>(rightHandPrefab.GetComponent<OVRSkeleton>().Bones);

            }
            else
            {
                fingerBones = new List<OVRBone>(leftHandPrefab.GetComponent<OVRSkeleton>().Bones);

            }
            xPalmPosition = (fingerBones[9].Transform.position.x);
            yPalmPosition = (fingerBones[3].Transform.position.y + fingerBones[15].Transform.position.y) / 2;
            zPalmPosition = fingerBones[15].Transform.position.z;

            Debug.Log("Palm x :" + xPalmPosition + " y :" + yPalmPosition + " z :" + zPalmPosition);

            if (!(System.Math.Sqrt((xBallPosition - xPalmPosition) * (xBallPosition - xPalmPosition) + (yBallPosition - yPalmPosition) * (yBallPosition - yPalmPosition) + (zBallPosition - zPalmPosition) * (zBallPosition - zPalmPosition)) > (ballRadius + sliderDetectionRadius)))
            {
                executed = true;
            }
        }
        if (executed) 
        {
            Debug.Log("COLLISION");
        }
        
        return executed;
    }

    }
