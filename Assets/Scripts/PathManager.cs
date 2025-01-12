using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    float deltatime = 0; //The minimum time elapsed between two notes
    List<float> notes = new List<float>();//The list of succeeding notes frequency, all separated by deltatime
    List<int> notesPos = new List<int>();//position of note from 0 to 11
    float[] notes_freq_high = { (float)164.81, (float)174.61, (float)184.99, (float)195.99, (float)207.65, (float)220, (float)233.08, 246.94f, 261.52f, 277.18f, 293.66f, 311.12f };//base notes frequencies
    float[] notes_freq_low = { (float)164.81, (float)174.61, (float)184.99, (float)195.99, (float)207.65, (float)220, (float)233.08, 246.94f, 261.52f, 277.18f, 293.66f, 311.12f };//base notes frequencies
    float[] notes_freq = {(float)164.81, (float)174.61, (float)184.99, (float)195.99, (float)207.65, (float)220, (float)233.08,246.94f,261.52f,277.18f,293.66f,311.12f};//base notes frequencies
    [SerializeField] UnityEngine.Color[] colors;
    [SerializeField] GameObject NotePoint_prefab;//Prefab to be able to spawn notepoints
    int nbNotes = 10; //number of notes
    int noteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Awake()
    {

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

            newNote.GetComponent<NotePoint>().Setup(notes[noteIndex], notesPos[noteIndex],colors[notesPos[noteIndex]]);
            Debug.Log(notesPos[noteIndex]);
            noteIndex += 1;
            deltatime = 0;
        }
        deltatime += Time.deltaTime;
        notesPos.Add(Random.Range(0, 12));
        notes.Add(notes_freq[notesPos.Last<int>()]);
    }
    void randomCreation(int size)
    {
        for (int i = 0; i < size; i++)
        {   notesPos.Add(Random.Range(0, 12));
            notes.Add(notes_freq[notesPos[i]]);
        }
        
    }
}
