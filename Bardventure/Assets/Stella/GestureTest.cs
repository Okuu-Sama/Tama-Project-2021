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

    bool firstCondition = false;
    bool secondCondition = false;
    bool thirdCondition = false;
    bool hasRecognizedClenched = false;
    bool hasRecognizedOpen = false;
    float openTime = 0;
    float closeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        previousGesture = new Gesture();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(debugMode && Input.GetKeyDown(KeyCode.Space))
        {
            fingerBones = new List<OVRBone>(skeleton.Bones);
            Save();
        }

        fingerBones = new List<OVRBone>(skeleton.Bones);
        specialGesture = SpecialGestureDetection();
        /*Gesture currentGesture = Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(previousGesture))
        {
            Debug.Log("Gesture found:" + currentGesture.name + "excuted for " + Time.timeSinceLevelLoad);
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


    public bool SpecialGestureDetection() 
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
            if (firstCondition)
            {
                if (hasRecognizedOpen)
                {
                    Debug.Log("Second condition validated");
                    closeTime = Time.time;
                    Debug.Log("Closed for " + (closeTime - openTime));
                    secondCondition = true;

                }
                else
                {
                    firstCondition = false;

                }
            }
            
            if (hasRecognizedClenched && !firstCondition)
            {
                openTime = Time.time;
                firstCondition = true;
                Debug.Log("First condition validated");

            }

            Debug.Log("First condition is: " + firstCondition);
            

            Debug.Log("Second condition is: " + secondCondition);
        }



        
        
        if (secondCondition) 
        {
            executed = true;
        }

        secondCondition = false;
        return executed;
    }
}
