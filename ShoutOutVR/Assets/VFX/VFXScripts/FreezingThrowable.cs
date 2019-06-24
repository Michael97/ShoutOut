//James Gratrix
//06.03.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingThrowable : MonoBehaviour {

    [SerializeField]
    private GameObject _particleSystemIce;

    [SerializeField]
    private float _timerTillUnfreeze = 10f;
    [SerializeField]
    private float _radiusOfInfluence = 10f;

    [SerializeField]
    private Collider _iceCollider;

    private float _timer = 10;
    private bool _timerActive = true;
    private float _freezeDelay = 10;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= -1 && _timerActive) {
            _timer = -1;
            _timerActive = false;
        }
    }

    IEnumerator FreezeUnfreezeTimer()
    {
        _iceCollider.enabled = true;
        Collider[] localColliders = Physics.OverlapSphere(transform.position, _radiusOfInfluence);
        for (int i = 0; i < localColliders.Length; ++i)
        {
            if (!localColliders[i].CompareTag("Player"))
            {
                if (localColliders[i].GetComponent<Rigidbody>())
                {
                    localColliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }
        Instantiate(_particleSystemIce, transform.position, transform.rotation);
        yield return new WaitForSeconds(_timerTillUnfreeze);

        localColliders = Physics.OverlapSphere(transform.position, _radiusOfInfluence);
        for (int i = 0; i < localColliders.Length; ++i)
        {
            if (!localColliders[i].CompareTag("Player"))
            {
                if (localColliders[i].GetComponent<Rigidbody>())
                {
                    if (localColliders[i].CompareTag("Enemy"))
                    {
                        localColliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    }
                    else
                    {
                        localColliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    }
                }
            }
        }
        _iceCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_timerActive)
        {
            StartCoroutine(FreezeUnfreezeTimer());
            _timer = _freezeDelay;
            _timerActive = true;
        }
    }

}
