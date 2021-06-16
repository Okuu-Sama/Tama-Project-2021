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
    private bool timingRespectedCondition = false;
    private bool hasRecognizedClenched = false;
    private bool hasRecognizedOpen = false;
    private float closingTime = 0;
    private float openingTime = 0;

    //Roration tests for simple note

    private GameObject leftHandPrefab;
    private GameObject rightHandPrefab;
    private GameObject cube;
    private GameObject cube2;
    private bool handForward = false;
    bool firstTimeHandForward = false;
    float firstZPosition = -1000;


    // Start is called before the first frame update
    void Start()
    {
        previousGesture = new Gesture();
        leftHandPrefab = GameObject.Find("OVRHandPrefabLeft");
        rightHandPrefab = GameObject.Find("OVRHandPrefabRight");
        cube = GameObject.Find("Cube");
        cube2 = GameObject.Find("Cube2");

    }

    // Update is called once per frame
    void Update()
    {
        if(debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            fingerBones = new List<OVRBone>(skeleton.Bones);
            Save();
        }

        
        SliderGestureDetection(0, 0);
        /*cube.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        cube2.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        fingerBones = new List<OVRBone>(skeleton.Bones);
        if (SimpleGestureDetection(1)) {
            cube.GetComponent<Renderer>().material.color = new Color(0, 255, 0);
        }
        if (SimpleGestureDetection(0))
        {
            cube2.GetComponent<Renderer>().material.color = new Color(255, 0, 0);
        }*/

        //specialGesture = SpecialGestureDetection();
        /*Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("Gesture found:" + currentGesture.name);
            previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }
        else 
        {
            Debug.Log("No gesture detected.");
        }*/
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

    private Gesture Recognize() 
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


    public bool SpecialGestureDetection(float time)
    {
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

                    Debug.Log("Comparing if " + (openingTime - closingTime) + ">" + (time * 1.1) + " and " + (openingTime - closingTime) + "<" + (time * 0.9) );
                    if ((openingTime - closingTime) < (time * 1.1) && (openingTime - closingTime) > (time * 0.9))
                    { //10% TIMING error
                        Debug.Log("Third condition validated");
                        timingRespectedCondition = true;
                    }

                }
                else
                {
                    wasClosedCondition = false;

                }
            }
            
            if (hasRecognizedClenched && !wasClosedCondition)
            {
                closingTime = Time.time;
                wasClosedCondition = true;
                Debug.Log("First condition validated");
                
                

            }


            Debug.Log("First condition is: " + wasClosedCondition);
            

            Debug.Log("Second condition is: " + isOpenedAfterCondition);
            Debug.Log("Third condition is: " + timingRespectedCondition);
        }



        
        
        if (isOpenedAfterCondition && timingRespectedCondition) 
        {
            executed = true;
        }

        isOpenedAfterCondition = false;
        timingRespectedCondition = false;
        return executed;
    }


    public bool SimpleGestureDetection(int trackSide) {
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
            if (currentZPosition>= (firstZPosition + 0.1)) {
                //Debug.Log("MOVED FORWARD");
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

    public bool SliderGestureDetection(int trackSide, float time)
    {
        bool executed = false;
        float xPosition = 0;
        float yPosition = 0;
        float zPosition = 0;

        

        if (trackSide == 0)
        {
            fingerBones = new List<OVRBone>(rightHandPrefab.GetComponent<OVRSkeleton>().Bones);
            xPosition = rightHandPrefab.transform.position.x;
            yPosition = rightHandPrefab.transform.position.y;
            zPosition = rightHandPrefab.transform.position.z;
            
        }
        else
        {
            fingerBones = new List<OVRBone>(leftHandPrefab.GetComponent<OVRSkeleton>().Bones);
            xPosition = leftHandPrefab.transform.position.x;
            yPosition = leftHandPrefab.transform.position.y;
            zPosition = leftHandPrefab.transform.position.z;
        }
        float xPalmPosition = (fingerBones[3].Transform.position.x + fingerBones[15].Transform.position.x) / 2;
        float yPalmPosition = (fingerBones[3].Transform.position.y + fingerBones[15].Transform.position.y) / 2;
        float zPalmPosition = fingerBones[15].Transform.position.z;

        Debug.Log("Wrist x :" + xPosition + " y :" + yPosition + " z :" + zPosition);
        Debug.Log("Palm x :" + xPalmPosition + " y :" + yPalmPosition + " z :" + zPalmPosition);

        return executed;
    }

    }
