//James Gratrix
// 20.02.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexCube : MonoBehaviour {

    [SerializeField]
    private float _pullForce = 30f;

    [SerializeField]
    private GameObject _particleSystemPull;
    [SerializeField]
    private GameObject _particleSystemExplo;

    private float _timer = -1;
    private bool _timerActive = false;
    private bool _vortexActive = false;

    private Vector3 _setVortexPosition = Vector3.zero;

    //Main start function
    private void Start() {
        Invoke("ActivateSelf", 2f);
    }

    //Called by invoke to activate vortex after 5 secs
    private void ActivateSelf() {
        _vortexActive = true;
    }

    //Main update loop
    private void Update() {
        if(_timerActive) {
            _timer -= Time.deltaTime;
            ApplyPullForce();
        }
        if (_timer <= -1) {
            _timer = -1;
            _timerActive = false;
            //ApplyExplosionForce();
        }
    }
    
    //Creates inital Particle System
    private void OnCollisionEnter(Collision collision) {
        if (_timer <= 0 && _vortexActive && collision.transform.tag == "Object") {
            _setVortexPosition = transform.position;
            Instantiate(_particleSystemPull, transform);
            _timerActive = true;
            _timer = 30;
        }
    }

    //Aplies pull force to enemyAI
    private void ApplyPullForce() {
        Collider[] localColliders = Physics.OverlapSphere(_setVortexPosition,10);
        for (int i = 0; i < localColliders.Length; ++i) {
            if (!localColliders[i].CompareTag("Player") && localColliders[i].gameObject.layer != 9) {
                if (localColliders[i].GetComponent<Rigidbody>()) {
                    Vector3 positionDifference = _setVortexPosition - localColliders[i].transform.position;
                    localColliders[i].GetComponent<Rigidbody>().AddForce(positionDifference * Time.deltaTime * _pullForce);
                }
            }
        }
    }

    //Aplies pull force to enemyAI
    private void ApplyExplosionForce() {
        Collider[] localColliders = Physics.OverlapSphere(_setVortexPosition, 1);
        for (int i = 0; i < localColliders.Length; ++i) {
            if (!localColliders[i].CompareTag("Player") && localColliders[i].gameObject.layer != 9) {
                if (localColliders[i].GetComponent<Rigidbody>()) {
                    Vector3 positionDifference = _setVortexPosition - localColliders[i].transform.position;
                    localColliders[i].GetComponent<Rigidbody>().AddForce(-positionDifference * Time.deltaTime * _pullForce*0.5f,ForceMode.Impulse);
                }
            }
        }
        Instantiate(_particleSystemExplo, transform);
    }
}
