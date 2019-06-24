using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSceneManager : MonoBehaviour {

    [SerializeField]
    private GameObject[] listOfItems;
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            for (int i = 0; i < listOfItems.Length; ++i)
            {
                listOfItems[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < listOfItems.Length; ++i)
            {
                listOfItems[i].SetActive(true);
            }
        }
    }
}
