//Sam Baker
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIScript : MonoBehaviour {

    private int enemiesKilled, shotsFired;
    [SerializeField]
    private GameObject ekObj, sfObj;
    
    void Start() {
        if (!(ekObj == null) && !(sfObj == null)) {
            if (PlayerPrefs.HasKey("shots")) {
                shotsFired = PlayerPrefs.GetInt("shots");
                sfObj.GetComponent<TextMesh>().text = "Shots Fired: " + shotsFired.ToString();
            } else {
                PlayerPrefs.SetInt("shots", 0);
            }
            if (PlayerPrefs.HasKey("kill")) {
                enemiesKilled = PlayerPrefs.GetInt("kill");
                ekObj.GetComponent<TextMesh>().text = "Enemies Killed: " + enemiesKilled.ToString();
            } else {
                PlayerPrefs.SetInt("kill", 0);
            }
        }
    }

    public void Back() {
        SceneManager.LoadScene(1);
    }
    //Delete all saved stats
    public void Reset() {
        PlayerPrefs.DeleteAll();
    }
}

//int shots = PlayerPrefs.GetInt("shots");
//shots++;
//PlayerPrefs.SetInt("shots", shots);

//int kill = PlayerPrefs.GetInt("kill");
//kill++;
//PlayerPrefs.SetInt("kill", kill);