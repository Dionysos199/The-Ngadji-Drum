using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight_ : MonoBehaviour
{

    //public bool isHit = false;
     float timeDelay;
    public Material emissiveMaterial;
    public soundManager soundManager;


    float normalLightEmissionIntensity =2.5f;
    private void Start()
    {

    }
    void Update()
    {
      
    }

    public void flicker(float flickerForce, Vector3 u, int n )
    {
        StartCoroutine(Flickering(flickerForce));

    }
    IEnumerator Flickering(float FlickeringTime)
    {
        
        this.gameObject.GetComponent<Light>().enabled = false;
        
        emissiveMaterial.SetColor("_EmissionColor", 0*Color.black);
        AudioClip spark= soundManager.soundsArray[(int)soundManager.sounds.ELECTRICBUZZ];
        soundManager.instantiateSound(transform.position, spark, spark.length);

        ParticleSystem sparks = GetComponent<ParticleSystem>();
        sparks.Play();
        timeDelay = Random.Range(0.1f, 1f*FlickeringTime);
        
        yield return new WaitForSeconds(timeDelay);

        emissiveMaterial.SetColor("_EmissionColor", normalLightEmissionIntensity  * new Color(.75f,.75f,.6f));
        this.gameObject.GetComponent<Light>().enabled = true;


        AudioClip lightON = soundManager.soundsArray[(int)soundManager.sounds.NEONLIGHTON];
        soundManager.instantiateSound(transform.position, lightON, lightON.length);
        timeDelay = Random.Range(.5f, 1.5f*FlickeringTime);
        yield return new WaitForSeconds(timeDelay);
       
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isHit = true;
            
    //    }
    //}
}
