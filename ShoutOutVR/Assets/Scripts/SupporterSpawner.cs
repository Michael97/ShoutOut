//James Gratrix 11.04.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupporterSpawner : MonoBehaviour {

	[Space(5)]
    [Header("SpawnSettings")]
    [SerializeField]
    private float m_spawnDist = 28.0f;

    [SerializeField]
    private int m_maxSupporterCount = 20;

    [SerializeField]
    private GameObject m_supporterPrefab;

    private List<Transform> supporters = new List<Transform>();

    private void Start() {
        SpawnSupporters();
    }

    private void SpawnSupporters() {

        for (int i = 0; i < m_maxSupporterCount; ++i) {
            GameObject newSupporter = Instantiate(m_supporterPrefab,transform);

            Vector3 newPos = Vector3.zero;
            float angle = Random.Range(0f, Mathf.PI * 2);
            newPos.x = Mathf.Sin(angle) * m_spawnDist;
            newPos.z = Mathf.Cos(angle) * m_spawnDist;
            newSupporter.transform.localPosition = newPos;

            newSupporter.transform.LookAt(transform);
            supporters.Add(newSupporter.transform);
        }
    }

}
