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

        emissiveMaterial.SetColor("_EmissionColor", normalLightEmissionIntensity * new Color(.75f, .75f, .6f));
        this.gameObject.GetComponent<Light>().enabled = true; 
    }
    void Update()
    {
      
    }
    private void OnDisable()
    {
        
    }
    public void flicker(float hitForce, Vector3 u, int n )
    {
        if(hitForce>20 & gameManager.Instance.numberOfHits>3)
        StartCoroutine(Flickering(hitForce));

    }
    IEnumerator Flickering(float FlickeringTime)
    {
        
        this.gameObject.GetComponent<Light>().enabled = false;
        
        emissiveMaterial.SetColor("_EmissionColor", 0*Color.black);


        AudioClip spark= soundManager.soundsArray[(int)soundManager.sounds.ELECTRICBUZZ];
        soundManager.instantiateSound(transform.position, spark,.5f, spark.length);

        ParticleSystem sparks = GetComponent<ParticleSystem>();
        sparks.Play();

        timeDelay = Random.Range(0.01f, .1f)* FlickeringTime;
        
        yield return new WaitForSeconds(timeDelay);

        emissiveMaterial.SetColor("_EmissionColor", normalLightEmissionIntensity  * new Color(.75f,.75f,.6f));
        this.gameObject.GetComponent<Light>().enabled = true;

        //add the light going back on sound effect
        AudioClip lightON = soundManager.soundsArray[(int)soundManager.sounds.NEONLIGHTON];
        soundManager.instantiateSound(transform.position, lightON,.5f, lightON.length);

        timeDelay = Random.Range(.05f, .15f)*FlickeringTime;
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
