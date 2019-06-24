using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Events;

public class SlowTime_Throwable : MonoBehaviour {
    [SerializeField]
    private GameObject _particleSystem;
    [SerializeField]
    private PostProcessProfile _defaultPostProfile;
    [SerializeField]
    private PostProcessProfile _slowTimePostProfile;

    [SerializeField]
    private float _timerTillUnSlow = 10f;

    private float _timer = 2;
    private bool _timerActive = true;
    private float _timeDelay = 10;

    public event UnityAction OnOver;

    private void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= -1 && _timerActive) {
            _timer = -1;
            _timerActive = false;
        }
    }

    IEnumerator FreezeUnfreezeTimer() {
        Instantiate(_particleSystem, transform.position, transform.rotation);
        Time.timeScale = 0.2f;
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i = 0; i < cameras.Length; ++i) {
            if (cameras[i].GetComponent<PostProcessVolume>()) {
                cameras[i].GetComponent<PostProcessVolume>().profile = _slowTimePostProfile;
            }
        }
        yield return new WaitForSeconds(_timerTillUnSlow);
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i = 0; i < cameras.Length; ++i) {
            if (cameras[i].GetComponent<PostProcessVolume>()) {
                cameras[i].GetComponent<PostProcessVolume>().profile = _defaultPostProfile;
            }
        }
        Time.timeScale = 1f;
    }

    private void OnEnable() {
        OnOver += HandleOver;
    }

    public void HandleOver() {
        if (!_timerActive) {
            GetComponentInParent<Animation>().Play();
            StartCoroutine(FreezeUnfreezeTimer());
            _timer = _timeDelay;
            _timerActive = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Hand")) {
            HandleOver();
            Debug.Log("hity");
        }
    }
}
