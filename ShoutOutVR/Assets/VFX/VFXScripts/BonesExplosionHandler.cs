//James Gratrix 
//06.03.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonesExplosionHandler : MonoBehaviour {

    [SerializeField]
    private GameObject[] _boneWeaponPrefab;

    [SerializeField]
    private int _maxBones = 3;
	// Use this for initialization
	void Start () {
        Collider[] cols = Physics.OverlapSphere(transform.position, 10);
        foreach (Collider col in cols)
        {
            if (col.GetComponent<Rigidbody>())
            {
                col.GetComponent<Rigidbody>().AddExplosionForce(20,transform.position,10);
            }
        }
        for (int i = 0; i < _maxBones; ++i)
        {
            GameObject newBone = Instantiate(_boneWeaponPrefab[Random.Range(0,_boneWeaponPrefab.Length)]);
            newBone.transform.localScale = new Vector3(Random.Range(0.3f, 0.6f), Random.Range(0.3f, 0.6f), Random.Range(0.3f, 0.6f));
        }
	}
}
