//James Gratrix
//06.02.2019
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BonesKeywordHandler : MonoBehaviour {

    [SerializeField]
    private TextMeshPro[] _keyWordText;
    private void Start()
    {
        GameObject _gameController = GameObject.FindGameObjectWithTag("GameController");
        string[] a_slistOfNames = _gameController.GetComponent<VoiceRecognition>()._listOfNames;
        string newName = a_slistOfNames[Random.Range(0, a_slistOfNames.Length)];
        for (int i = 0; i < _keyWordText.Length; ++i) {
            _keyWordText[i].text = newName;
        }

    }

    private void Update()
    {
        Vector3 localScale = transform.localScale;
        localScale.x = localScale.y = localScale.z -= (Time.deltaTime * 0.01f);
        transform.localScale = localScale;
        if (transform.localScale.x <= 0.02f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy") && collision.gameObject.GetComponentInChildren<TextMeshPro>().text == _keyWordText[0].text)
        {
            Destroy(collision.gameObject);
        }
    }
}
