/// Peter Phillips
/// 18.02.19 - 19.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    private float _lifeTime;
    private float _timer = 0f;

    private void Start()
    {
        _lifeTime = GetComponent<ParticleSystem>().main.startLifetimeMultiplier;
    }

    void Update ()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
        {
            Destroy(gameObject);
        }
	}
}
