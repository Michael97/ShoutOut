///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///
///                                                                                             ///
///     File name       :   PitchDeathScript.cs                                                 ///
///                                                                                             ///
///     Author          :   Peter Phillips                                                      ///
///                                                                                             ///
///     Date created    :   03.10.2018                                                          ///
///                                                                                             ///
///     Last Modified   :   13.05.2019                                                          ///
///                                                                                             ///
///     Brief           :   Script to control shield breaking due to pitch recognition.         ///
///                                                                                             ///
///\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PitchDeathScript : MonoBehaviour
{
    [SerializeField] private float _shieldProbability = .5f;
    [SerializeField] private GameObject _enemy;

    private bool isHit = false;
    private int _colourNum;
    private Color _colour;

    private void Awake()
    {
        VisualiserScript.PitchThresholdReached += ShieldBreak;
    }

    void Start ()
    {
        //if (UnityEngine.Random.Range(0f, 1f) < _shieldProbability)
        //{
        //    Destroy(gameObject);
        //    VisualiserScript.PitchThresholdReached -= ShieldBreak;
        //    return;
        //}
        _enemy.tag = "shielded";
        _colourNum = UnityEngine.Random.Range(0, 16);
        _colour = Color.HSVToRGB((_colourNum * 22.5f) / 360f, 1, 1);
        _colour.a = .5f;
        GetComponent<Renderer>().material.color = _colour;
	}
	
	void Update ()
    {
        if (GetComponent<Renderer>().material.color.a <= .1f)
        {
            Destroy(gameObject);
            _enemy.tag = "Enemy";
            VisualiserScript.PitchThresholdReached -= ShieldBreak;
        }
        else if (GetComponent<Renderer>().material.color.a < .5f)
        {
            _colour = GetComponent<Renderer>().material.color;
            _colour.a = Mathf.Clamp(_colour.a + .01f, 0, .5f);
            GetComponent<Renderer>().material.color = _colour;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "bullet")
    //    {
    //        Destroy(other);
    //    }
    //}

    private void ShieldBreak(int barPos)
    {
        if (barPos == _colourNum && gameObject)
        {
            _colour = GetComponent<Renderer>().material.color;
            _colour.a = Mathf.Clamp(_colour.a - .05f, 0, .5f);
            GetComponent<Renderer>().material.color = _colour;
        }
    }
}
