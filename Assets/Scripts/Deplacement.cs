using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
//using static UnityEditor.PlayerSettings;

public class Deplacement : MonoBehaviour
{
    float ratio;
    public float pitch = 210;
    // Start is called before the first frame update
    void Start()
    {

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

        newPos = 12f * Mathf.Log(pitch / 440f, 2);//calculating the postion with the formula for linear scale between frequency and notes
        newPos += 11.5f;
        newPos = (2f / 3f) * newPos - 4f;
       //Debug.Log(newPos);
        newPos = Mathf.Min(newPos, 5);
        newPos = Mathf.Max(newPos, -5);
        Vector3 res = new Vector3(transform.position.x, newPos, transform.position.z);
        transform.position = res;


    }
    /*IEnumerator Movement(Vector3 endPoint)
    {
        transform.position = Vector3.MoveTowards(transform.position, endPoint, 0.1f);
        return null;
    }*/
}
