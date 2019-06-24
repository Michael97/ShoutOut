//James Gratrix
//07.03.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField_Throwable : MonoBehaviour {

    [SerializeField]
    private GameObject _particleSystemExplo;

    private float _timer = -1;
    private bool _timerActive = false;

    //Main update loop
    private void Update() {
        if (_timerActive) {
            _timer -= Time.deltaTime;
        }
        if (_timer <= -1) {
            _timer = -1;
            _timerActive = false;
        }
    }

    //Creates inital Particle System
    private void OnCollisionEnter(Collision collision) {
        if (_timer <= 0) {
            Instantiate(_particleSystemExplo, transform.position, transform.rotation);
            _timerActive = true;
            _timer = 12;
        }
    }
}
