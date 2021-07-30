using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class SceneSwitch : MonoBehaviour
{
    static public string currentScene; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (Input.GetKeyDown(KeyCode.Space) && currentScene != "Hub")
            SceneManager.LoadScene("Hub");
        else if (Input.GetKeyDown(KeyCode.Space) && currentScene != "TestTerrainSky")
            SceneManager.LoadScene("TestTerrainSky");


    }
}
