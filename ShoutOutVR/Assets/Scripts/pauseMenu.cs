///Ollie Padfield
///02.05.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class pauseMenu : MonoBehaviour
{
    public static bool paused = false;
    public GameObject menu;

    [SerializeField]
    private PostProcessProfile _defaultPostProfile;
    [SerializeField]
    private PostProcessProfile _otherPostProfile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    public void pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        if (Camera.main.GetComponent<PostProcessVolume>())
        {
            Camera.main.GetComponent<PostProcessVolume>().profile = _otherPostProfile;
        }
        paused = true;
    }

    public void resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        if (Camera.main.GetComponent<PostProcessVolume>())
        {
            Camera.main.GetComponent<PostProcessVolume>().profile = _defaultPostProfile;
        }
        paused = false;
    }

    public void loadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
