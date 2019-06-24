//Michael Thomas

using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BossAudioController : MonoBehaviour {

    AudioSource audioData;

    public void FootStep()
    {
        audioData.Play();
    }
}
