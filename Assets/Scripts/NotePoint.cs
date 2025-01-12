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
        if (transform.position.x < -10)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void SetColor(Color color)
    {
        //TODO
    }
    void SetHeight(int pos)
    {
        
        transform.position = new Vector3(10,((2f/3f)*(float)pos)-4f,-1) ;
    }
    public void Setup(float frequency, int pos)
    {
        SetHeight(pos);
        freq = frequency;
        
    }

    public int scoreValue = 10; // Points awarded when colliding with this note point

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // the character has the "Player" tag
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(scoreValue);
                Debug.Log($"Score added: {scoreValue}. Total Score: {scoreManager.score}");
            }

            Destroy(gameObject);
        }
    }
}
