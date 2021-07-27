using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_for_test_build : MonoBehaviour
{
    GameObject thiscube;
    Color blue = new Color(0f, 0f, 1f);
    Color red = new Color(1f, 0f, 0f);
    Renderer thisrender;
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        thisrender = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(i);
        if(i < 2000)
        {
            thisrender.material.color = blue;
            i++;
        }else
        {
            thisrender.material.color = red;
            i++;
            if (i == 4000)
            {
                i = 0;
            }
        }
    }
}
