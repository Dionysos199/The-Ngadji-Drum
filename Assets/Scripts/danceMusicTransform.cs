using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danceMusicTransform : MonoBehaviour
{
    public soundManager soundManager;
    AudioSource audioSource;
    float volume;


    //Listener to drumHit
    //plays the women singing music and increases it gradually with each phase 
    public void playDanceMusic(float hitForce, Vector3 u, int n)
    {
        
        if (n%5==0)
        {
            volume += .2f;
            AudioClip DanceMusic = soundManager.soundsArray[(int)soundManager.sounds.DANCEDRUMWOMENMUSIC];
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = DanceMusic;
            audioSource.Play();
            audioSource.volume = volume;
        }

    }
}
