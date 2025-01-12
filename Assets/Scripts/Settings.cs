using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool pitchMode = false; // false = low pitch, 1 = high pitch
    public UnityEngine.UI.Button pitchButton;
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
       pitchButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void TaskOnClick()
    {
        pitchMode = !pitchMode;
        if (pitchMode)
        {
            pitchButton.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "High Pitch";
        }
        else
        {
            pitchButton.gameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "Low Pitch";
        }
    }
}
