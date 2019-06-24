using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceStart : MonoBehaviour {

    public AudioSource audioSource;
    public AudioClip[] audioClips;

	// Use this for initialization
	void Start () {
        audioSource = GameObject.FindGameObjectWithTag("Voice").GetComponent<AudioSource>();

        int index = Random.Range(0, audioClips.Length);
        AudioClip playClip = audioClips[index];
        audioSource.clip = playClip;
        audioSource.Play();
    }
	

}
