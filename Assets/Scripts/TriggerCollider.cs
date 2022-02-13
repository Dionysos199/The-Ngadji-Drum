using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public GameObject music;
    public AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        music.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            music.gameObject.SetActive(true);
            Audio.Play();
        }
        else
        {

            music.SetActive(false);
            Audio.Stop();
        }
    }




}
