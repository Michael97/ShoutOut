//Sam Baker
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhatItemScript : MonoBehaviour {

    public GameObject popUpText;
    [SerializeField]
    private GameObject optionsUI;
    [SerializeField]
    private bool isFlushed = true;
    private int numOfItems = 1;

    [SerializeField]
    private GameObject toiletFlushObject;

    [SerializeField]
    private ScreenFader screenFader;

    void Start() {
        optionsUI.SetActive(false);
    }

    void Update() {
        //if (numOfItems >= 2) {
        //    popUpText.SetActive(true);
        //} else { popUpText.SetActive(false); }
    }

    //void OnTriggerEnter(Collider col) {
    //    //numOfItems=1;
    //}

    void OnTriggerEnter(Collider col) {
        if (numOfItems == 1) {
            GetComponent<AudioSource>().Play();
            if (col.gameObject.tag == "Play") { //Load the game
                StartCoroutine(screenFader.LoadScene("Arena"));
            } else if (col.gameObject.tag == "Trophy") { //Trophy Room
                StartCoroutine(screenFader.LoadScene("TrophyRoom"));
            } else if (col.gameObject.tag == "Collection") { //Collection Room
                StartCoroutine(screenFader.LoadScene("CollectionsRoom"));
            } else if (col.gameObject.tag == "Home") { //Load the start of the main menu
                StartCoroutine(screenFader.LoadScene("MainMenu"));
            } else if (col.gameObject.tag == "Exit") { //Exit the game
                Application.Quit();
            } else if (col.gameObject.tag == "Options") { //Options UI
                optionsUI.SetActive(true);
            }
        }
    }
}
