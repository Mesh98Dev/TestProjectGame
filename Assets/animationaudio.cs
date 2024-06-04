using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AnimationAudioTrigger : MonoBehaviour
{
  
    public AudioClip[] attackSounds;
    private AudioSource soundSource;
    // Start is called before the first frame update
    void Start()
    {
        soundSource = GetComponent<AudioSource>();
     
    }
    public void AttackSound()
    {
        int n = Random.Range(1, attackSounds.Length);
        soundSource.clip = attackSounds[n];
        soundSource.PlayOneShot(soundSource.clip);

        attackSounds[n] = attackSounds[0];
        attackSounds[0] = soundSource.clip;
    }

}
