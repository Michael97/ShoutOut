using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject thisObject;

    private void Start()
    {
        if(!GameObject.FindGameObjectWithTag("Player"))
        Instantiate(thisObject,transform.position,transform.rotation);
    }
}
