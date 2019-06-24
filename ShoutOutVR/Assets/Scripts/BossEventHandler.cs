using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class BossEventHandler : MonoBehaviour {

    [SerializeField] private GameObject _laserVFXPrefab;
    [SerializeField] private GameObject _laserExploVFXPrefab;
    [SerializeField] private GameObject _largeExploVFXPrefab;
    [SerializeField] private GameObject _largeExploVFXPrefab2;
    [SerializeField] private GameObject _largeExploVFXPrefab3;
    [SerializeField] private GameObject _vortexVFXPrefab;
    [SerializeField] private GameObject _largeEnemyPrefab;

    [SerializeField] private float _timeTillDeonation = 10.0f;

    [SerializeField] private float _laserOffset = 10.0f;

    private AudioSource _audioSource;

    [SerializeField] private AudioClip _laserEffectClip;
    [SerializeField] private AudioClip _laserExploClip;

    private Camera mainCam;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(BossSpawn());
        mainCam = Camera.main;
	}

    private void Update() {
        _audioSource.pitch = Time.timeScale;
    }

    IEnumerator BossSpawn()
    {
        while (_laserOffset > 1) {
            yield return new WaitForSeconds(_timeTillDeonation);
            //Spawn Initial Laser VFX
            _audioSource.PlayOneShot(_laserEffectClip, 0.8f);
            GameObject vfx = Instantiate(_laserVFXPrefab, transform.position + new Vector3(0, _laserOffset, 0), _laserVFXPrefab.transform.rotation);
            GameObject vfxExplo;
            RaycastHit hit;
            Vector3 hitPoint = transform.position;
            if (Physics.Raycast(transform.position + new Vector3(0, _laserOffset, 0), -transform.up, out hit)) {
                if (hit.distance <= _laserOffset + 5) {
                    vfxExplo = Instantiate(_laserExploVFXPrefab, hit.point, transform.rotation);
                    vfxExplo.transform.SetParent(vfx.transform);
                    hitPoint = hit.point;
                }
            }
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);

            if (mainCam.GetComponent<CameraShake>()) {
                StartCoroutine(mainCam.GetComponent<CameraShake>().Shake(21, 1f));
            }

            yield return new WaitForSeconds(9);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);
            Instantiate(_vortexVFXPrefab, transform.position, _laserVFXPrefab.transform.rotation);

            yield return new WaitForSeconds(12);
            _audioSource.PlayOneShot(_laserExploClip, 2.0f);
            vfxExplo = Instantiate(_largeExploVFXPrefab, hitPoint, transform.rotation);
            Instantiate(_largeExploVFXPrefab, hitPoint, transform.rotation);
            Instantiate(_largeExploVFXPrefab, hitPoint, transform.rotation);
            Instantiate(_largeExploVFXPrefab2, hitPoint, transform.rotation);
            Instantiate(_largeExploVFXPrefab3, hitPoint, transform.rotation);
            GameObject enemy = Instantiate(_largeEnemyPrefab, hitPoint + new Vector3(0, 3, 0), transform.rotation);
            enemy.transform.localScale = new Vector3(3, 3, 3);
            Destroy(vfx);
            yield return new WaitForSeconds(Random.Range(150, 260));
        }
    }
}
