using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeathVFX : MonoBehaviour {

    [SerializeField]
    private GameObject _explosionDeathVFX;

    [SerializeField]
    private AudioClip death_Clip;
    private AudioSource ac;

    public HighscoresController highscoreController;

    private void Start() {
        //ac = GameObject.FindObjectOfType<AudioCenter>();
        ac = gameObject.AddComponent<AudioSource>();
        highscoreController = GameObject.FindGameObjectWithTag("Highscore").GetComponent<HighscoresController>();
    }

    private void OnDestroy() {
        if(!highscoreController)
        {
            highscoreController = GameObject.FindGameObjectWithTag("Highscore").GetComponent<HighscoresController>();
        }

        if (_explosionDeathVFX) {
            Instantiate(_explosionDeathVFX, transform.position, transform.rotation);
        }

        if (ac) {
            ac.clip = death_Clip;
            ac.Play();
        }

        highscoreController.AddScore(50);
    }
}
