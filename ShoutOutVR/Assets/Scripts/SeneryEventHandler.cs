//James Gratrix
//21/03/2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//VERY BASIC, Needs improving!

public class SeneryEventHandler : MonoBehaviour {

    [SerializeField] private GameObject[] FlyingObjects = new GameObject[2];
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private int maxAsteroids = 6;
    [SerializeField] public List<Transform> asteroids = new List<Transform>();
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnAsteroid());
	}
    private void Update() {
        for (int i = 0; i < asteroids.Count; ++i) {
            if (Vector3.Distance(asteroids[i].position, Vector3.zero) >= 510) {
                Transform go = asteroids[i];
                asteroids.Remove(asteroids[i]);
                Destroy(go.gameObject);
                
            }
        }
        if (asteroids.Count <= 0) {
            StartCoroutine(SpawnAsteroid());
        }
        
    }
    IEnumerator SpawnAsteroid() {
        while (asteroids.Count < maxAsteroids) {
            //new spawn pos
            Vector3 newPos = Random.onUnitSphere * 200;
            newPos.y = 91;
            //New target Pos
            Vector3 newTargetPos = newPos;
            newTargetPos.x *= -1;
            newTargetPos.z *= -1;
            newTargetPos.y = 60;
            GameObject newAsteroid = Instantiate(FlyingObjects[Random.Range(0,2)], newPos, transform.rotation);
            newAsteroid.transform.LookAt(newTargetPos);
            asteroids.Add(newAsteroid.transform);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
