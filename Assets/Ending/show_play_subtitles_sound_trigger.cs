using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_play_subtitles_sound_trigger : MonoBehaviour
{
    public GameObject uiObject;
    public AudioSource playSound;
    public GameObject danceMusic;
    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
        playSound.Stop();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            Destroy(gameObject);
            playSound.Play();
            danceMusic.GetComponent<AudioSource>().volume = .2f;
        }
    }

   
}
