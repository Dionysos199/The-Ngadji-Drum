using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this class is also a listener to the the hitdrum unity event
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

    // when the unityEvent drumhit is envoked this function is called 
    //It takes the speed with which we are hitting the drum as an argument 
    // this value controls how long the lights will remain turned off
    // the stronger you hit the longer the lights stay off
    public void flicker(float hitForce, Vector3 u, int n )
    {
        if(hitForce>20 & gameManager.Instance.numberOfHits>3)
        StartCoroutine(Flickering(hitForce));

    }
    IEnumerator Flickering(float OffDuration)
    {
        //first we turn off the lights
        this.gameObject.GetComponent<Light>().enabled = false;

        //the emissive material of the neon lamps is turned down too
        emissiveMaterial.SetColor("_EmissionColor", 0*Color.black);

        //plays the buzzing sound effect
        AudioClip spark= soundManager.soundsArray[(int)soundManager.sounds.ELECTRICBUZZ];
        soundManager.instantiateSound(transform.position, spark,.5f, spark.length);

        
        ParticleSystem sparks = GetComponent<ParticleSystem>();
        sparks.Play();

        timeDelay = Random.Range(0.01f, .1f)* OffDuration;
        // multiplies by the flickeringtime which corresponds to the force by which we hit
        
        yield return new WaitForSeconds(timeDelay);

        //turns the emmissive material back on
        emissiveMaterial.SetColor("_EmissionColor", normalLightEmissionIntensity  * new Color(.75f,.75f,.6f));
        //turns the lights back on
        this.gameObject.GetComponent<Light>().enabled = true;

        //plays the light-going-back-on sound effect
        AudioClip lightON = soundManager.soundsArray[(int)soundManager.sounds.NEONLIGHTON];
        soundManager.instantiateSound(transform.position, lightON,.5f, lightON.length);


        //may be this part is not necessary// will check later 
        timeDelay = Random.Range(.05f, .15f)*OffDuration;
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
