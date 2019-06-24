using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HintSystem : MonoBehaviour {

    [SerializeField]
    private TextMeshPro hintTitle;
    [SerializeField]
    private TextMeshPro hintMessage;

    [SerializeField]
    private string[] messages;

    // Use this for initialization
    void Start () {
        StartCoroutine(ShowHints());
	}

    IEnumerator ShowHints() {
        for (int i = 0; i < messages.Length; ++i) {
            hintMessage.text = messages[i];
            yield return new WaitForSeconds(5.0f);
            if (i >= messages.Length) {
                Destroy(this.gameObject);
            }
        }
        yield return new WaitForSeconds(5.0f);
        while (hintTitle.color.a > 0f)
        {
            Color n = hintTitle.color;
            n.a -= Time.deltaTime * 0.1f;
            hintTitle.color = n;
            hintMessage.color = n;

        }
    }
}
