using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class animationaudio : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip[] attackSounds;
    private AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
     
    }
    public void AttackSound()
    {
        int n = Random.Range(0, attackSounds.Length);
        soundSource.clip = attackSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

    }

    public void DeathSound()
    {
        soundSource.clip = deathSound;
        soundSource.PlayOneShot(soundSource.clip);
    }


}
