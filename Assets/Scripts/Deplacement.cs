using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deplacement : MonoBehaviour
{
    float ratio;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeight(float pitch)
    {
        float height = CvtPitchToHeight(pitch);


        float position_y = gameObject.transform.position.y;
        position_y = height;
    }

    private float CvtPitchToHeight(float pitch)
    {
        return (ratio * pitch);
    }
}
