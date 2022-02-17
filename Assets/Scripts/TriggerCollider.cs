using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public GameObject drum;
     AudioSource pokomoSinging;
    public Material drumEmissiveMat;
    // Start is called before the first frame update
    void Start()
    {
        pokomoSinging = drum.GetComponent<AudioSource>();

        pokomoSinging.Play();
        pokomoSinging.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            // music.gameObject.SetActive(true);
            StartCoroutine(volumeFadeOut(.5f, 0, .001f));
            drumEmissiveMat.SetColor("_EmissionColor", 5 * Color.white);
            StartCoroutine(FadeIn(0,4,.05f));
            Debug.Log("you are inside");
        }
      
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeOut(4,0,.1f));

        }
        // music.SetActive(false);
    }
    float intensity;
    public float maxIntensity = 4;
    float volume;
    IEnumerator FadeOut(float peak ,float low,  float increment)
    {
        for (float alpha = peak; alpha >= low; alpha -= increment)
        {
          
            drumEmissiveMat.SetColor("_EmissionColor", alpha * Color.white);
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
    IEnumerator FadeIn( float low, float peak, float increment )
    {
        for (float alpha =low; alpha <= peak; alpha += increment)
        {
           
            drumEmissiveMat.SetColor("_EmissionColor", alpha * Color.white);
            yield return null;
        }
    }

}




