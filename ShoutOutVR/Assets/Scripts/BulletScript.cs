/// Peter Phillips
/// 18.02.19 - 20.02.19
/// Editied by James Gratrix, 19.02.19
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = .3f;
    [SerializeField] private GameObject _trailParticleSystem;
    [SerializeField] private TextMesh _bulletText;

    private float _lifeTime = 1f;
    private float _timer = 0f;
    private int kills;

    private void Start()
    {
        CreateTrailParticleSystem();

        GameObject _word = GameObject.FindGameObjectWithTag("WordAmmo");
        _bulletText.text = _word.GetComponent<TextMesh>().text;
    }

    void Update ()
    {
        _timer += Time.deltaTime;

        if (_timer < _lifeTime)
        {
            transform.position += transform.forward * _bulletSpeed;
        }
        else
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && other.GetComponentInChildren<TextMeshPro>().text == _bulletText.text)
        {
            if (other.transform.localScale.x >= 1.0f) {
                other.transform.localScale *= 0.7f;
                //other.GetComponent<EnemyAI>().ChooseNewName();
            } else {
                Destroy(other.gameObject);
                Destroy(gameObject);
                kills = PlayerPrefs.GetInt("kill");
                kills++;
                PlayerPrefs.SetInt("kill", kills);
            }
        }
    }

    private void CreateTrailParticleSystem()
    {
        GameObject _newTrailPS = Instantiate(_trailParticleSystem, transform.position, transform.rotation);
        _newTrailPS.transform.SetParent(transform);
    }
}
