//James Gratrix
//07.03.2019
using UnityEngine;
using System.Collections;

public class ForceFieldEffectHandler : MonoBehaviour {
    private float _time = 0;
    [SerializeField]
    private ParticleSystem localPS;
    [SerializeField]
    private SphereCollider localCollider;
    private void Update() {
        _time += Time.deltaTime * 0.1f;
        localCollider.radius = localPS.sizeOverLifetime.size.curve.Evaluate(_time) * 4.5f;
    }
}
