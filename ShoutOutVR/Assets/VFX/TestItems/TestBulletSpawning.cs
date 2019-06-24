using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBulletSpawning : MonoBehaviour {

    [SerializeField] private GameObject _bullet;

    [SerializeField] private float _spawnRate = 0.2f;
    private float _lifeTime = 1f;
    private float _timer = 0f;

    // Update is called once per frame
    void Update () {
        _timer += Time.deltaTime * _spawnRate;

        if (_timer > _lifeTime)
        {
            Instantiate(_bullet, transform.position, transform.rotation);
            _timer = 0f;
        }
    }
}
