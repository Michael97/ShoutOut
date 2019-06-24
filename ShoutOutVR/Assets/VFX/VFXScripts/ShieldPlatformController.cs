using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPlatformController : MonoBehaviour {

    [SerializeField] private float m_riseingSpeed = 1.0f;

    [SerializeField] private float m_timeBetweenDeloy = 12.0f;

    [SerializeField] private AnimationClip m_riseAnimClip;

    private float timer = 0;
    private Vector3 newPos = Vector3.zero;

    private void Start() {
        StartCoroutine(Deploy());
    }

    IEnumerator Deploy() {
        float radius = Random.Range(1f, 3f);
        transform.localScale = new Vector3(radius, Random.Range(1f, 3f), radius);
        newPos = new Vector3(Random.Range(-15, 15), 0, Random.Range(-15, 15));

        GetComponent<Animation>().Play();
        yield return new WaitForSeconds(m_timeBetweenDeloy);
        StartCoroutine(Deploy());
    }

    private void LateUpdate() {
        transform.position = newPos;
    }

}
