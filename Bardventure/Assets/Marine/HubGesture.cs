using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubGesture : MonoBehaviour
{

    private GameObject gestureRecognition;
    private GestureTest[] myGestureTestCompTable;
    private GestureTest myGestureTestComp;
    //private int victoryCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        gestureRecognition = GameObject.Find("GestureTest");
        
        myGestureTestCompTable = gestureRecognition.GetComponents<GestureTest>();
        myGestureTestComp = myGestureTestCompTable[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (myGestureTestComp.getVictoryCounter() == 1)
        {
            //function pour ouvrir ta fenêtre
        }
        else { 
            //function pour la fermer ?
        }
        /*myGestureTestComp.fingerBones = new List<OVRBone>(myGestureTestComp.leftHandPrefab.GetComponent<OVRSkeleton>().Bones);
        Gesture currentGesture = myGestureTestComp.Recognize();
        bool hasRecognized = !currentGesture.Equals(new Gesture());
        if (hasRecognized && !currentGesture.Equals(myGestureTestComp.previousGesture))
        {
            Debug.Log("Gesture found:" + currentGesture.name);
            myGestureTestComp.previousGesture = currentGesture;
            currentGesture.onRecognized.Invoke();
        }*/
        //myGestureTestComp.HubGestureDetection();
    }
}
