using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is not active at the moment 

public class TriggerCollider : MonoBehaviour
{
    public GameObject drum;
     AudioSource pokomoSinging;
    public Material drumEmissiveMat;
    Light reactiveLight;
    // Start is called before the first frame update
    void Start()
    {
        pokomoSinging = drum.GetComponent<AudioSource>();

        reactiveLight = drum.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // music.gameObject.SetActive(true);
            StartCoroutine(FadeIn(0, 5, .1f));
            Debug.Log("you are inside");
            pokomoSinging.Play();
            StartCoroutine(volumeFadeIn(0, 0.2f, .001f));
        }
      
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(FadeOut(5, 0, .1f));

            StartCoroutine(volumeFadeOut(.2f, 0, .001f));

            // music.SetActive(false);
        }
    }
    float intensity;
    public float maxIntensity = 4;
    float volume;
    IEnumerator FadeOut(float peak ,float low,  float increment)
    {
        for (float alpha = peak; alpha >= low; alpha -= increment)
        {
            yield return null;
        }
    }
   
    IEnumerator FadeIn( float low, float peak, float increment )
    {
        for (float alpha =low; alpha <= peak; alpha += increment)
        {

            reactiveLight.intensity = alpha;
            yield return null;
        }
    }
    IEnumerator volumeFadeOut(float peak, float low, float increment)
    {
        for (float alpha = peak; alpha >= low; alpha -= increment)
        {

            pokomoSinging.volume = alpha;
            yield return null;
        }
    }
    IEnumerator volumeFadeIn(float low, float peak, float increment)
    {
        for (float alpha = low; alpha <= peak; alpha += increment)
        {

            pokomoSinging.volume = alpha;
            yield return null;
        }
    }
}




