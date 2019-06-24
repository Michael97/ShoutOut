/// Peter Phillips
/// 18.02.19 - 20.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	void Start ()
    {
        //// FOR DEBUGGING
        //string[] _names = UnityEngine.Input.GetJoystickNames();
        //foreach (string s in _names)
        //{
        //    Debug.Log(s);
        //}
    }

    void Update ()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainMenu");
        }

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * .2f);

        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
