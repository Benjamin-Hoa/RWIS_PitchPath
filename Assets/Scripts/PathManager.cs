using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PathManager : MonoBehaviour
{

    float deltatime = 0; //The minimum time elapsed between two notes
    List<float> notes = new List<float>();//The list of succeeding notes frequency, all separated by deltatime
    List<int> notesPos = new List<int>();//position of note from 0 to 6
    float[] notes_freq = {(float)261.63, (float)293.6, (float)329.63, (float)349.23, (float)392, (float)440, (float)493.88};//base notes frequencies
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
        Debug.Log("appel");
    }
    // Update is called once per frame
    void Update()
    {

        if (noteIndex < nbNotes && deltatime > 4)
        {
            GameObject newNote = GameObject.Instantiate(NotePoint_prefab);
            newNote.SetActive(true);
            Debug.Log(notesPos);
            newNote.GetComponent<NotePoint>().Setup(notes[noteIndex], notesPos[noteIndex]);
            noteIndex += 1;
            deltatime = 0;
        }
        deltatime += Time.deltaTime;

    }
    void randomCreation(int size)
    {
        for (int i = 0; i < size; i++)
        {   notesPos.Add(Random.Range(0, 7));
            notes.Add(notes_freq[notesPos[i]]);
        }
        Debug.Log(notesPos);
    }
}
