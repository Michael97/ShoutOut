//James Gratrix
//07.03.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragThrowable : MonoBehaviour {

    [SerializeField]
    private float _force = 5000f;

    [SerializeField]
    private float _radiusOfInfluence = 5f;

    [SerializeField]
    private GameObject _particleSystemExplo;

    private float _timer = -1;
    private bool _timerActive = false;



    //Creates inital Particle System
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Floor") {
            ApplyExplosionForce();
        }
   
    }

    //Aplies pull force to enemyAI
    private void ApplyExplosionForce() {
        Collider[] localColliders = Physics.OverlapSphere(transform.position, _radiusOfInfluence);
        for (int i = 0; i < localColliders.Length; ++i) {
            if (!localColliders[i].CompareTag("Player") && localColliders[i].gameObject.layer != 9) {
                if (localColliders[i].GetComponent<Rigidbody>()) {
                    Vector3 positionDifference = transform.position - localColliders[i].transform.position;
                    localColliders[i].GetComponent<Rigidbody>().AddForce(-positionDifference * Time.deltaTime * _force * 0.5f, ForceMode.Impulse);
                    if (Vector3.Distance(transform.position, localColliders[i].transform.position) < (_radiusOfInfluence * 0.5f)
                        && localColliders[i].CompareTag("Enemy")) {
                        Destroy(localColliders[i].gameObject);
                    }
                }
            }
        }
        Instantiate(_particleSystemExplo, transform);
    }
}
