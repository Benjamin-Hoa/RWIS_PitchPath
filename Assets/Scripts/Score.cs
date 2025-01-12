using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] int score;
    [SerializeField] TMPro.TextMeshProUGUI score_number;
    [SerializeField] ParticleSystem particle_effect;

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
	particle_effect.Play();
        UpdateScore();
        Destroy(collision.gameObject);
	ParticleSystem.MainModule settings = particle_effect.main;
	settings.startColor = new ParticleSystem.MinMaxGradient(collision.gameObject.GetComponent<Renderer>().material.color);
    }

    void UpdateScore()
    {
        score_number.text = score.ToString();
    }
}
