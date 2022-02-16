using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{
    public GameObject music;
    public AudioSource Audio;
    public Material drumEmissiveMat;
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

            drumEmissiveMat.SetColor("_EmissionColor", 5 * Color.white);
            StartCoroutine(glowFadeIn());
            Debug.Log("you are inside");
        }
        else
        {

            music.SetActive(false);
            Audio.Stop();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(glowFadeOut());
        music.SetActive(false);
        Audio.Stop();
    }
    float intensity;
    public float maxIntensity=4;
    IEnumerator glowFadeIn()
    {
        for (float  i = 0; i < maxIntensity; intensity +=0.1f)
        {
            drumEmissiveMat.SetColor("_EmissionColor", intensity * Color.white);
            Debug.Log("intensity drumm hihi" + intensity);

            yield return new WaitForSeconds(.05f);
        }
        
      
    }
    IEnumerator glowFadeOut()
    {
        for (float i = intensity; i > 0; i -= 0.1f)
        {
            drumEmissiveMat.SetColor("_EmissionColor", intensity * Color.white);
            Debug.Log("intensity drumm hihi" + intensity);
            yield return new WaitForSeconds(.05f);
        }
    }
}
