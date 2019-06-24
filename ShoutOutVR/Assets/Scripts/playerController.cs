///Ollie Padfield
///14.05.19
/// 
///09.05.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class playerController : MonoBehaviour
{
    public float health = 10;

    private float heartbeatTimer = 0f;
    private float heartbeat = 10f;
    private AudioSource audioSource;
    private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }

    // Update is called once per frame
    void Update()
    {
        heartbeatTimer += Time.deltaTime;
        if(heartbeatTimer > heartbeat)
        {
            if (health > 5f)
            {
                heartbeatTimer = 0f;
            }
            if (health == 5f || heartbeat == 4)
            {
                heartbeat = 3f;
                play(audioClip);
            }
            if (health == 3f || heartbeat == 2)
            {
                heartbeat = 1.5f;
                play(audioClip);
            }
            if (health == 1f)
            {
                heartbeat = 0.75f;
                play(audioClip);
            }
            heartbeatTimer = 0f;
        }

        if (health <= 0)
        {
            heartbeatTimer = 0f;
            //end game
        }
    }


    public void takeDamage()
    {
        health -= 1;
    }

    public void play(AudioClip clip)
    { 
        audioSource.PlayOneShot(clip);
    }      
}
