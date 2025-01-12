using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] TMPro.TextMeshProUGUI score_number;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        UpdateScore();
        Destroy(collision.gameObject);
    }

    void UpdateScore()
    {
        score_number.text = score.ToString();
    }
}
