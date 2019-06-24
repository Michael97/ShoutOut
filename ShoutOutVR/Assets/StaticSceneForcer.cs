using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticSceneForcer : MonoBehaviour {

    [SerializeField]
    private Scene thisScene;

    [SerializeField]
    private string thisSceneName;
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != thisSceneName)
        {
            Destroy(this.gameObject);
        }
    }
}
