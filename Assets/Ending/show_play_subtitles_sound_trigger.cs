using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class show_play_subtitles_sound_trigger : MonoBehaviour
{
    public GameObject uiObject;
    public AudioSource playSound;
    public GameObject danceMusic;
    public lastSceneScript lastscene;
    float startTime;
    bool startCounting=false;

    // Start is called before the first frame update
    void Start()
    {

        //  StartCoroutine(waitBeforeReturning());
        
        playSound.Stop();
    }
    private void Update()
    {

        Debug.Log(CountUp(startTime) + "djnfksjnvkjn");
        if (startCounting & CountUp(startTime)>3)
        {
            lastscene.returnToMuseum();
        }
    }
    float CountUp(float startTime)
    {
        return (Time.time - startTime);
    }
    // Update is called once per frame
    void OnTriggerEnter (Collider player)
    {
        startTime = Time.time;
        startCounting = true;
        Debug.Log(startCounting + "dkjvfgslkfdvnl");
        Destroy(gameObject);
            playSound.Play();
            danceMusic.GetComponent<AudioSource>().volume = .2f;
        
      
        

    }

    

}
