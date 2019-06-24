using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioCenter : MonoBehaviour {

    private AudioSource _audioSource;

    private void Start () {
        
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnLevelWasLoaded(int level) {
        if (level == 0) {
            Destroy(this.gameObject);
        }
    }

    //Plays a clip and overlays even if another is playing
    public void Play(AudioClip a_clip, bool a_pitchVariation, float a_volume = 1)
    {
        if (!_audioSource) {
            _audioSource = GetComponent<AudioSource>();
            return;
        }
        if (a_pitchVariation)
        {
            _audioSource.pitch = Random.Range(0.5f, 1.5f);
        }
        else
        {
            _audioSource.pitch = 1;
        }
        _audioSource.volume = a_volume;
        _audioSource.PlayOneShot(a_clip);
    }

    //Plays a random clip from array and overlays even if another is playing
    public void Play(AudioClip[] a_clip, bool a_pitchVariation)
    {
        if (!_audioSource)
        {
            _audioSource = GetComponent<AudioSource>();
            return;
        }
        if (a_pitchVariation)
        {
            _audioSource.pitch = Random.Range(0.5f, 1.5f);
        }
        else
        {
            _audioSource.pitch = 1;
        }
        _audioSource.PlayOneShot(a_clip[Random.Range(0,a_clip.Length)]);
        
    }

    //Stops all sounds from source
    public void Stop()
    {
        if (!_audioSource)
        {
            _audioSource = GetComponent<AudioSource>();
            return;
        }
        _audioSource.pitch = 1;
        _audioSource.Stop();
    }
}
