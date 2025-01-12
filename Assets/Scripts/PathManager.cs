using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    float deltatime = 0; //The minimum time elapsed between two notes
    List<float> notes = new List<float>();//The list of succeeding notes frequency, all separated by deltatime
    List<int> notesPos = new List<int>();//position of note from 0 to 12
    float[] notes_freq_high = { (float)220, (float)233.08, (float)246.94, (float)261.62, (float)277.18, (float)296.66, (float)311.12, 329.62f, 349.22f, 369.99f, 391.99f, 415.30f,440f };//base notes frequencies
    float[] notes_freq_low = { (float)110, (float)116.54, (float)123.47, (float)130.81, (float)138.59, (float)146.83, (float)155.56, 164.81f, 174.61f, 184.99f, 195.99f, 207.652f, 220f };//base notes frequencies
    public float[] notes_freq;//base notes frequencies
    [SerializeField] GameObject NotePoint_prefab;//Prefab to be able to spawn notepoints
    int nbNotes = 10; //number of notes
    int noteIndex = 0;
    GameObject settings;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {
        settings = GameObject.FindWithTag("Settings");
        if (settings.transform.GetComponent<Settings>().pitchMode)
        {
            notes_freq = notes_freq_high;
        }
        else
        {
            notes_freq = notes_freq_low;
        }

        randomCreation(nbNotes);
        
    }
    // Update is called once per frame
    void Update()
    {


    }
    void FixedUpdate()
    {
        if (noteIndex < Mathf.Infinity && deltatime > 5)
        {
            GameObject newNote = GameObject.Instantiate(NotePoint_prefab);
            newNote.SetActive(true);

            newNote.GetComponent<NotePoint>().Setup(notes[noteIndex], notesPos[noteIndex]);
            Debug.Log(notesPos[noteIndex]);
            noteIndex += 1;
            deltatime = 0;
        }
        deltatime += Time.deltaTime;
        notesPos.Add(Random.Range(0, 13));
        notes.Add(notes_freq[notesPos.Last<int>()]);
    }
    void randomCreation(int size)
    {
        for (int i = 0; i < size; i++)
        {   notesPos.Add(Random.Range(0, 13));
            notes.Add(notes_freq[notesPos[i]]);
        }
        
    }
}
