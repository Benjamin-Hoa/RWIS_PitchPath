using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//using static UnityEditor.PlayerSettings;

public class Deplacement : MonoBehaviour
{
    float ratio;
    public float pitch = 210;
    float[] frequencyList; //list of frequencies
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameObject.FindWithTag("Path"));
        frequencyList = GameObject.FindWithTag("Path").transform.GetComponent<PathManager>().notes_freq;
    }

    // Update is called once per frame
    void Update()
    { //DebugMode

        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y--;
            this.transform.position = position;
        }
        */

        //normal mode
        CvtPitchToHeight(pitch);


    }
    void FixedUpdate()
    {
    }

    private void CvtPitchToHeight(float pitch)
    {
        float newPos;
        newPos = 12f * Mathf.Log(pitch / frequencyList[6] , 2);//calculating the postion with the formula for linear scale between frequency and notes
        newPos *= (4f / 6f);
        newPos = Mathf.Min(newPos, 5);
        newPos = Mathf.Max(newPos, -5);
        Vector3 res = new Vector3(transform.position.x, newPos, transform.position.z);
        transform.position = res;


    }

}
