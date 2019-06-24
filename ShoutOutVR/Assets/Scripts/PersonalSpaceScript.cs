/// Peter Phillips
/// 15.05.19 - 15.05.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalSpaceScript : MonoBehaviour 
{
    [SerializeField] private float _forceMultiplier = 1000;

    private Vector3 _playerPos;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null && other.tag == "Enemy") 
        {
            _playerPos = transform.position;
            _playerPos.y = other.transform.position.y;
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - _playerPos).normalized * _forceMultiplier * (GetComponent<CapsuleCollider>().radius - Vector3.Distance(other.transform.position, transform.position)));
        }
    }
}
