using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePoint : MonoBehaviour
{
    float height;//Y position value of the note, depends on the frequency
    float freq;//frequency of the note
    float pos;//position of the note
    [SerializeField] float speedValue; // change the speed of the notes
    Color color;//Color of the point, depends on the frequency
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(transform.position.x - speedValue,transform.position.y , transform.position.z);
    }

    void SetColor(Color color)
    {
        //TODO
    }
    void SetHeight(int pos)
    {
        
        transform.position = new Vector3(10,pos-3,-1) ;
        
    }
    public void Setup(float frequency, int pos)
    {
        SetHeight(pos);
        freq = frequency;
        
    }
}
