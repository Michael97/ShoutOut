using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereExplosionHandler : MonoBehaviour {
    [SerializeField]
    private float _pullForce = 10000f;
    [SerializeField]
    private float _timerTillUnSlow = 10f;

    private float _timer = 2;
    private bool _timerActive = true;
    private float _timeDelay = 10;

    private void Update() {
        
        _timer -= Time.deltaTime;
        if (_timer <= -1 && _timerActive) {
            _timer = -1;
            _timerActive = false;
        } else {
            ApplyPullForce();
        }
    }

    //Aplies pull force to enemyAI
    private void ApplyPullForce() {
        Collider[] localColliders = Physics.OverlapSphere(transform.position, 50);
        for (int i = 0; i < localColliders.Length; ++i) {
            if (!localColliders[i].CompareTag("Player")) {
                if (localColliders[i].GetComponent<Rigidbody>()) {
                    Vector3 positionDifference = transform.position - Vector3.zero;
                    localColliders[i].GetComponent<Rigidbody>().AddForce(positionDifference * Time.deltaTime * _pullForce);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (!_timerActive) {
            _timer = _timeDelay;
            _timerActive = true;
        }
    }
}
