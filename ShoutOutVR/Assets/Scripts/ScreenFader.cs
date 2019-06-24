using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

    [SerializeField]
    private RawImage alphaScreen;

    private bool fadeOut = false;
    private bool fadeIn = false;
    private string loadingScene = "";

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    public IEnumerator LoadScene(Scene newScene) {
        loadingScene = newScene.name;
        Debug.Log("Loading");
        fadeOut = true;
        yield return new WaitForSeconds(2);
        fadeIn = true;
    }

    public IEnumerator LoadScene(string newScene) {
        Debug.Log("Loading");
        loadingScene = newScene;
        fadeOut = true;
        yield return new WaitForSeconds(2);
        fadeIn = true;
    }

    private void Update() {
        if (fadeOut && alphaScreen.color.a < 2f) {
            Color col = alphaScreen.color;
            col.a += Time.deltaTime;
            alphaScreen.color = col;
            Debug.Log(alphaScreen.color.a);
        }
        else  if (fadeOut && alphaScreen.color.a >= 2f) {
            fadeOut = false;
            SceneManager.LoadScene(loadingScene);
            fadeIn = true;
        }

        if (fadeIn && alphaScreen.color.a > 0.1f) {
            Color col = alphaScreen.color;
            col.a -= Time.deltaTime;
            alphaScreen.color = col;
        }
        else if (alphaScreen.color.a <= 0.1f) {
            fadeIn = false;
        }
    }
}
