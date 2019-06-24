/// Peter Phillips
/// 18.02.19 - 19.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Valve.VR;
using UnityEngine.SceneManagement;

public class WeaponScript : MonoBehaviour
{
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _barrel;
    [SerializeField] private GameObject _glove;
    [SerializeField] private GameObject _gloveDirection;
    private int shots;
    private RaycastHit hit;
    private int _layerMask = 1 << 9;

    [Space(10)]
    [Header("Audio Settings")]
    [Space(5)]
    [SerializeField] private AudioClip _fireSoundClip;

    private AudioSource _audioCenter;

    private void Start()
    {
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Arena")
        {
            return;
        }

        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            shots = PlayerPrefs.GetInt("shots");
            shots++;
            PlayerPrefs.SetInt("shots", shots);
            Debug.Log("fired");
            GameObject thisBullet = Instantiate(_bullet, _barrel.transform.position, _barrel.transform.rotation);
            thisBullet.transform.parent = null;
            if (_audioCenter)
            {
                _audioCenter.clip = _fireSoundClip;
                _audioCenter.Play();
            }
            else
            {
                _audioCenter = gameObject.AddComponent<AudioSource>();
            }
        }




        if (SteamVR_Actions.default_Magnetise.GetState(SteamVR_Input_Sources.LeftHand))
        {
            //Debug.Log("MAGNETISE");
            if (Physics.SphereCast(_glove.transform.position, 1f, _gloveDirection.transform.forward, out hit, 25f, _layerMask))
            {
                //if (hit.collider.tag == "Object" && hit.rigidbody)
                {
                    //Debug.Log("HIT");
                    //hit.rigidbody.useGravity = false;
                    //hit.rigidbody.AddForce(10 * (_glove.transform.position - hit.transform.position));
                    hit.transform.position += (_glove.transform.position - hit.transform.position) / 10f;
                }
            }
        }
    }
}
